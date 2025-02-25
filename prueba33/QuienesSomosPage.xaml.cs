using prueba33.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace prueba33
{
    public partial class QuienesSomosPage : ContentPage
    {
        public QuienesSomosPage()
        {
            InitializeComponent();
        }

        private void OnLogoClicked(object sender, EventArgs e)
        {
            // Toggle visibility of the side menu
            SideMenu.IsVisible = !SideMenu.IsVisible;
        }

        private async void OnInventarioTapped(object sender, EventArgs e)
        {
            // Navigate to the InventarioPage
            await Navigation.PushAsync(new InventarioPage());
            // Hide the side menu after navigation
            SideMenu.IsVisible = false;
        }

        private async void OnCalculadoraPreciosTapped(object sender, EventArgs e)
        {
            // Navegar a la página Calculadora de Precios
            await Navigation.PushAsync(new CalculadoradepreciosPage());
            // Ocultar el menú lateral después de la navegación
            SideMenu.IsVisible = false;
        }

        private async void OnRegistroGananciaTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegistroGananciaPage());

            SideMenu.IsVisible = false;
        }
    }
}
