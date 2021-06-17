using KolbiApp.Modelos;
using KolbiApp.Services;
using KolbiApp.ViewModels;
using MvvmHelpers;
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
    public partial class Market : ContentPage
    {
        static ObservableRangeCollection<Productos> productos;

        public Market()
        {
            InitializeComponent();
            btnCarrito.Clicked += BtnCarrito_Clicked;
            btnReportes.Clicked += BtnReportes_Clicked;
            btnLogOut.Clicked += BtnLogOut_Clicked;
            ValidarProdcutos();

            if (productos == null)
                productos = new ObservableRangeCollection<Productos>();
 }

        public async void ValidarProdcutos()
        {
            var productos = await ProductoService.GetAllProductos();

            if (productos.Count() == 0)
            {
                await ProductoService.AddProducto("iPhone 12", "900", "https://static3.abc.es/media/tecnologia/2020/12/14/pro-max-keZG--620x349@abc.jpg");
                await ProductoService.AddProducto("Huawei p40 pro", "700", "https://consumer.huawei.com/content/dam/huawei-cbg-site/common/mkt/list-image/phones/p40-pro/p40-pro-silver.png");
                await ProductoService.AddProducto("Xiaomi Redmi Note 8", "600", "https://m.media-amazon.com/images/I/41luj67K-ZL.jpg");
                await ProductoService.AddProducto("LG K8", "800", "https://www.lg.com/ar/images/celulares/md05753288/gallery/medium01.jpg");
                await ProductoService.AddProducto("Galaxy Note 20 Ultra", "500", "https://tiendasamsung.cr/media/catalog/product/cache/fd79ca39c172cf5a18782c64c6340a1c/s/a/samsung_galaxy_note_20_ultra_256gb_8gb_android_10q_negro_08.jpg");
                await ProductoService.AddProducto("Huawei mate 30 pro", "500", "https://img01.huaweifile.com/sg/ms/co/pms/product/6901443366187/428_428_F08AF880788609025E490D22FCE64E093CFF2F49E10151E4mp.png");
            }
        }

        private void BtnLogOut_Clicked(object sender, EventArgs e)
        {
            var route = $"{nameof(SignUp)}";
            Shell.Current.GoToAsync(route);
        }

        private void BtnReportes_Clicked(object sender, EventArgs e)
        {
            var route = $"{nameof(Reportes)}";
            Shell.Current.GoToAsync(route);
        }

        private void BtnCarrito_Clicked(object sender, EventArgs e)
        {
            var route = $"{nameof(ShoppingCart)}";
            Shell.Current.GoToAsync(route);
        }

        private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {

            var resultSearch = await ProductoService.GetAllProductos();


            var resultado = resultSearch.Where(c => c.Name.ToLower().Contains(SearchBar.Text.ToLower()));

            if (resultado == null)
            {
                productos.Clear();
                productos.AddRange(resultSearch);
                ProductosView.ItemsSource = productos;
            }
            else
            {
                ProductosView.ItemsSource = resultado;
                resultado = null;
            } 

        }
    }
}