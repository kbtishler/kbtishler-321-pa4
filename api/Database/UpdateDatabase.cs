using System.Xml.Linq;
using System;
using System.IO;
using System.Collections.Generic;
using mis321_pa4_kbtishler.api;
using mis321_pa4_kbtishler.Models;
using mis321_pa4_kbtishler.api.Models.Interfaces;
using MySql.Data.MySqlClient;
using MySql.Data;
using System.Data;

namespace repos.mis321_pa4_kbtishler.api.Database
{
    public class UpdateDatabase : IUpdateSongs
    {
        public void Update(Song s)
        {
            ConnectionString myConnection = new ConnectionString();
            string cs = myConnection.cs;
            using var con = new MySqlConnection(cs);
            string fav = "y";
            con.Open();

            using var cmd = new MySqlCommand(@"UPDATE songs set timeadded = @timeadded, deleted = @deleted, favorited = @favorited WHERE title=@title");
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@title",s.SongTitle);
            cmd.Parameters.AddWithValue("@timeadded",s.SongTimestamp);
            cmd.Parameters.AddWithValue("@deleted",s.Deleted);
            cmd.Parameters.AddWithValue("@favorited",fav);

            cmd.Prepare();

            cmd.ExecuteNonQuery();


        }
    }
}