using Domain;
using Domain.Entities;
using Domain.Wrapper;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
[Route("[controller]")]

public class MentorController:ControllerBase
{
 private MentorService _mentorService;
 public MentorController(MentorService mentorService)
 {
    _mentorService = mentorService;
 }

 [HttpGet("GetMentors")]
 public async Task<Response<List<Mentor>>> GetMentorss()
 {
   var result =  await _mentorService.GetMentors();
   return result;
 }
 

 [HttpPost("AddMentors")]
public async Task<Response<Mentor>>  AddMentors(Mentor mentor)
{
   var result =  await _mentorService.AddMentor(mentor);
   return result;
}

[HttpPut("UpdateMentors")]
public async Task<Response<Mentor>> UpdateMentors(Mentor mentor)
{
   var res = await _mentorService.UpdateMentorss(mentor);
   return res;
}

[HttpDelete("DeleteMentors")]
public async Task<Response<int>> DeleteMentors(int id)
{
 return await _mentorService.DeleteMentor(id);
}


}
