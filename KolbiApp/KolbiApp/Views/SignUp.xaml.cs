using KolbiApp.Modelos;
using KolbiApp.Services;
using MvvmHelpers;
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
    public partial class SignUp : ContentPage
    {
        public ObservableRangeCollection<Usuario> Usuarios { get; set; }

        public SignUp()
        {           
            InitializeComponent();

            Shell.SetNavBarIsVisible(this, false);
            ValidarAdmin();
            btnIrLogin.Clicked += BtnIrLogin_Clicked;
            btnSignUp.Clicked += BtnSignUp_Clicked;          
        }

        public async void ValidarAdmin() 
        {
            var usuarios = await UsuarioService.GetAllUsuarios();

            var validarAdmin = usuarios.FirstOrDefault(c => c.Admin == "Y");

            if (validarAdmin == null) 
            {
                await UsuarioService.AddAdmin();
            }           
        }

        private async void BtnSignUp_Clicked(object sender, EventArgs e)
        {
            if (name.Text == null || email.Text == null || pwd.Text == null ||
                name.Text.Equals(" ") || email.Text.Equals(" ") || pwd.Text.Equals(" "))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debe ingresar los datos", "Ok");
            }
            else
            {
                await UsuarioService.AddUsuario(name.Text, email.Text, pwd.Text);
                var route = $"{nameof(Login)}";
                await Shell.Current.GoToAsync(route);
            }                       
        }

        private void BtnIrLogin_Clicked(object sender, EventArgs e)
        {
            var route = $"{nameof(Login)}";
            Shell.Current.GoToAsync(route);
        }
    }
}