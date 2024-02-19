using MyMusic.Abstract;
using MyMusic.Models;
using System.Data;
using System.Data.SqlClient;
using Dapper;
namespace MyMusic.Service
{
    public class MusService : IMusic<song>
    {
        IConfiguration config;
        public MusService(IConfiguration config)
        {
            this.config = config;
        }

        public bool AddSong(song song)
        {
            using (SqlConnection db = new SqlConnection(config["conStr"]))
            {
                DynamicParameters p =  new DynamicParameters();
                p.Add("artist", song.artist);
                p.Add("title", song.title);
                p.Add("categoryid", song.categoryid);
                p.Add("duration", song.duration);
                p.Add("typeid", song.typeid);
                var res =  db.Query<song>("pMusic;2",p , commandType: CommandType.StoredProcedure);
                if (res != null)
                {
                    return true;
                }
                else return false;
            }
        }

        public IEnumerable<song> GetValues()
        {
            using (SqlConnection db = new SqlConnection(config["conStr"]))
            {
                return db.Query<song>("pMusic", commandType: CommandType.StoredProcedure);
            }
        }

    }
}
