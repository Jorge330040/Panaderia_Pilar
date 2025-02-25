using prueba33.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace prueba33
{
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private async void OnRegisterButtonClicked(object sender, EventArgs e)
        {
            if (!termsCheckBox.IsChecked)
            {
                await DisplayAlert("Error", "Debe aceptar los Términos de Servicio y la Política de Privacidad.", "OK");
                return;
            }

            string username = usernameEntry.Text?.Trim();
            string password = passwordEntry.Text?.Trim();
            string favoriteColor = favoriteColorEntry.Text?.Trim();

            // Validar que los campos no estén vacíos
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(favoriteColor))
            {
                await DisplayAlert("Error", "Todos los campos son obligatorios.", "OK");
                return;
            }

            var user = new Usuario
            {
                NombreUsuario = username,
                Contraseña = password,
                ColorFavorito = favoriteColor
            };

            // Guardar el usuario en la base de datos
            await App.UsuarioDatabase.SaveUsuarioAsync(user);

            await DisplayAlert("Registro", "Registro exitoso", "OK");
            await Navigation.PopAsync(); // Vuelve a la página anterior
        }
    }
}
