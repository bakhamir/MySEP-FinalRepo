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
                return db.Query<song>("GetAllSongs", commandType: CommandType.StoredProcedure);
            }
        }
        public bool EditSong(song song)
        {
            using (SqlConnection db = new SqlConnection(config["conStr"]))
            {
                DynamicParameters p = new DynamicParameters();
                p.Add("id", song.id); // assuming song.id is the primary key
                p.Add("artist", song.artist);
                p.Add("title", song.title);
                p.Add("categoryid", song.categoryid);
                p.Add("duration", song.duration);
                p.Add("typeid", song.typeid);
                var res = db.Execute("pEditMusic", p, commandType: CommandType.StoredProcedure);
                return res > 0;
            }
        }

        public bool DeleteSong(int id)
        {
            using (SqlConnection db = new SqlConnection(config["conStr"]))
            {
                DynamicParameters p = new DynamicParameters();
                p.Add("id", id);
                var res = db.Execute("pDeleteMusic", p, commandType: CommandType.StoredProcedure);
                return res > 0;
            }
        }


        object IMusic<song>.GetSongById(int id)
        {
            using (SqlConnection db = new SqlConnection(config["conStr"]))
            {
                DynamicParameters p = new DynamicParameters();
                p.Add("id", id);
                return db.QueryFirstOrDefault<song>("pGetMusicById", p, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
