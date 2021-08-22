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
        PagedResult<PageRecord> GetPageContents(int pageId, PageQuery pageQuery);
        public int CreatePage(CreatePageDto dto);
        public void UpdatePageContents(int pageId, List<PageRecordDto> pageRecords);
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
        public PagedResult<PageRecord> GetPageContents(int pageId, PageQuery pageQuery)
        {
            var page = FindPage(pageId);

            var allPageRecords = page.PageRecords;

            var pagedPageRecords = allPageRecords
                .Skip(pageQuery.RecordsPackageSize * (pageQuery.RecordsPackageNumber - 1))
                .Take(pageQuery.RecordsPackageSize)
                .ToList();

            var recordsCount = allPageRecords.Count;

            var pageSize = pageQuery.RecordsPackageSize;
            var pageNumber = pageQuery.RecordsPackageNumber;

            var result = new PagedResult<PageRecord>(pagedPageRecords, recordsCount, pageSize, pageNumber);

            return result;
        }
        public void UpdatePageContents(int pageId, List<PageRecordDto> pageRecords)
        {
            var page = FindPage(pageId);

            foreach (var pageRecord in pageRecords)
            {
                if(pageRecord.Action == PageRecordAction.UPDATE)
                {
                    if( page.PageRecords.Count <= pageRecord.Id)
                    {
                        throw new NotFoundException("Page record doesn't exist");
                    }
                    //TODO
                }
            }
        }

        private Page FindPage(int pageId)
        {
            var page = _dbContext
                .Pages
                .Include(p => p.PageRecords)
                .FirstOrDefault(p => p.Id == pageId);

            if (page is null)
            {
                throw new NotFoundException("Page not found");
            }

            return page;
        }
    }
}
