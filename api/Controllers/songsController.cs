using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using mis321_pa4_kbtishler.api.Models.Interfaces;
using mis321_pa4_kbtishler.Models;
using repos.mis321_pa4_kbtishler.api.Database;
using Microsoft.AspNetCore.Cors;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class songsController : ControllerBase
    {
        // GET: api/songs
        [EnableCors("AnotherPolicy")]
        [HttpGet]
        public List<Song>Get()
        {
            ReadFromDatabase readObject = new ReadFromDatabase();
           // readObject.ChangeDatabase();
            return readObject.GetAll();
        }

        // GET: api/songs/5


        [EnableCors("AnotherPolicy")]
        [HttpGet("{id}", Name = "Get")]
        public Song Get(int id)
        {
            ReadFromDatabase readObject = new ReadFromDatabase();
            
            return readObject.GetOne(id);
        }

        // POST: api/songs
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public void Post([FromBody] Song value)
        {
            System.Console.WriteLine("made it to post");
            ICreateSongs insertObject = new WriteToDatabase();
            insertObject.Create(value);
        }

        // PUT: api/songs/5
        [EnableCors("AnotherPolicy")]
        [HttpPut]
        public void Put([FromBody] Song song)
        {
            IUpdateSongs updateSongs = new UpdateDatabase();
            updateSongs.Update(song);
        }

        // DELETE: api/songs/5
        [EnableCors("AnotherPolicy")]
        [HttpDelete]
        public void Delete([FromBody] Song song)
        {
            IDeleteSongs delete = new DeleteFromDatabase();
            delete.Delete(song);
        }
    }
}
