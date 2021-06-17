using KolbiApp.Modelos;
using KolbiApp.Services;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Command = MvvmHelpers.Commands.Command;

namespace KolbiApp.ViewModels
{
    public class UsuarioViewModel : ViewModelBase
    {
        public ObservableRangeCollection<Usuario> Usuario { get; set; }
        public ObservableRangeCollection<Grouping<string, Usuario>> UsuarioGroup { get; }

        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand AddCommand { get; }
        public AsyncCommand<Usuario> RemoveCommand { get; }
        public AsyncCommand<object> SelectedCommand { get; }

        public UsuarioViewModel()
        {

            Title = "Usuarios";

            Usuario = new ObservableRangeCollection<Usuario>();
            Load();

            UsuarioGroup = new ObservableRangeCollection<Grouping<string, Usuario>>();


            RefreshCommand = new AsyncCommand(Refresh);
            SelectedCommand = new AsyncCommand<object>(Selected);
            AddCommand = new AsyncCommand(Add);
            RemoveCommand = new AsyncCommand<Usuario>(Remove);

      
        }

        Usuario selectedUsuario;
        public Usuario SelectedUsuario
        {
            get => selectedUsuario;
            set => SetProperty(ref selectedUsuario, value);
        }

        async Task Selected(object args)
        {
            var usuario = args as Usuario;
            if (usuario == null)
                return;

            SelectedUsuario = null;

            await Application.Current.MainPage.DisplayAlert("Selected", usuario.Username, "OK");

        }

        async Task Add()
        {
            //var name = await App.Current.MainPage.DisplayPromptAsync("Name", "Name");
            //var email = await App.Current.MainPage.DisplayPromptAsync("Email", "Email");
            //var password = await App.Current.MainPage.DisplayPromptAsync("Password", "Password");
            //await UsuarioService.AddUsuario(name, email, password);
            //await Refresh();
        }

        async Task Remove(Usuario usuario)
        {
            await UsuarioService.RemoveUsuario(usuario.Id);
            await Refresh();
        }



        async Task Refresh()
        {
            IsBusy = true;

            await Task.Delay(2000);

            Usuario.Clear();

            var usuarios = await UsuarioService.GetAllUsuarios();

            Usuario.AddRange(usuarios);

            IsBusy = false;
        }

        public async Task Load() 
        {
            var usuarios = await UsuarioService.GetAllUsuarios();

            Usuario.AddRange(usuarios);
        }

    }

}
