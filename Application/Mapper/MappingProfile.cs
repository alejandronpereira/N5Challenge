using Application.ViewModels;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Permission, PermissionModel>()
                .ForMember(x => x.PermissionType, x => x.MapFrom(y => y.PermissionType.Id));

            CreateMap<PermissionModel, Permission>()
            .ForMember(dest => dest.PermissionType, opt => opt.MapFrom(src => new PermissionType { Id = src.PermissionType }))
            .ForMember(dest => dest.Id, opt => opt.Ignore()); 

            CreateMap<int, PermissionType>().ConvertUsing(id => new PermissionType { Id = id });

            CreateMap<Permission, PermissionViewModel>().ReverseMap();
        }

    }
}
