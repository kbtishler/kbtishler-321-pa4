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
    public class ReadFromDatabase : IReadSongs
    {
        public List<Song> GetAll() { 
            List<Song> songs = new List<Song>();
            ConnectionString myConnection = new ConnectionString();
            string cs = myConnection.cs; 

            using var con = new MySqlConnection(cs);
            con.Open();

            string stm = "SELECT * FROM songs";
            using var cmd = new MySqlCommand(stm, con);
            using MySqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                int id = rdr.GetInt32(0);
                string title = rdr.GetString(1);
                DateTime timeadded = rdr.GetDateTime(2);
                string deleted = rdr.GetString(3);
                string favorite = rdr.GetString(4);
                songs.Add(new Song(){SongID = id, SongTitle = title, SongTimestamp = timeadded, Deleted = deleted, Favorited = favorite});
            }
            rdr.Close();

            // foreach(Song song in songs)
            // {
            //     Console.WriteLine(song);
            // }
            return songs;
        }
        public Song GetOne(int id)
        {
            Song mySong = new Song();
            return mySong;
        }

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

        public void ChangeDatabase(){
            ConnectionString myConnection = new ConnectionString();
            string cs = myConnection.cs;
            using var con = new MySqlConnection(cs);
            con.Open();
            //deletes song table if it exists
            string drop = @"DROP TABLE if exists songs";

            using var cmd = new MySqlCommand(drop,con);
            cmd.ExecuteNonQuery();

            ReadFromDatabase.CreateSongTable();

            
            Song a = new Song(){SongID = 123, SongTitle = "Grenade", SongTimestamp = DateTime.Now, Deleted = "n", Favorited = "n"};
            Song b = new Song(){SongID = 1234, SongTitle = "7 rings", SongTimestamp = DateTime.Now, Deleted = "n", Favorited = "n"};

            WriteToDatabase w = new WriteToDatabase();
            w.Create(a);
            w.Create(b);
        }
    }
}
