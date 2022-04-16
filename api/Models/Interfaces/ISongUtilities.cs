using System.Collections.Generic;
using mis321_pa4_kbtishler.Models;

namespace mis321_pa4_kbtishler.api.Models.Interfaces
{
    public interface ISongUtilities
    {
         public List<Song> playlist { get; set; }
         public void AddSong();
         public void DeleteSong();
         public void EditSong();
         public void PrintPlaylist();
    }
}