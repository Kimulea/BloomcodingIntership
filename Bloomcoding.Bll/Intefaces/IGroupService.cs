using Bloomcoding.Common.Dtos.Groups;
using Bloomcoding.Common.Models.PagedRequest;
using Bloomcoding.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloomcoding.Bll.Intefaces
{
    public interface IGroupService
    {
        Task<PagedResult<GroupListDto>> GetPagedGroups(PagedRequest pagedRequest);

        Task<GroupDto> GetGroup(int id);

        Task<GroupDto> CreateGroup(NewGroupDto newGroupDto);

        Task UpdateGroup(int id, GroupDto groupDto);

        Task DeleteGroup(int id);
    }
}
