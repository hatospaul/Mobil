using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Threading.Tasks;
using Mobil.Models;

namespace Mobil.Data
{
    public class RentalListDatabase
    {
        readonly SQLiteAsyncConnection _database;
        public RentalListDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<RentalList>().Wait();
            _database.CreateTableAsync<Movie>().Wait();
            _database.CreateTableAsync<ListMovie>().Wait();
        }
        public Task<List<RentalList>> GetRentalListsAsync()
        {
            return _database.Table<RentalList>().ToListAsync();
        }
        public Task<RentalList> GetRentalListAsync(int id)
        {
            return _database.Table<RentalList>()
            .Where(i => i.ID == id)
           .FirstOrDefaultAsync();
        }
        public Task<int> SaveRentalListAsync(RentalList rlist)
        {
            if (rlist.ID != 0)
            {
                return _database.UpdateAsync(rlist);
            }
            else
            {
                return _database.InsertAsync(rlist);
            }
        }
        public Task<int> DeleteRentalListAsync(RentalList rlist)
        {
            return _database.DeleteAsync(rlist);
        }

        public Task<int> SaveMovieAsync(Movie movie)
        {
            if (movie.ID != 0)
            {
                return _database.UpdateAsync(movie);
            }
            else
            {
                return _database.InsertAsync(movie);
            }
        }
        public Task<int> DeleteMovieAsync(Movie movie)
        {
            return _database.DeleteAsync(movie);
        }
        public Task<List<Movie>> GetMoviesAsync()
        {
            return _database.Table<Movie>().ToListAsync();
        }

        public Task<int> SaveListMovieAsync(ListMovie listm)
        {
            if (listm.ID != 0)
            {
                return _database.UpdateAsync(listm);
            }
            else
            {
                return _database.InsertAsync(listm);
            }
        }
        public Task<List<Movie>> GetListMoviesAsync(int rentallistid)
        {
            return _database.QueryAsync<Movie>(
            "select M.ID, M.Description from Movie M"
            + " inner join ListMovie LM"
            + " on M.ID = LM.MovieID where LM.RentalListID = ?",
            rentallistid);
        }
    }
}

