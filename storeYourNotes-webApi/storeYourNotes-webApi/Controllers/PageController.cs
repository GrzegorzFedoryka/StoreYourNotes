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
        [HttpGet]
        public ActionResult GetPageContent([FromQuery]PageQuery pageQuery)
        {
            var pagedRecords = _pageService.GetPageContent(pageQuery);
            return Ok(pagedRecords);
        }
    }
}
