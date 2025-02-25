using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using prueba33.Models;

namespace prueba33.Data
{
    public class UsuarioDatabase
    {
        private readonly SQLiteAsyncConnection _database;

        public UsuarioDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Usuario>().Wait();
        }

        public Task<List<Usuario>> GetUsuariosAsync()
        {
            return _database.Table<Usuario>().ToListAsync();
        }

        public Task<int> SaveUsuarioAsync(Usuario usuario)
        {
            if (usuario.Id != 0)
            {
                return _database.UpdateAsync(usuario);
            }
            else
            {
                return _database.InsertAsync(usuario);
            }
        }

        public Task<int> DeleteUsuarioAsync(Usuario usuario)
        {
            return _database.DeleteAsync(usuario);
        }

        public Task<Usuario> GetUsuarioAsync(string nombreUsuario)
        {
            return _database.Table<Usuario>().Where(i => i.NombreUsuario == nombreUsuario).FirstOrDefaultAsync();
        }
    }
}

