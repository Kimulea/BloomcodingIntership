using AutoMapper;
using Bloomcoding.Common.Dtos.Groups;
using Bloomcoding.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloomcoding.Bll.Profiles
{
    class GroupProfile : Profile
    {
        public GroupProfile()
        {
            CreateMap<Group, GroupListDto>();
            CreateMap<Group, GroupDto>();
            CreateMap<NewGroupDto, Group>();
        }
    }
}
