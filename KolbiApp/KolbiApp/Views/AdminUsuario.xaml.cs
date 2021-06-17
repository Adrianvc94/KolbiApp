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
    public partial class AdminUsuario : ContentPage
    {
        public AdminUsuario()
        {
            InitializeComponent();
            btnGoBack.Clicked += BtnGoBack_Clicked;
        }

        private void BtnGoBack_Clicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("..");
        }
    }
}