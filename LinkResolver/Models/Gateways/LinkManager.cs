using LinkResolver.Models.Data.Repositories;
using LinkResolver.Models.Domain.Entities;
using LinkResolver.Models.Gateways.Exceptions;
using LinkResolver.Models.Gateways.Interfaces;
using LinkResolver.Models.Tools;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LinkResolver.Models.Gateways
{
    public class LinkManager : ILinkManager
    {
        private readonly ICodeGenerator codeGen;
        private readonly ILinkEFManager linkMgr;
        private readonly LinkManagerOptions options;

        public LinkManager(ICodeGenerator codeGen, ILinkEFManager linkMgr, LinkManagerOptions options)
        {
            this.codeGen = codeGen;
            this.linkMgr = linkMgr;
            this.options = options;
        }

        public async Task<string> Resolve(string ShortUrl)
        {
            if (ShortUrl.Length != options.UrlBase.Length + options.ShortMaxSize)
                throw new ArgumentException("Invalid parameter", "ShortUrl");

            if (!ShortUrl.StartsWith(options.UrlBase, StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException("Invalid parameter", "ShortUrl");

            var shortPart = ShortUrl.Substring(options.UrlBase.Length);

            var link = (await linkMgr.GetFiltered(new LinkSpecification(l => l.ShortLink == shortPart))).FirstOrDefault();

            if (link != null)
                return link.LongLink;
            else
                throw new ObjectNotFoundException("Link can't be found");
        }

        public async Task<string> Save(string LongUrl)
        {
            //LongUrl = LongUrl.ToUpper();
            if (!Regex.IsMatch(LongUrl, URLHelpers.HttpRegexTemplate))
                throw new ArgumentException("Invalid parameter", "LongUrl");

            //get string sha256 hash
            var hash = GetCryptoHashCode(LongUrl.ToUpper()).ToHexString();

            //check if it exists
            //if exists, return existing short link
            var link = (await linkMgr.GetFiltered(new LinkSpecification(l => l.LinkHash == hash))).FirstOrDefault();
            if (link != null)
                return GetFullShortLink(link.ShortLink);
            else
            {
                int retryCount = 0;
                do
                {
                    //generate shortCode
                    var shortPart = codeGen.Generate();
                    link = (await linkMgr.GetFiltered(new LinkSpecification(l => l.ShortLink == shortPart))).FirstOrDefault();
                    if (link == null)
                    {
                        link = new Link()
                        {
                            LongLink = LongUrl,
                            LinkHash = hash,
                            ShortLink = shortPart,
                            CreatedAt = DateTime.Now
                        };
                        try
                        {
                            await linkMgr.Create(link);
                            return GetFullShortLink(link.ShortLink);
                        }
                        catch (DbUpdateException) { }
                    }

                } while (++retryCount < options.RetryLimit);
                throw new OperationAbortedException("Operation cann't be completed. Try again later");
            }
        }

        private string GetFullShortLink(string shortPart) => options.UrlBase + shortPart;

        private byte[] GetCryptoHashCode(string Link)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(Encoding.UTF8.GetBytes(Link));
            }
        }
    }
}
