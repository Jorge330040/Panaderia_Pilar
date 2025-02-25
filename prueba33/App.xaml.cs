using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using prueba33.Data;

namespace prueba33
{
    public partial class App : Application
    {
        private static UsuarioDatabase usuarioDatabase;
        private static InventarioDatabase inventarioDatabase;
        private static GananciaDatabase gananciaDatabase;

        public static UsuarioDatabase UsuarioDatabase
        {
            get
            {
                if (usuarioDatabase == null)
                {
                    usuarioDatabase = new UsuarioDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Usuarios.db3"));
                }
                return usuarioDatabase;
            }
        }

        public static InventarioDatabase InventarioDatabase
        {
            get
            {
                if (inventarioDatabase == null)
                {
                    inventarioDatabase = new InventarioDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Inventarios.db3"));
                }
                return inventarioDatabase;
            }
        }

        // Nueva propiedad para la base de datos de Ganancias
        public static GananciaDatabase GananciaDatabase
        {
            get
            {
                if (gananciaDatabase == null)
                {
                    gananciaDatabase = new GananciaDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Ganancias.db3"));
                }
                return gananciaDatabase;
            }
        }

        public App()
        {
            InitializeComponent();

            // Establecer la MainPage dentro de una NavigationPage
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}