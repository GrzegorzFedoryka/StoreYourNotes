using AutoMapper;
using storeYourNotes_webApi.Entities;
using storeYourNotes_webApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace storeYourNotes_webApi
{
    public class StoreYourNotesMappingProfile : Profile
    {
        public StoreYourNotesMappingProfile()
        {
            CreateMap<CreatePageDto, Page>();
            CreateMap<CreateOwnerDto, Owner>();
        }
    }
}
