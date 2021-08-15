using Microsoft.AspNetCore.Mvc;
using storeYourNotes_webApi.Models;
using storeYourNotes_webApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace storeYourNotes_webApi.Controllers
{
    [ApiController]
    [Route("")]
    public class PageController : ControllerBase
    {
        private readonly IPageService _pageService;

        public PageController(IPageService pageService)
        {
            _pageService = pageService;
        }

        [HttpPost]
        public ActionResult CreatePage([FromBody]CreatePageDto dto)
        {
            var pageId = _pageService.CreatePage(dto);

            return Created($"/{pageId}", null);
        }

        [HttpGet("{pageId}")]
        public ActionResult GetPageContent([FromRoute]int pageId, [FromQuery]PageQuery pageQuery)
        {
            var pagedRecords = _pageService.GetPageContent(pageId, pageQuery);
            return Ok(pagedRecords);
        }
    }
}
