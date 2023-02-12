using Dapper;
using IBM.Data.Db2;

namespace AnimeAPI.Controllers;
public class Repository
{
    private readonly string _connectionString;

    public Repository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task Save(Anime anime)
    {
        using (var db = new DB2Connection(_connectionString))
        {
            string sqlQuery = "INSERT INTO animes (id, name) VALUES(@Id, @Name)";

            await db.ExecuteAsync(sqlQuery, anime);
        }
    }

    public async Task<IEnumerable<Anime>> List()
    {
        using (var db = new DB2Connection(_connectionString))
        {
            string sqlQuery = "SELECT * FROM animes";

            return await db.QueryAsync<Anime>(sqlQuery);
        }
    }
}