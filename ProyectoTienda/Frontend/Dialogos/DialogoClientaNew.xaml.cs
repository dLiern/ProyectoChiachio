using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using ProyectoTienda.Backend.Modelo;
using ProyectoTienda.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProyectoTienda.Frontend.Dialogos
{
    /// <summary>
    /// Lógica de interacción para DialogoClientaNew.xaml
    /// </summary>
    public partial class DialogoClientaNew : MetroWindow
    {
        private tiendaEntities tieEnt;
        private MVClienta mvClie;
        private bool edita;
        public DialogoClientaNew(tiendaEntities ent)
        {
            InitializeComponent();
            tieEnt = ent;
            mvClie = new MVClienta(tieEnt);
            mvClie.clienteNuevo = new clienta();
            DataContext = mvClie; // <- Todos los bindings se hacen a esta clase

            this.AddHandler(Validation.ErrorEvent, new RoutedEventHandler(mvClie.OnErrorEvent));
            mvClie.btnGuardar = Guardar; // <-El aceptar se asigna al nombre del boton del formulario
            edita = false;
        }

        public DialogoClientaNew(tiendaEntities ent, clienta c)
        {
            InitializeComponent();
            tieEnt = ent;
            mvClie = new MVClienta(tieEnt);
            mvClie.clienteNuevo = c;
            DataContext = mvClie; // <- Todos los bindings se hacen a esta clase

            this.AddHandler(Validation.ErrorEvent, new RoutedEventHandler(mvClie.OnErrorEvent));
            mvClie.btnGuardar = Guardar; // <-El aceptar se asigna al nombre del boton del formulario
            edita = true;
        }

        private async void Guardar_Click(object sender, RoutedEventArgs e)
        {
            if (edita)
            {
                if (mvClie.edita)
                {
                    await this.ShowMessageAsync("GESTION EDICION CLIENTAS", "TODO CORRECTO");
                    DialogResult = true;
                }
                else
                {
                    await this.ShowMessageAsync("GESTION EDICION CLIENTAS", "ERROR");
                    DialogResult = false;
                }
            }
            else
            {
                if (mvClie.guarda)
                {
                    await this.ShowMessageAsync("GESTION CREACION CLIENTAS", "TODO CORRECTO");
                    DialogResult = true;
                }
                else
                {
                    await this.ShowMessageAsync("GESTION CREACION CLIENTAS", "ERROR");
                    DialogResult = false;
                }
            }

        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;

        }
    }
}
