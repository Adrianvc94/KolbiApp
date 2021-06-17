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
    public static class ProductoService
    {
        static SQLiteAsyncConnection db;
        static List<int> articulos = new List<int>();

        static async Task Init()
        {
            if (db != null)
            {
                return;
            }

            // Get an absolute path to the database file
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "MyData.db");

            db = new SQLiteAsyncConnection(databasePath);

            await db.CreateTableAsync<Productos>();
        }

        public static async Task AddProducto(string name, string price, string image)
        {
            await Init();
            var producto = new Productos
            {
                Name = name,
                Price = price,
                Imagen = image,
                Plan = "",
                Memoria = "",
                Color = "",
                Fecha = DateTime.Now,
                Estado = ""
            };

            await db.InsertAsync(producto);

        }

        public static async Task UpdateProducto(int id, string plan, string memoria, string color, DateTime fecha, string estado)
        {
            await Init();

            Productos p = await GetIdProducto(id);

            var producto = new Productos
            {
                Id = id,
                Name = p.Name,
                Price = p.Price,
                Imagen = p.Imagen,
                Plan = plan,
                Memoria = memoria,
                Color = color,
                Fecha = fecha,
                Estado = estado
            };

            await db.UpdateAsync(producto);
        }

        public static async Task EstadoActivo(int id)
        {
            await Init();

            Productos p = await GetIdProducto(id);

            var producto = new Productos
            {
                Id = id,
                Name = p.Name,
                Price = p.Price,
                Imagen = p.Imagen,
                Plan = p.Plan,
                Memoria = p.Memoria,
                Color = p.Color,
                Fecha = p.Fecha,
                Estado = "Activo"
            };

            await db.UpdateAsync(producto);
        }

        public static async Task EstadoCancelado(int id)
        {
            await Init();

            Productos p = await GetIdProducto(id);

            var producto = new Productos
            {
                Id = id,
                Name = p.Name,
                Price = p.Price,
                Imagen = p.Imagen,
                Plan = p.Plan,
                Memoria = p.Memoria,
                Color = p.Color,
                Fecha = p.Fecha,
                Estado = "Cancelado"
            };

            await db.UpdateAsync(producto);
        }

        public static async Task RemoveProductoID(int id)
        {
            await Init();

            await db.DeleteAsync<Productos>(id);
        }

        public static async Task RemoveProducto(string name, string price)
        {
            await Init();

            var producto = await db.Table<Productos>()
                .FirstOrDefaultAsync(c => c.Name == name && c.Price == price);

            await db.DeleteAsync<Productos>(producto.Id);
        }

        public static async Task<IEnumerable<Productos>> GetAllProductos()
        {
            await Init();

            var productos = await db.Table<Productos>().ToListAsync();
            return productos;
        }

        public static async Task<Productos> GetIdProducto(int id)
        {
            await Init();

            var producto = await db.Table<Productos>()
                .FirstOrDefaultAsync(c => c.Id == id);

            return producto;
        }

        public static void AddArtShoppingCart(int id)
        {
            articulos.Add(id);
        }

        public static List<int> GetArtShoppingCart()
        {
            return articulos;
        }
    }
}
