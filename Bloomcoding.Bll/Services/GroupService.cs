using AutoMapper;
using Bloomcoding.Bll.Intefaces;
using Bloomcoding.Common.Dtos.Groups;
using Bloomcoding.Common.Models.PagedRequest;
using Bloomcoding.Dal.Interfaces;
using Bloomcoding.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Net;
using Bloomcoding.Bll.Exceptions;

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

        public async Task<PagedResult<GroupListDto>> GetPagedGroups(PagedRequest pagedRequest)
        {
            var pagedGroupsDto = await _repository.GetPagedData<Group, GroupListDto>(pagedRequest);
            return pagedGroupsDto;
        }

        public async Task<GroupDto> GetGroup(int id)
        {
            var group = await _repository.GetById<Group>(id);

            if(group == null)
            {
                throw new EntryNotFoundException("Group not found");
            }

            var groupDto = _mapper.Map<GroupDto>(group);
            return groupDto;
        }

        public async Task<GroupDto> CreateGroup(NewGroupDto newGroupDto)
        {
            var group = _mapper.Map<Group>(newGroupDto);
            _repository.Add(group);
            await _repository.SaveChangesAsync();

            var groupDto = _mapper.Map<GroupDto>(group);

            return groupDto;
        }

        public async Task UpdateGroup(int id, GroupDto bookDto)
        {
            var group = await _repository.GetById<Group>(id);

            _mapper.Map(bookDto, group);    
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteGroup(int id)
        {
            await _repository.Delete<Group>(id);
            await _repository.SaveChangesAsync();
        }
    }
}
