namespace Infrastructure.Services;
using Dapper;
using Domain;
using Domain.Wrapper;
using Npgsql;
using Infrastructure.DataContext;
using Domain.Entities;

public class MentorService
{

    private DataContext _context;
    public MentorService(DataContext context)
    {
        _context = context;
    }

    public async Task<Response<List<Mentor>>> GetMentors()
    {
        using (var connection = _context.CreateConnection())
        {
            try
            {
             var response = await connection.QueryAsync<Mentor>($"select * from Mentor;");
            return new Response<List<Mentor>>(response.ToList());   
            }
            catch (Exception ex)
            {
                return new Response<List<Mentor>>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
            
        }
    }
    

    public async Task<Response<Mentor>> AddMentor(Mentor mentor)
    {
        // Add Mentorss to database
        using (var connection = _context.CreateConnection())
        {
            try
            {
                string sql = $"insert into Mentor (FirstName,LastName,Email,Phone,Address,City) values ('{mentor.FirstName}', '{mentor.LastName}','{mentor.Email}','{mentor.Phone}','{mentor.Adress}','{mentor.City}') returning id";
                var id = await connection.ExecuteScalarAsync<int>(sql);
                mentor.Id = id;
                return new Response<Mentor>(mentor);
            }
            catch (Exception ex)
            {
                return new Response<Mentor>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }


        }
    }
    public async Task<Response<int>> DeleteMentor(int id)
    {
        // Add contact to database
        using (var connection = _context.CreateConnection())
        {
            try
            {
                connection.Open();
                string sql = $"delete from Mentor where id = '{id}';";
                var response = await connection.ExecuteAsync(sql);
                return new Response<int>(response);
            }
            catch (System.Exception ex)
            {

                return new Response<int>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }

    public async Task<Response<Mentor>> UpdateMentorss(Mentor mentor)
    {
        // Add contact to database
        using (var connection = _context.CreateConnection())
        {
            try
            {
             string sql = $"UPDATE Mentor SET FirstName='{mentor.FirstName}',LastName='{mentor.LastName}',Email = '{mentor.Email}',Phone='{mentor.Phone}',Address='{mentor.Adress}',City='{mentor.City}' WHERE id = {mentor.Id} returning id;";
            var id = await connection.ExecuteScalarAsync<int>(sql);
            mentor.Id = id;
            return new Response<Mentor>(mentor);   
            }
            catch (Exception ex)
            {
                return new Response<Mentor>(System.Net.HttpStatusCode.InternalServerError,ex.Message);
            }
            
        }
    }


    // public async Task<Response<List<Mentor>>> GetRandomMentors()
    // {
    //     using (var connection = _context.CreateConnection())
    //     {
    //         try
    //     {
            
    //             var response = await connection.QueryAsync<Mentor>($"select * from Mentor ORDER BY random() limit 1;");
    //             return new Response<List<Mentor>>(response.ToList());
    //     }
    //     catch (Exception ex)
    //         {
    //             return new Response<List<Mentor>>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
    //         }
    //     }
    // }

}

