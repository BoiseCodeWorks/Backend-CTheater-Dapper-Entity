using CTheater.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CTheater.Repositories
{
    public class TheaterRepository
    {
        private readonly IDbConnection _db;

        public TheaterRepository(IDbConnection db)
        {
            _db = db;
        }

        public IEnumerable<Movie> GetAll()
        {
            return _db.Query<Movie>("SELECT * from movies");
        }

        public Movie GetOne(string id)
        {
            return _db.QueryFirstOrDefault<Movie>($"SELECT * FROM movies WHERE id = @id", new { id });
        }

        public Movie Add(Movie movie)
        {
            string Id = Guid.NewGuid().ToString();
            int id = _db.ExecuteScalar<int>(@"
                INSERT INTO movies (Id, Title, Description)
                VALUES(@Id, @Title, @Description);
                SELECT LAST_INSERT_ID()",
                new
                {
                    Id,
                    movie.Title,
                    movie.Description
                });

            return new Movie()
            {
                Id = Id,
                Title = movie.Title,
                Description = movie.Description
            };
        }

        public Movie GetOneByIdAndUpdate(string id, Movie movie)
        {
            movie.Id = id;
            return _db.QueryFirstOrDefault<Movie>($@"
                UPDATE movies SET
                Title = @Title,
                Description = @Description
                WHERE Id = @id;
                SELECT * FROM movies WHERE Id = @id", movie);
        }

        public string FindByIdAndRemove(string id)
        {
            var success = _db.Execute(@"
            DELETE FROM movies WHERE Id = @id", new { id });
            return success > 0 ? "Successfully deleted!" : "Couldn't delete that";
        }
    }
}
