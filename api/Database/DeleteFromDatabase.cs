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


    public class DeleteFromDatabase : IDeleteSongs
    {
         public void Delete(Song s)
         {
            ConnectionString myConnection = new ConnectionString();
            string cs = myConnection.cs;
            using var con = new MySqlConnection(cs);
            con.Open();

            using var cmd = new MySqlCommand("DELETE FROM songs WHERE title = '" + s.SongTitle+"'",con);


            cmd.ExecuteNonQuery();
         }
    }
}