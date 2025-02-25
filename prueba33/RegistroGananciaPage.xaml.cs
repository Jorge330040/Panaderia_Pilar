using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace prueba33.DataBase
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistroGananciaPage : ContentPage
    {
        public RegistroGananciaPage()
        {
            InitializeComponent();
            CargarGanancias();
        }

        private async void CargarGanancias()
        {
            // Cargar todas las ganancias desde la base de datos y mostrarlas en la lista
            List<Ganancia> ganancias = await App.GananciaDatabase.GetGananciasAsync();
            gananciaListView.ItemsSource = ganancias;
        }

        private void OnCalcularTotalClicked(object sender, EventArgs e)
        {
            if (decimal.TryParse(precioEntry.Text, out decimal precio) &&
                int.TryParse(cantidadEntry.Text, out int cantidad))
            {
                decimal total = precio * cantidad;
                totalLabel.Text = $"Total: {total:F2}";
            }
            else
            {
                DisplayAlert("Error", "Ingrese valores válidos para el precio y la cantidad.", "OK");
            }
        }

        private async void OnGuardarRegistroClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(productoEntry.Text) ||
                !decimal.TryParse(precioEntry.Text, out decimal precio) ||
                !int.TryParse(cantidadEntry.Text, out int cantidad))
            {
                await DisplayAlert("Error", "Por favor, complete todos los campos correctamente.", "OK");
                return;
            }

            decimal total = precio * cantidad;

            var ganancia = new Ganancia
            {
                Producto = productoEntry.Text,
                PrecioPorUnidad = precio,
                CantidadVendida = cantidad,
                Total = total,
                Fecha = fechaPicker.Date
            };

            await App.GananciaDatabase.SaveGananciaAsync(ganancia);

            await DisplayAlert("Éxito", "Registro guardado exitosamente.", "OK");

            // Limpiar los campos después de guardar
            productoEntry.Text = string.Empty;
            precioEntry.Text = string.Empty;
            cantidadEntry.Text = string.Empty;
            totalLabel.Text = "Total: 0.00";

            // Recargar la lista de ganancias para mostrar el nuevo registro
            CargarGanancias();
        }

        private async void OnEliminarUltimoClicked(object sender, EventArgs e)
        {
            var ganancias = await App.GananciaDatabase.GetGananciasAsync();
            var ultimoGanancia = ganancias.LastOrDefault();

            if (ultimoGanancia != null)
            {
                bool confirm = await DisplayAlert("Confirmar", $"¿Estás seguro de que deseas eliminar el registro de {ultimoGanancia.Producto}?", "Sí", "No");
                if (confirm)
                {
                    // Eliminar el último registro de la base de datos
                    await App.GananciaDatabase.DeleteGananciaAsync(ultimoGanancia);

                    // Refrescar la lista
                    CargarGanancias(); // Asegúrate de tener un método que recargue los datos en la vista
                }
            }
        }
    }
}