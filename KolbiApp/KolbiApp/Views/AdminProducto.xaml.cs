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
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminProducto : ContentPage
    {
        public AdminProducto()
        {
            InitializeComponent();
            btnAgregar.Clicked += BtnAgregar_Clicked;
            btnEliminar.Clicked += BtnEliminar_Clicked;
            btnGoBack.Clicked += BtnGoBack_Clicked;
        }

        private void BtnGoBack_Clicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("..");
        }

        private async void BtnEliminar_Clicked(object sender, EventArgs e)
        {
            if (txtNombre.Text == null || txtPrecio.Text == null ||
                txtNombre.Text.Equals(" ") || txtPrecio.Text.Equals(" "))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debe ingresar el nombre y el precio", "Ok");
            }
            else
            {
                await ProductoService.RemoveProducto(txtNombre.Text, txtPrecio.Text);

                await Application.Current.MainPage.DisplayAlert("", "Producto eliminado", "Ok");
            }
        }

        private async void BtnAgregar_Clicked(object sender, EventArgs e)
        {
            if (txtNombre.Text == null || txtPrecio.Text == null || txtImagen.Text == null ||
                txtNombre.Text.Equals(" ") || txtPrecio.Text.Equals(" ") || txtImagen.Text.Equals(" "))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debe ingresar todos los datos", "Ok");
            }
            else
            {
                 await ProductoService.AddProducto(txtNombre.Text, txtPrecio.Text, txtImagen.Text);

                await Application.Current.MainPage.DisplayAlert("", "Producto agregado", "Ok");
            }
        }


    }
}