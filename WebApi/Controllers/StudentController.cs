using Domain;
using Domain.Entities;
using Domain.Wrapper;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
[Route("[controller]")]

public class StudentController:ControllerBase
{
 private StudentServices _studentService;
 public StudentController(StudentServices studentService)
 {
    _studentService = studentService;
 }

 [HttpGet("GetStudents")]
 public async Task<Response<List<Student>>> GetStudentss()
 {
   var result =  await _studentService.GetStudents();
   return result;
 }
 

 [HttpPost("AddStudents")]
public async Task<Response<Student>>  AddStudents(Student student)
{
   var result =  await _studentService.AddStudent(student);
   return result;
}

[HttpPut("UpdateStudents")]
public async Task<Response<Student>> UpdateStudents(Student student)
{
   var res = await _studentService.UpdateStudentss(student);
   return res;
}

[HttpDelete("DeleteStudents")]
public async Task<Response<int>> DeleteStudents(int id)
{
 return await _studentService.DeleteStudent(id);
}


//     [HttpGet("GetRandomStudents")]
//  public async Task<Response<List<Students>>> GetRandomStudents()
//  {
//    var result = await _StudentService.GetRandomStudents();
//    return result;
//  }
}
