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
        public void UpdatePageRecords(int pageId, List<PageRecordDto> pageRecords);
        public void CreatePageRecords(int pageId, List<PageRecordDto> pageRecords);
        public List<Page> GetPages();

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
        public List<Page> GetPages()
        {
            var pages = _dbContext
                .Pages
                .Include(p => p.PageRecords)
                .ToList();
            return pages;
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
        public void CreatePageRecords(int pageId, List<PageRecordDto> pageRecords)
        {
            var page = FindPage(pageId);
            List<PageRecord> newPageRecords = new();
            foreach (var pageRecordDto in pageRecords)
            {
                var newPageRecord = _mapper.Map<PageRecord>(pageRecordDto);

                var previousPageRecord = FindPreviousRecord(pageRecordDto.PreviousRecordId);
                if (previousPageRecord != null)
                {
                    newPageRecord.NextRecordId = previousPageRecord.NextRecordId;
                }
                else
                {
                    var nextPageRecord = FindNextRecord(null);
                    if (nextPageRecord != null)
                    {
                        newPageRecord.NextRecordId = nextPageRecord.Id;
                    }
                    else
                    {
                        newPageRecord.NextRecordId = null;
                    }
                }

                newPageRecord.PageId = page.Id;
                newPageRecord.Id = 0;
                _dbContext.PageRecords.Add(newPageRecord);

                newPageRecords.Add(newPageRecord);
            }

            _dbContext.SaveChanges();

            UpdateAdjacentRecords(newPageRecords);

            _dbContext.SaveChanges();
        }

        public void UpdatePageRecords(int pageId, List<PageRecordDto> pageRecords)
        {
            //var page = FindPage(pageId);

            foreach (var pageRecordDto in pageRecords)
            {
                var pageRecord = _dbContext
                    .PageRecords
                    .FirstOrDefault(r => r.Id == pageRecordDto.Id);

                if (pageRecord is null)
                {
                    RollbackChanges();
                    throw new NotAcceptableException("Page record(s) don't exist, can't update");
                }

                var nextRecordId = pageRecord.NextRecordId;
                var previousRecordId = pageRecord.PreviousRecordId;
                var recordId = pageRecord.Id;
                _dbContext.PageRecords.Update(pageRecord);
                _mapper.Map(pageRecordDto, pageRecord);

                pageRecord.NextRecordId = nextRecordId;
                pageRecord.PreviousRecordId = previousRecordId;
                pageRecord.Id = recordId;
                pageRecord.PageId = pageId;

                _dbContext.PageRecords.Update(pageRecord);
            }

            _dbContext.SaveChanges();
        }

        private PageRecord FindPreviousRecord(int? previousRecordId)
        {
            var previousPageRecord = previousRecordId != null ?
                        _dbContext
                        .PageRecords
                        .FirstOrDefault(r => r.Id == previousRecordId) :
                        null;
            return previousPageRecord;
        }
        private PageRecord FindNextRecord(int? previousRecordId)
        {
            var nextPageRecord = _dbContext
                        .PageRecords
                        .FirstOrDefault(previousRecordId != null ?
                            r => r.PreviousRecordId == previousRecordId :
                            r => r.PreviousRecordId == null);
            return nextPageRecord;
        }
        private PageRecord FindNextRecord(int? previousRecordId, int exceptId)
        {
            var nextPageRecord = _dbContext
                        .PageRecords
                        .FirstOrDefault(previousRecordId != null ?
                            r => r.PreviousRecordId == previousRecordId :
                            r => r.PreviousRecordId == null && r.Id != exceptId);
            return nextPageRecord;
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

        private void RollbackChanges()
        {
            var dbChangedEntries = _dbContext.ChangeTracker.Entries()
                .Where(c => c.State != EntityState.Unchanged)
                .ToList();

            foreach (var entry in dbChangedEntries)
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.CurrentValues.SetValues(entry.OriginalValues);
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Unchanged;
                        break;
                }
            }
        }
        private void UpdateAdjacentRecords(List<PageRecord> addedRecords)
        {
            foreach (var record in addedRecords)
            {
                var previousRecord = FindPreviousRecord(record.PreviousRecordId);
                if (previousRecord != null)
                {
                    previousRecord.NextRecordId = record.Id;
                    _dbContext.PageRecords.Update(previousRecord);
                }

                var nextRecord = FindNextRecord(record.PreviousRecordId, record.Id);
                if (nextRecord != null)
                {
                    nextRecord.PreviousRecordId = record.Id;
                    _dbContext.PageRecords.Update(nextRecord);
                }
            }
        }
    }
}
