using Bloomcoding.Bll.Exceptions;
using Bloomcoding.Bll.Intefaces;
using Bloomcoding.Common.Dtos.Groups;
using Bloomcoding.Common.Models.PagedRequest;
using Bloomcoding.Dal.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bloomcoding.API.Controllers
{
    [Route("api/group")]
    public class GroupController : BaseController
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        /*//[Authorize(Roles = "Admin")]
        [AllowAnonymous]
        [HttpGet("GetAllGroups")]
        public async Task<IEnumerable<GroupListDto>> GetAllGroups()
        {
            var publishers = await _groupService.GetAllGroups();
            return publishers;
        }*/

        //[Authorize(Roles = "Admin")]
        [AllowAnonymous]
        [HttpPost("paginated-search")]
        public async Task<PagedResult<GroupListDto>> GetPagedGroups([FromBody] PagedRequest pagedRequest)
        {
            var pagedGroupsDto = await _groupService.GetPagedGroups(pagedRequest);
            return pagedGroupsDto;
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        [ApiExceptionFilter]
        public async Task<GroupDto> GetGroup(int id)
        {
            var groupDto = await _groupService.GetGroup(id);
            return groupDto;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateGroup(NewGroupDto newGroupDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var bookDto = await _groupService.CreateGroup(newGroupDto);

            return CreatedAtAction(nameof(GetGroup), new { Name = bookDto.Name }, bookDto);
        }

        [AllowAnonymous]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGroup(int id, GroupDto groupDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _groupService.UpdateGroup(id, groupDto);
            return Ok();
        }

        [AllowAnonymous]
        [HttpDelete("{id}")]
        public async Task DeleteGroup(int id)
        {
            await _groupService.DeleteGroup(id);
        }
    }
}
