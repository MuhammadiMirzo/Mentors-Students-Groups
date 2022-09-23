namespace Infrastructure.Services;
using Dapper;
using Domain;
using Domain.Wrapper;
using Npgsql;
using Infrastructure.DataContext;
using Domain.Entities;

public class StudentServices
{

    private DataContext _context;
    public StudentServices(DataContext context)
    {
        _context = context;
    }

    public async Task<Response<List<Student>>> GetStudents()
    {
        using (var connection = _context.CreateConnection())
        {
            try
            {
             var response = await connection.QueryAsync<Student>($"select * from Student;");
            return new Response<List<Student>>(response.ToList());   
            }
            catch (Exception ex)
            {
                return new Response<List<Student>>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
            
        }
    }
    

    public async Task<Response<Student>> AddStudent(Student student)
    {
        // Add Studentss to database
        using (var connection = _context.CreateConnection())
        {
            try
            {
                string sql = $"insert into Student (FirstName,LastName,Email,Phone,Address,City) values ('{student.FirstName}', '{student.LastName}','{student.Email}','{student.Phone}','{student.Adress}','{student.City}') returning id";
                var id = await connection.ExecuteScalarAsync<int>(sql);
                student.Id = id;
                return new Response<Student>(student);
            }
            catch (Exception ex)
            {
                return new Response<Student>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }


        }
    }
    public async Task<Response<int>> DeleteStudent(int id)
    {
        // Add contact to database
        using (var connection = _context.CreateConnection())
        {
            try
            {
                connection.Open();
                string sql = $"delete from Student where id = '{id}';";
                var response = await connection.ExecuteAsync(sql);
                return new Response<int>(response);
            }
            catch (System.Exception ex)
            {

                return new Response<int>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }

    public async Task<Response<Student>> UpdateStudentss(Student student)
    {
        // Add contact to database
        using (var connection = _context.CreateConnection())
        {
            try
            {
             string sql = $"UPDATE Student SET FirstName='{student.FirstName}',LastName='{student.LastName}',Email = '{student.Email}',Phone='{student.Phone}',Address='{student.Adress}',City='{student.City}' WHERE id = {student.Id} returning id;";
            var id = await connection.ExecuteScalarAsync<int>(sql);
            student.Id = id;
            return new Response<Student>(student);   
            }
            catch (Exception ex)
            {
                return new Response<Student>(System.Net.HttpStatusCode.InternalServerError,ex.Message);
            }
            
        }
    }

}

