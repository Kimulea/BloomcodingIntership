using AutoMapper;
using Bloomcoding.Bll.Intefaces;
using Bloomcoding.Common.Dtos.Groups;
using Bloomcoding.Dal.Interfaces;
using Bloomcoding.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloomcoding.Bll.Services
{
    public class GroupService : IGroupService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public GroupService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GroupListDto>> GetAllGroups()
        {
            var groupList = await _repository.GetAll<Group>();

            /*List<GroupListDto> groupListDtos = new List<GroupListDto>();

            foreach (var item in groupList)
            {
                GroupListDto groupListDto = new GroupListDto()
                {
                    Name = item.Name,
                    Level = item.Level
                };

                groupListDtos.Add(groupListDto);
            }*/

            var groupDtoList = _mapper.Map<List<GroupListDto>>(groupList);
            return groupDtoList;
        }
    }
}
