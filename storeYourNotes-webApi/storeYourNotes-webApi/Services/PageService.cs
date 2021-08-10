using AutoMapper;
using Microsoft.EntityFrameworkCore;
using storeYourNotes_webApi.Entities;
using storeYourNotes_webApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace storeYourNotes_webApi.Services
{
    public interface IPageService
    {
        PagedResult<PageRecord> GetPageContent(PageQuery pageQuery);
    }

    public class PageService : IPageService
    {
        private readonly IMapper _mapper;
        private readonly StoreYourNotesDbContext _dbContext;

        public PageService(IMapper mapper, StoreYourNotesDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }
        public PagedResult<PageRecord> GetPageContent(PageQuery pageQuery)
        {
            var page = _dbContext
                .Pages
                .FirstOrDefault(p => p.Id == pageQuery.PageId);

            var pageContents = page.PageContents;
            //TODO convert JSON to PageRecord list
        }
    }
}
