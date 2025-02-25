using prueba33.Models;
using System.Linq;
using System.Threading.Tasks;
using System;
using Xamarin.Forms;
using System.Collections.Generic;

namespace prueba33
{
    public partial class InventarioPage : ContentPage
    {
        public InventarioPage()
        {
            InitializeComponent();
            _ = LoadSavedDataAsync();  // Cargar datos guardados al iniciar la página
        }

        private async Task LoadSavedDataAsync()
        {
            try
            {
                var inventarios = await App.InventarioDatabase.GetInventariosAsync();
                inventarioListView.ItemsSource = inventarios;
                // Ya no se actualiza ni muestra el total de inversión aquí
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Ocurrió un error al cargar los datos: {ex.Message}", "OK");
            }
        }

        private async void OnGuardarRegistroClicked(object sender, EventArgs e)
        {
            if (decimal.TryParse(precioEntry.Text, out decimal precio) &&
                int.TryParse(cantidadEntry.Text, out int cantidad) &&
                !string.IsNullOrWhiteSpace(materialEntry.Text))
            {
                var inventario = new Inventario
                {
                    Material = materialEntry.Text,
                    Cantidad = cantidad,
                    UnidadMedida = unidadMedidaEntry.Text,
                    PrecioUnidad = precio,
                    ValorTotal = precio * cantidad,
                    Fecha = DateTime.Now.Date
                };

                await App.InventarioDatabase.SaveInventarioAsync(inventario);

                // Actualizar la lista después de guardar
                var inventarios = await App.InventarioDatabase.GetInventariosAsync();
                inventarioListView.ItemsSource = inventarios;

                // Limpiar los campos de entrada
                materialEntry.Text = string.Empty;
                cantidadEntry.Text = string.Empty;
                unidadMedidaEntry.Text = string.Empty;
                precioEntry.Text = string.Empty;
            }
            else
            {
                await DisplayAlert("Error", "Por favor complete todos los campos antes de guardar.", "OK");
            }
        }

        private async void OnEliminarUltimoRegistroClicked(object sender, EventArgs e)
        {
            try
            {
                var inventarios = await App.InventarioDatabase.GetInventariosAsync();
                if (inventarios.Any())
                {
                    var ultimoInventario = inventarios.Last();
                    await App.InventarioDatabase.DeleteInventarioAsync(ultimoInventario);

                    // Actualizar la lista después de eliminar
                    inventarios = await App.InventarioDatabase.GetInventariosAsync();
                    inventarioListView.ItemsSource = inventarios;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Ocurrió un error al eliminar el registro: {ex.Message}", "OK");
            }
        }

        private async void OnCerrarRegistroClicked(object sender, EventArgs e)
        {
            try
            {
                var inventarios = await App.InventarioDatabase.GetInventariosAsync();
                if (inventarios.Any())
                {
                    decimal totalInversion = inventarios.Sum(i => i.ValorTotal);

                    // Crear un registro final que indique el cierre
                    var cierreInventario = new Inventario
                    {
                        Material = "Total de Inversión",
                        Cantidad = null,  // No mostrar nada en "Cantidad"
                        UnidadMedida = DateTime.Now.ToString("dd/MM/yyyy"), // Colocar la fecha aquí
                        PrecioUnidad = null,  // No mostrar nada en "Precio"
                        ValorTotal = totalInversion,
                        Fecha = DateTime.Now.Date
                    };

                    await App.InventarioDatabase.SaveInventarioAsync(cierreInventario);

                    // Actualizar la lista para mostrar el registro de cierre
                    inventarios = await App.InventarioDatabase.GetInventariosAsync();
                    inventarioListView.ItemsSource = inventarios;

                    await DisplayAlert("Registro Cerrado", $"Se ha registrado el cierre de la inversión con un total de {totalInversion:C2}.", "OK");
                }
                else
                {
                    await DisplayAlert("Error", "No hay registros para cerrar.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Ocurrió un error al cerrar el registro: {ex.Message}", "OK");
            }
        }
    }
}