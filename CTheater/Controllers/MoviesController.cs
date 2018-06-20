using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CTheater.Models;
using CTheater.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CTheater.Controllers
{
    [Produces("application/json")]
    [Route("api/Movies")]
    public class MoviesController : Controller
    {
        private readonly TheaterRepository _repo;

        public MoviesController(TheaterRepository repo)
        {
            _repo = repo;
        }

        // GET: api/Movies
        [HttpGet]
        public IEnumerable<Movie> Get()
        {
            return _repo.GetAll();
        }

        // GET: api/Movies/5
        [HttpGet("{id}", Name = "Get")]
        public Movie Get(string id)
        {
            return _repo.GetOne(id);
        }
        
        // POST: api/Movies
        [HttpPost]
        public Movie Post([FromBody]Movie movie)
        {
            return _repo.Add(movie);
        }
        
        // PUT: api/Movies/5
        [HttpPut("{id}")]
        public Movie Put(string id, [FromBody]Movie movie)
        {
            return _repo.GetOneByIdAndUpdate(id, movie);
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public string Delete(string id)
        {
            return _repo.FindByIdAndRemove(id);
        }
    }
}
