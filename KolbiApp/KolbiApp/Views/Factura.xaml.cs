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
    public partial class Factura : ContentPage
    {
        static ObservableCollection<Productos> productos;
        static List<int> ValidarArticulos = new List<int>();
        static int total = 0;

        public Factura()
        {
            InitializeComponent();
            btnMarket.Clicked += BtnMarket_Clicked;

            if (productos == null)
                productos = new ObservableCollection<Productos>();

            ArticulosCarrito();
        }

        private void BtnMarket_Clicked(object sender, EventArgs e)
        {
            var route = $"{nameof(Market)}";
            Shell.Current.GoToAsync(route);
        }

        protected override void OnAppearing()
        {
            ProductosView.ItemsSource = productos;
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
                    total += Convert.ToInt32(articulo.Price);
                }

                if (articulo.Estado == "Cancelado")
                {
                    total -= Convert.ToInt32(articulo.Price);
                    Refresh();
                }
            }
            txtTotal.Text = "$" + Convert.ToString(total);
        }

        private void Refresh()
        {
            productos.Clear();
            RefreshArticulosCarrito();
        }

        public async void RefreshArticulosCarrito()
        {
            List<int> articulos = ProductoService.GetArtShoppingCart();

            foreach (int a in articulos)
            {
                Productos articulo = await ProductoService.GetIdProducto(a);
                if (articulo.Estado == "Cancelado")
                {
                    productos.Remove(articulo);
                    ValidarArticulos.Remove(articulo.Id);
                }
                if (articulo.Estado == "Activo")
                {
                    productos.Add(articulo);
                    if (!ValidarArticulos.Contains(articulo.Id))
                    {
                        ValidarArticulos.Add(articulo.Id);
                    }
                }
            }
        }

    }
}