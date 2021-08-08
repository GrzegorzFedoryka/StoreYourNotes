using AutoMapper;
using Microsoft.EntityFrameworkCore;
using storeYourNotes_webApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace storeYourNotes_webApi.Services
{
    public interface IPageService
    {
        void GetPageContent(PageQuery pageQuery);
    }

    public class PageService : IPageService
    {
        private readonly IMapper _mapper;
        private readonly DbContext _dbContext;

        public PageService(IMapper mapper, DbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }
        public void GetPageContent(PageQuery pageQuery)
        {
            
        }
    }
}
