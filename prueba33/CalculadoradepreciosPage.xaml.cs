using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace prueba33
{
    public partial class CalculadoradepreciosPage : ContentPage
    {
        private List<Ingrediente> ingredientes;

        public CalculadoradepreciosPage()
        {
            InitializeComponent();
            ingredientes = new List<Ingrediente>();
        }

        private void OnAddIngredienteClicked(object sender, EventArgs e)
        {
            var grid = new Grid
            {
                ColumnSpacing = 10,
                RowSpacing = 5,
                BackgroundColor = Color.FromHex("#FAFAFA"),
                Padding = new Thickness(5),
                ColumnDefinitions =
        {
            new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }, // Columna 1
            new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }, // Columna 2
            new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }  // Columna 3
        }
            };

            var nombreEntry = new Entry
            {
                Placeholder = "Nombre",
                TextColor = Color.FromHex("#000000"),
                BackgroundColor = Color.FromHex("#FFFFFF"),
                WidthRequest = 100
            };

            var cantidadEntry = new Entry
            {
                Placeholder = "Cantidad utilizada",
                Keyboard = Keyboard.Numeric,
                TextColor = Color.FromHex("#000000"),
                BackgroundColor = Color.FromHex("#FFFFFF"),
                WidthRequest = 100
            };

            var precioEntry = new Entry
            {
                Placeholder = "Precio por unidad",
                Keyboard = Keyboard.Numeric,
                TextColor = Color.FromHex("#000000"),
                BackgroundColor = Color.FromHex("#FFFFFF"),
                WidthRequest = 100
            };

            grid.Children.Add(nombreEntry, 0, 0);  // Añadir el nombre del ingrediente en la primera columna
            grid.Children.Add(cantidadEntry, 1, 0);  // Añadir la cantidad en la segunda columna
            grid.Children.Add(precioEntry, 2, 0);  // Añadir el precio en la tercera columna

            IngredientesContainer.Children.Add(grid);
            ingredientes.Add(new Ingrediente
            {
                NombreEntry = nombreEntry,
                CantidadEntry = cantidadEntry,
                PrecioEntry = precioEntry
            });
        }

        private void OnCalcularClicked(object sender, EventArgs e)
        {
            try
            {
                // Obtener los valores de los ingredientes
                decimal costoTotalIngredientes = 0;
                foreach (var ingrediente in ingredientes)
                {
                    if (decimal.TryParse(ingrediente.CantidadEntry.Text, out var cantidad) &&
                        decimal.TryParse(ingrediente.PrecioEntry.Text, out var precio))
                    {
                        costoTotalIngredientes += (cantidad * precio);
                    }
                }

                // Obtener los costos adicionales
                decimal.TryParse(CostosAdicionalesEntry.Text, out var costosAdicionales);

                // Obtener la cantidad de panes producidos
                int.TryParse(CantidadProducidaEntry.Text, out var cantidadProducida);

                // Obtener el porcentaje de ganancia
                decimal.TryParse(PorcentajeGananciaEntry.Text, out var porcentajeGanancia);

                // Calcular el costo total de producción
                var costoTotalProduccion = costoTotalIngredientes + costosAdicionales;

                // Calcular el precio por unidad sin ganancia
                var costoPorUnidad = costoTotalProduccion / cantidadProducida;

                // Calcular el precio por unidad con ganancia
                var precioPorUnidad = costoPorUnidad * (1 + (porcentajeGanancia / 100));

                // Mostrar el resultado
                ResultadoLabel.Text = $"Precio por unidad: {precioPorUnidad:F2}";
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", $"Ocurrió un error al calcular el precio: {ex.Message}", "OK");
            }
        }

        private class Ingrediente
        {
            public Entry NombreEntry { get; set; }
            public Entry CantidadEntry { get; set; }
            public Entry PrecioEntry { get; set; }
        }
    }
}