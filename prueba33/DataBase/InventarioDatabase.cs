using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using prueba33.Models; 

namespace prueba33.Data
{
    public class InventarioDatabase
    {
        private readonly SQLiteAsyncConnection _database;

        public InventarioDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Inventario>().Wait();
        }

        public Task<List<Inventario>> GetInventariosAsync()
        {
            return _database.Table<Inventario>().ToListAsync();
        }

        public Task<int> SaveInventarioAsync(Inventario inventario)
        {
            if (inventario.Id != 0)
            {
                return _database.UpdateAsync(inventario);
            }
            else
            {
                return _database.InsertAsync(inventario);
            }
        }

        public Task<int> DeleteInventarioAsync(Inventario inventario)
        {
            return _database.DeleteAsync(inventario);
        }

        public Task<int> DeleteAllInventariosAsync()
        {
            return _database.DeleteAllAsync<Inventario>();
        }
    }
}
