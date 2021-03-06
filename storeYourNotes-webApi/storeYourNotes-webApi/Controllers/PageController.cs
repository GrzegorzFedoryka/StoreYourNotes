using Microsoft.AspNetCore.Mvc;
using storeYourNotes_webApi.Entities;
using storeYourNotes_webApi.Models;
using storeYourNotes_webApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

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

        [HttpGet]
        public ActionResult GetPages()
        {
            var pages = _pageService.GetPages();
            return Ok(pages);
        }
        [HttpPost]
        public ActionResult CreatePage([FromBody]CreatePageDto dto)
        {
            var pageId = _pageService.CreatePage(dto);

            return Created($"/{pageId}", null);
        }

        [HttpGet("{pageId}")]
        public ActionResult GetPageContents([FromRoute]int pageId, [FromQuery]PageQuery pageQuery)
        {
            var pagedRecords = _pageService.GetPageContents(pageId, pageQuery);
            return Ok(pagedRecords);
        }
        [HttpPut("{pageId}")]
        public ActionResult UpdatePageRecords([FromRoute]int pageId, [FromBody]List<PageRecordDto> pageRecords)
        {
            _pageService.UpdatePageRecords(pageId, pageRecords);
            return Ok($"/{pageId}");
        }
        [HttpPost("{pageId}")]
        public ActionResult CreatePageRecords([FromRoute] int pageId, [FromBody] List<PageRecordDto> pageRecords)
        {
            _pageService.CreatePageRecords(pageId, pageRecords);
            return Ok();
        }
    }
}
