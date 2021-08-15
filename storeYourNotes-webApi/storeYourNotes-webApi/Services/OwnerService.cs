using AutoMapper;
using storeYourNotes_webApi.Entities;
using storeYourNotes_webApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace storeYourNotes_webApi.Services
{
    public interface IOwnerService
    {
        public int CreateOwner(CreateOwnerDto dto);
    }
    public class OwnerService : IOwnerService
    {
        private readonly IMapper _mapper;
        private readonly StoreYourNotesDbContext _dbContext;

        public OwnerService(IMapper mapper, StoreYourNotesDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }
        public int CreateOwner(CreateOwnerDto dto)
        {
            var owner = _mapper.Map<Owner>(dto);
            _dbContext.Owners.Add(owner);
            _dbContext.SaveChanges();
            var id = owner.Id;

            return id;
        }
    }
}
