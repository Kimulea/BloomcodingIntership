using Bloomcoding.Bll.Intefaces;
using Bloomcoding.Common.Dtos.Groups;
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

        //[Authorize(Roles = "Admin")]
        [AllowAnonymous]
        [HttpGet("GetAllGroups")]
        public async Task<IEnumerable<GroupListDto>> GetAllGroups()
        {
            var publishers = await _groupService.GetAllGroups();
            return publishers;
        }

        /*public async Task<IActionResult> CreateGroup(CreateGroupDto createGroupDto)
        {

        }*/
    }
}
