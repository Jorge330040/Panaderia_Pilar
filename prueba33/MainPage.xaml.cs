using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials; // Importar Xamarin.Essentials

namespace prueba33
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            string username = usernameEntry.Text;
            string password = passwordEntry.Text;

            var user = await App.UsuarioDatabase.GetUsuarioAsync(username);

            if (user != null && user.Contraseña == password)
            {
                await Navigation.PushAsync(new QuienesSomosPage());
            }
            else
            {
                await DisplayAlert("Error", "Usuario o contraseña incorrectos.", "OK");
            }
        }

        private async void OnRegisterTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }

        private async void OnForgotPasswordTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RecoverPasswordPage());
        }

        private async void OnYouTubeButtonClicked(object sender, EventArgs e)
        {
            var uri = new Uri("https://www.youtube.com/@codickyt2661");
            await Launcher.OpenAsync(uri);
        }
    }
}