using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace prueba33
{
    public partial class RecoverPasswordPage : ContentPage
    {
        public RecoverPasswordPage()
        {
            InitializeComponent();
        }

        private async void OnRecoverPasswordButtonClicked(object sender, EventArgs e)
        {
            string username = usernameEntry.Text; // Nombre de usuario ingresado
            string favoriteColor = favoriteColorEntry.Text; // Color favorito ingresado

            // Cambiar de App.Database a App.UsuarioDatabase para obtener la base de datos de usuarios
            var user = await App.UsuarioDatabase.GetUsuarioAsync(username);

            if (user != null && user.ColorFavorito.Equals(favoriteColor, StringComparison.OrdinalIgnoreCase))
            {
                // Si el usuario existe y el color coincide, mostrar la contraseña
                await DisplayAlert("Recuperación de contraseña", $"Su contraseña es: {user.Contraseña}", "OK");
            }
            else
            {
                // Si el usuario no existe o el color no coincide, mostrar error
                await DisplayAlert("Error", "Usuario o color favorito incorrecto.", "OK");
            }
        }
    }
}