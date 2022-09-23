namespace Infrastructure.Services;
using Dapper;
using Domain;
using Domain.Wrapper;
using Npgsql;
using Infrastructure.DataContext;
using Domain.Entities;

public class GroupService
{

    private DataContext _context;
    public GroupService(DataContext context)
    {
        _context = context;
    }

    public async Task<Response<List<Groupes>>> GetGroups()
    {
        using (var connection = _context.CreateConnection())
        {
            try
            {
             var response = await connection.QueryAsync<Groupes>($"select * from Groupes;");
            return new Response<List<Groupes>>(response.ToList());   
            }
            catch (Exception ex)
            {
                return new Response<List<Groupes>>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
            
        }
    }
    

    public async Task<Response<Groupes>> AddGroup(Groupes group)
    {
        // Add Groupss to database
        using (var connection = _context.CreateConnection())
        {
            try
            {
                string sql = $"insert into Groupes (GroupName, GroupDescription, CourseId) values ('{group.GroupName}','{group.GroupDescription}',{group.CourseId}) returning id";
                var id = await connection.ExecuteScalarAsync<int>(sql);
                group.Id = id;
                return new Response<Groupes>(group);
            }
            catch (Exception ex)
            {
                return new Response<Groupes>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
    public async Task<Response<int>> DeleteGroup(int id)
    {
        // Add contact to database
        using (var connection = _context.CreateConnection())
        {
            try
            {
                string sql = $"delete from Groupes where id = '{id}';";
                var response = await connection.ExecuteAsync(sql);
                return new Response<int>(response);
            }
            catch (System.Exception ex)
            {

                return new Response<int>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }

    public async Task<Response<Groupes>> UpdateGroupss(Groupes group)
    {
        // Add contact to database
        using (var connection = _context.CreateConnection())
        {
            try
            {
             string sql = $"UPDATE Groupes SET GroupName='{group.GroupName}',GroupDescription='{group.GroupDescription}',CourseId= '{group.CourseId}' WHERE id = {group.Id} returning id;";
            var id = await connection.ExecuteScalarAsync<int>(sql);
            group.Id = id;
            return new Response<Groupes>(group);   
            }
            catch (Exception ex)
            {
                return new Response<Groupes>(System.Net.HttpStatusCode.InternalServerError,ex.Message);
            }
            
        }
    }

}

