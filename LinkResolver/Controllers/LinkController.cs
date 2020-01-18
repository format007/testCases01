using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinkResolver.Models.Dto.Requests;
using LinkResolver.Models.UseCases.Interfaces;
using LinkResolver.Models.UseCases.Link;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LinkResolver.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinkController : ControllerBase
    {
        [HttpPost("resolve")]
        public async Task<IActionResult> Resolve([FromBody]string shortUrl, 
            [FromServices]ICommandHandler<LinkResolveCommand, string> handler)
        {
            var result = await handler.ExecAsync(new LinkResolveCommand() { ShortUrl = shortUrl });
            if (result.Success)
                return Ok(result.Result);
            else
                return BadRequest(result.Error);
        }

        [HttpPost("convert")]
        public async Task<IActionResult> Convert([FromBody]string longUrl, 
            [FromServices]ICommandHandler<LinkSaveCommand, string> handler)
        {
            var result = await handler.ExecAsync(new LinkSaveCommand() { LongUrl = longUrl });
            if (result.Success)
                return Ok(result.Result);
            else
                return BadRequest(result.Error);
        }

    }
}