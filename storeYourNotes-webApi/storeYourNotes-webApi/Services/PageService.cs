using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using storeYourNotes_webApi.Entities;
using storeYourNotes_webApi.Models;
using storeYourNotes_webApi.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace storeYourNotes_webApi.Services
{
    public interface IPageService
    {
        PagedResult<PageRecord> GetPageContent(int pageId, PageQuery pageQuery);
        public int CreatePage(CreatePageDto dto);
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

        public int CreatePage(CreatePageDto dto)
        {
            var page = _mapper.Map<Page>(dto);
            _dbContext.Pages.Add(page);
            _dbContext.SaveChanges();

            var id = page.Id;

            return id;

        }
        public PagedResult<PageRecord> GetPageContent(int pageId, PageQuery pageQuery)
        {
            var page = _dbContext
                .Pages
                .FirstOrDefault(p => p.Id == pageId);

            if(page is null)
            {
                throw new NotFoundException("Page not found");
            }
            if (string.IsNullOrEmpty(page.PageContents))
            {
                throw new NotFoundException("There is no contents");
            }
            var allPageContents = page.PageContents;
            List<PageRecord> pageRecords = JsonConvert.DeserializeObject<List<PageRecord>>(allPageContents);

            var pagedPageRecords = pageRecords
                .Skip(pageQuery.RecordsPackageSize * (pageQuery.RecordsPackageNumber - 1))
                .Take(pageQuery.RecordsPackageSize)
                .ToList();

            var recordsCount = pageRecords.Count;

            var pageSize = pageQuery.RecordsPackageSize;
            var pageNumber = pageQuery.RecordsPackageNumber;

            var result = new PagedResult<PageRecord>(pagedPageRecords, recordsCount, pageSize, pageNumber);

            return result;
        }

        private Exception NotFoundException()
        {
            throw new NotImplementedException();
        }
    }
}
