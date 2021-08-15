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
    [Route("owners")]
    public class OwnerController : ControllerBase
    {
        private readonly IOwnerService _ownerService;

        public OwnerController(IOwnerService ownerService)
        {
            _ownerService = ownerService;
        }

        [HttpPost]
        public ActionResult CreateOwner(CreateOwnerDto dto)
        {
            var ownerId = _ownerService.CreateOwner(dto);
            return Created($"{ownerId}", null);
        }
    }
}
