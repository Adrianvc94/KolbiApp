using KolbiApp.Modelos;
using KolbiApp.Views;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace KolbiApp.Services
{
    public static class UsuarioService
    {
        static SQLiteAsyncConnection db;

        static async Task Init()
        {
            if (db != null)
                return;

            // Get an absolute path to the database file
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "MyData.db");

            db = new SQLiteAsyncConnection(databasePath);

            await db.CreateTableAsync<Usuario>();
        }

        public static async Task AddUsuario(string name, string email, string password)
        {
            await Init();
            var usuario = new Usuario
            {
                Username = name,
                Email = email,
                Password = password
            };

            await db.InsertAsync(usuario);
        }

        public static async Task AddAdmin()
        {
            await Init();
            var admin = new Usuario
            {
                Username = "admin",
                Email = "admin",
                Password = "admin",
                Admin = "Y"
            };

            await db.InsertAsync(admin);
        }

        public static async Task RemoveUsuario(int id)
        {
            await Init();

            await db.DeleteAsync<Usuario>(id);
        }

        public static async Task<IEnumerable<Usuario>> GetAllUsuarios()
        {
            await Init();

            return await db.QueryAsync<Usuario>("select * from Usuario");
        }

        public static async Task<Usuario> GetIdUsuario(int id)
        {
            await Init();

            var usuario = await db.Table<Usuario>()
                .FirstOrDefaultAsync(c => c.Id == id);

            return usuario;
        }

        public static async Task ValidarUsuario(string name, string password)
        {
            await Init();

            var validar = await db.Table<Usuario>()
                            .FirstOrDefaultAsync(c => c.Username == name && c.Password == password);

            if (validar != null)
                if (validar.Admin == "Y")
                {
                    var route = $"{nameof(PanelAdmin)}";
                    await Shell.Current.GoToAsync(route);
                }
                else
                {
                    var route = $"{nameof(Market)}";
                    await Shell.Current.GoToAsync(route);
                }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Datos incorrectos", "OK");
            }
               
        }
    }
}
