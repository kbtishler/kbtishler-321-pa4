using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using System;
using System.IO;
using System.Collections.Generic;
using mis321_pa4_kbtishler.api;
using mis321_pa4_kbtishler.Models;



namespace repos.mis321_pa4_kbtishler.api.Database
{
    public class ConnectionString
    {
        public string cs {get; set;}

        public ConnectionString()
        {
            string server = "ro2padgkirvcf55m.cbetxkdyhwsb.us-east-1.rds.amazonaws.com";
            string database = "rkif4ampzzqw3x0x";
            string port = "	3306";
            string userName = "	xzwvgvlh5k92mpd5";
            string pass = "snacghs2tkdyoj0t";

            cs = $@"server = {server};user={userName};database={database};port={port};password={pass};";

        }
    }
}