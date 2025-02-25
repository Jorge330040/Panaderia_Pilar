using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace prueba33.Data
{
    public class GananciaDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public GananciaDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Ganancia>().Wait();
        }

        public Task<List<Ganancia>> GetGananciasAsync()
        {
            return _database.Table<Ganancia>().ToListAsync();
        }

        public Task<int> SaveGananciaAsync(Ganancia ganancia)
        {
            if (ganancia.Id != 0)
            {
                return _database.UpdateAsync(ganancia);
            }
            else
            {
                return _database.InsertAsync(ganancia);
            }
        }

        public Task<int> DeleteGananciaAsync(Ganancia ganancia)
        {
            return _database.DeleteAsync(ganancia);
        }
    }
}
