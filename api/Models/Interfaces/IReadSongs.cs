using System.Collections.Generic;
using mis321_pa4_kbtishler.Models;

namespace mis321_pa4_kbtishler.api.Models.Interfaces
{
    public interface IReadSongs
    {
        public List<Song> GetAll();
        public Song GetOne(int id);
         
    }
}