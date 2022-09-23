using Microsoft.Extensions.Configuration;
using Npgsql;
namespace Infrastructure.DataContext;

public class DataContext
{
    private readonly IConfiguration _configuration;
    public DataContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public NpgsqlConnection CreateConnection()
    {
        var connectionString = _configuration.GetConnectionString("NpgsqlConnection");
        return new NpgsqlConnection(connectionString);
    }
}

