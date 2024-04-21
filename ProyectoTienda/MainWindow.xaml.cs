using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using ProyectoTienda.Backend.Modelo;
using ProyectoTienda.Frontend.Dialogos;
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

namespace ProyectoTienda
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private tiendaEntities tieEnt;
        private MVClienta mvClie;


        public MainWindow()
        {
            InitializeComponent();
            tieEnt = new tiendaEntities();
            mvClie = new MVClienta(tieEnt);
            DataContext= mvClie;
            dgListaClientas.SelectedItem = null;
        }

        private void textNombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            mvClie.Filtra();
        }

        private void comboMes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            mvClie.Filtra();
        }

        private void btnQuitaFiltros_Click(object sender, RoutedEventArgs e)
        {
            mvClie.quitaFiltros();

        }

        private void btnNuevaClienta_Click(object sender, RoutedEventArgs e)
        {
            DialogoClientaNew diag = new DialogoClientaNew(tieEnt);
            diag.ShowDialog();
            
            dgListaClientas.ItemsSource = mvClie.Refresca();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (dgListaClientas.SelectedItem != null && dgListaClientas.SelectedItem is clienta)
            {
                clienta c = (clienta)dgListaClientas.SelectedItem;
                DialogoClientaNew diag = new DialogoClientaNew(tieEnt, c);
                diag.ShowDialog();
            }
            dgListaClientas.ItemsSource = mvClie.Refresca();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            if (dgListaClientas.SelectedItem != null && dgListaClientas.SelectedItem is clienta)
            {
                clienta c = (clienta)dgListaClientas.SelectedItem;
                DialogoEdiarPuntos diag = new DialogoEdiarPuntos(tieEnt, c);
                diag.ShowDialog();
            }
            dgListaClientas.ItemsSource = mvClie.Refresca();
        }
    }
}
