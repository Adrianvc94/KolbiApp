using KolbiApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KolbiApp
{

    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Shell.SetNavBarIsVisible(this, false);

            Routing.RegisterRoute(nameof(SignUp),
                 typeof(SignUp));

            Routing.RegisterRoute(nameof(Login),
                 typeof(Login));

            Routing.RegisterRoute(nameof(Market),
                 typeof(Market));

            Routing.RegisterRoute(nameof(ProductDetails),
                 typeof(ProductDetails));

            Routing.RegisterRoute(nameof(ShoppingCart),
                typeof(ShoppingCart));

            Routing.RegisterRoute(nameof(Factura),
              typeof(Factura));

            Routing.RegisterRoute(nameof(Reportes),
             typeof(Reportes));

            Routing.RegisterRoute(nameof(PanelAdmin),
             typeof(PanelAdmin));

            Routing.RegisterRoute(nameof(ReportesAdmin),
            typeof(ReportesAdmin));

            Routing.RegisterRoute(nameof(AdminUsuario),
            typeof(AdminUsuario));

            Routing.RegisterRoute(nameof(AdminProducto),
            typeof(AdminProducto));
        }
    }
}