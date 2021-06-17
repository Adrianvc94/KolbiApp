using KolbiApp.Modelos;
using KolbiApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KolbiApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReportesAdmin : ContentPage
    {
        static ObservableCollection<Productos> productos;
        static List<int> ValidarArticulos = new List<int>();

        public ReportesAdmin()
        {

            InitializeComponent();
            btnactivas.Clicked += Btnactivas_Clicked;
            btncanceladas.Clicked += Btncanceladas_Clicked;
            btntotales.Clicked += Btntotales_Clicked;
            btnGoBack.Clicked += BtnGoBack_Clicked;

            if (productos == null)
                productos = new ObservableCollection<Productos>();

            ArticulosCarrito();
        }

        protected override void OnAppearing()
        {
            ProductosView.ItemsSource = productos;
        }

        private void BtnGoBack_Clicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("..");
        }

        private void Btnactivas_Clicked(object sender, EventArgs e)
        {
            RefreshActivos();
        }

        private void Btncanceladas_Clicked(object sender, EventArgs e)
        {
            RefreshCancelados();
        }

        private void Btntotales_Clicked(object sender, EventArgs e)
        {
            Refresh();
        }

        public async void RefreshActivos()
        {
            productos.Clear();

            List<int> articulos = ProductoService.GetArtShoppingCart();

            foreach (int a in articulos)
            {
                Productos articulo = await ProductoService.GetIdProducto(a);
                if (articulo.Estado == "Activo")
                {
                    productos.Add(articulo);
                }
            }
        }

        public async void RefreshCancelados()
        {
            productos.Clear();

            List<int> articulos = ProductoService.GetArtShoppingCart();

            foreach (int a in articulos)
            {
                Productos articulo = await ProductoService.GetIdProducto(a);
                if (articulo.Estado == "Cancelado")
                {
                    productos.Add(articulo);
                }
            }
        }



        public async void ArticulosCarrito()
        {
            List<int> articulos = ProductoService.GetArtShoppingCart();

            foreach (int a in articulos)
            {
                Productos articulo = await ProductoService.GetIdProducto(a);
                if (!ValidarArticulos.Contains(articulo.Id))
                {
                    productos.Add(articulo);

                    ValidarArticulos.Add(articulo.Id);
                }
            }

        }

        public async void RefreshArticulosCarrito()
        {
            List<int> articulos = ProductoService.GetArtShoppingCart();

            foreach (int a in articulos)
            {
                Productos articulo = await ProductoService.GetIdProducto(a);
                productos.Add(articulo);           
            }
        }

        private async void Btncancela_Clicked(object sender, EventArgs e)
        {
            var productoParametro = (Button)sender;
            var producto = (Productos)productoParametro.CommandParameter;
            await ProductoService.EstadoCancelado(producto.Id);

            Refresh();
        }

        private async void Btnactiva_Clicked(object sender, EventArgs e)
        {
            var productoParametro = (Button)sender;
            var producto = (Productos)productoParametro.CommandParameter;
            await ProductoService.EstadoActivo(producto.Id);

            Refresh();
        }

        private void Refresh()
        {
            productos.Clear();
            RefreshArticulosCarrito();
        }
    }
}