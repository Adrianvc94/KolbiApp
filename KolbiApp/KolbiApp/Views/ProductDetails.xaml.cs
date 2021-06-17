using KolbiApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KolbiApp.Views
{
    [QueryProperty(nameof(ProductId), nameof(ProductId))]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductDetails : ContentPage
    {
        public string ProductId { get;set; }
        
        public ProductDetails()
        {
            InitializeComponent();
            btnBack.Clicked += BtnBack_Clicked;
            btnCarrito.Clicked += BtnCarrito_Clicked;
        }

        private async void BtnCarrito_Clicked(object sender, EventArgs e)
        {

            int.TryParse(ProductId, out var result);
            ProductoService.AddArtShoppingCart(result);
            await ProductoService.UpdateProducto(result, CHPlan.SelectedItem.ToString(), CHMemo.SelectedItem.ToString(), CHColor.SelectedItem.ToString(), DateTime.Now, "Activo");
        }

        private void BtnBack_Clicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("..");
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            int.TryParse(ProductId, out var result);

            BindingContext = await ProductoService.GetIdProducto(result);
        }
    }
}