using Domain;
using Domain.Entities;
using Domain.Wrapper;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
[Route("[controller]")]

public class GroupController:ControllerBase
{
 private GroupService _groupService;
 public GroupController(GroupService groupService)
 {
    _groupService = groupService;
 }

 [HttpGet("GetGroups")]
 public async Task<Response<List<Groupes>>> GetGroupss()
 {
   var result =  await _groupService.GetGroups();
   return result;
 }
 

 [HttpPost("AddGroups")]
public async Task<Response<Groupes>>  AddGroups(Groupes group)
{
   var result =  await _groupService.AddGroup(group);
   return result;
}

[HttpPut("UpdateGroups")]
public async Task<Response<Groupes>> UpdateGroups(Groupes group)
{
   var res = await _groupService.UpdateGroupss(group);
   return res;
}

[HttpDelete("DeleteGroups")]
public async Task<Response<int>> DeleteGroups(int id)
{
 return await _groupService.DeleteGroup(id);
}


}
