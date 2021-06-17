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
    public partial class PanelAdmin : ContentPage
    {
        public PanelAdmin()
        {
            InitializeComponent();

            btnReportes.Clicked += BtnReportes_Clicked;
            btnUsuarios.Clicked += BtnUsuarios_Clicked;
            btnProductos.Clicked += BtnProductos_Clicked;
            btnLogOut.Clicked += BtnLogOut_Clicked;
        }

        private void BtnLogOut_Clicked(object sender, EventArgs e)
        {
            var route = $"{nameof(SignUp)}";
            Shell.Current.GoToAsync(route);
        }

        private void BtnProductos_Clicked(object sender, EventArgs e)
        {
            var route = $"{nameof(AdminProducto)}";
            Shell.Current.GoToAsync(route);
        }

        private void BtnUsuarios_Clicked(object sender, EventArgs e)
        {
            var route = $"{nameof(AdminUsuario)}";
            Shell.Current.GoToAsync(route);
        }

        private void BtnReportes_Clicked(object sender, EventArgs e)
        {
            var route = $"{nameof(ReportesAdmin)}";
            Shell.Current.GoToAsync(route);
        }
    }
}