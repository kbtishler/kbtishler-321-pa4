using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using mis321_pa4_kbtishler.api.Models.Interfaces;
using MySql.Data.MySqlClient;
using mis321_pa4_kbtishler.api;
using mis321_pa4_kbtishler.api.Models.Interfaces;
using mis321_pa4_kbtishler.Models;
using System.Collections.Generic;

namespace repos.mis321_pa4_kbtishler.api.Database
{
    public class WriteToDatabase : ICreateSongs
    {
        public static void CreateSongTable()
        {
            ConnectionString myConnection = new ConnectionString();
            string cs = myConnection.cs;

            using var con = new MySqlConnection(cs);
            con.Open();

            string stm = @"CREATE TABLE songs(id INTEGER PRIMARY KEY AUTO_INCREMENT, title TEXT, timeadded DATETIME, deleted TEXT, favorited TEXT)";

            using var cmd = new MySqlCommand(stm, con);

            cmd.ExecuteNonQuery();
        }
        public void Create(Song song)
        {
            //data insertion
            ConnectionString myConnection = new ConnectionString();
            string cs = myConnection.cs;

            using var con = new MySqlConnection(cs);
            con.Open();

            string stm = @"INSERT INTO songs(title, timeadded, deleted, favorited) VALUES(@title, @timeadded, @deleted, @favorited)";

            using var cmd = new MySqlCommand(stm, con);

            cmd.Parameters.AddWithValue("@title", song.SongTitle);
            cmd.Parameters.AddWithValue("@timeadded", song.SongTimestamp);
            cmd.Parameters.AddWithValue("@deleted", song.Deleted);
            cmd.Parameters.AddWithValue("@favorited", song.Favorited);


            cmd.Prepare();

            cmd.ExecuteNonQuery();
        }

        public static void  WriteAllToDatabase(List<Song> playlist)
        {
            ConnectionString myConnection = new ConnectionString();
            string cs = myConnection.cs;
            using var con = new MySqlConnection(cs);
            con.Open();

            string drop = @"DROP TABLE if exists songs";

            using var cmd = new MySqlCommand(drop,con);
            cmd.ExecuteNonQuery();

            WriteToDatabase.CreateSongTable();

            string stm = @"INSERT INTO songs(id,title,timeadded, deleted, favorited) VALUES(@id,@title,@timeadded,@deleted,@favorited)";

            foreach(Song song in playlist)
            {
                using var c = new MySqlCommand(stm,con);
                string deleted = "n";
                string favorited = "n";
                
                c.Parameters.AddWithValue("@id",song.SongID);
                c.Parameters.AddWithValue("@title",song.SongTitle);
                c.Parameters.AddWithValue("@timeadded",song.SongTimestamp);
                c.Parameters.AddWithValue("@deleted",deleted);
                c.Parameters.AddWithValue("@favorited",favorited);

                c.Prepare();

                c.ExecuteNonQuery();
            }
        }
    }
}