using KolbiApp.Modelos;
using KolbiApp.Services;
using KolbiApp.Views;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KolbiApp.ViewModels
{
    public class ProductoViewModel : ViewModelBase
    {
        
        public ObservableRangeCollection<Productos> Producto { get; set; }
        public ObservableRangeCollection<Grouping<string, Productos>> ProductoGroup { get; }

        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand<Productos> RemoveCommand { get; }
        public AsyncCommand<Productos> SelectedCommand { get; }
        public AsyncCommand<Productos> CambiarActivoCommand { get; }
        public AsyncCommand<Productos> CambiarCanceladoCommand { get; }

        public ProductoViewModel()
        {

            Title = "Productos";

            Producto = new ObservableRangeCollection<Productos>();

            Load();

            ProductoGroup = new ObservableRangeCollection<Grouping<string, Productos>>();

            RefreshCommand = new AsyncCommand(Refresh);
            SelectedCommand = new AsyncCommand<Productos>(Selected);
            RemoveCommand = new AsyncCommand<Productos>(Remove);
            CambiarActivoCommand = new AsyncCommand<Productos>(Activo);
            CambiarCanceladoCommand = new AsyncCommand<Productos>(Cancelado);
        }

        Productos selectedProducto;
        public Productos SelectedProducto
        {
            get => selectedProducto;
            set => SetProperty(ref selectedProducto, value);
        }

        async Task Selected(Productos producto)
        {
            if (producto == null)
                return;
            var route = $"{nameof(ProductDetails)}?ProductId={producto.Id}";
            await Shell.Current.GoToAsync(route);
        }

        async Task Activo(Productos producto)
        {
            if (producto == null)
                return;
            var route = $"{nameof(Reportes)}?ProductId={producto.Id}";
            await Shell.Current.GoToAsync(route);
        }

        async Task Cancelado(Productos producto)
        {
            if (producto == null)
                return;
            var route = $"{nameof(Reportes)}?ProductId={producto.Id}";
            await Shell.Current.GoToAsync(route);
        }

        async Task Remove(Productos productos)
        {
            await ProductoService.RemoveProductoID(productos.Id);
            await Refresh();
        }

        async Task Refresh()
        {
            IsBusy = true;

            await Task.Delay(500);

            Producto.Clear();


            var productos = await ProductoService.GetAllProductos();

            Producto.AddRange(productos);

            IsBusy = false;
        }

        async Task Load()
        {
            var productos = await ProductoService.GetAllProductos();

            Producto.AddRange(productos);
        }

        async Task EstadoActivo(Productos producto)
        {
            var productos = await ProductoService.GetAllProductos();

            Producto.AddRange(productos);
        }
    }
}
