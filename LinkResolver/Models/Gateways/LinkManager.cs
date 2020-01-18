using LinkResolver.Models.Data.Repositories;
using LinkResolver.Models.Domain.Entities;
using LinkResolver.Models.Gateways.Interfaces;
using LinkResolver.Models.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
                throw new Exception("invalidshorturl");// заменить на конкретное исключение

            if (!ShortUrl.StartsWith(options.UrlBase, StringComparison.OrdinalIgnoreCase))
                throw new Exception("invalidDomain");// заменить на конкретное исключение

            var shortPart = ShortUrl.Substring(options.UrlBase.Length);

            var link = (await linkMgr.GetFiltered(new LinkSpecification(l => l.ShortLink == shortPart))).FirstOrDefault();

            if (link != null)
                return link.LongLink;
            else
                throw new Exception("link not found");// заменить на конкретное исключение
        }

        public async Task<string> Save(string LongUrl)
        {
            //LongUrl = LongUrl.ToUpper();

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
                    var shortPart = codeGen.Generate().ToHexString().Substring(0, options.ShortMaxSize);
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

                        await linkMgr.Create(link);
                        return GetFullShortLink(link.ShortLink);
                    }

                } while (++retryCount < options.RetryLimit);
                throw new Exception("Operation can't be completed");
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
