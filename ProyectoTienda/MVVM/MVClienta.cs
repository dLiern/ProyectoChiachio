using ProyectoFede.MVVM.Base;
using ProyectoTienda.Backend.Modelo;
using ProyectoTienda.Backend.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ProyectoTienda.MVVM
{
    public class MVClienta : MVBaseCRUD<clienta>
    {
        // VARIABLES PRIVADAS ***********************************
        private tiendaEntities tieEnt;
        private ServicioClienta servCli;
        private clienta _clienteNuevo;
        private clienta _clienteSeleccionado;
        private ListCollectionView listaAux; //<-- Lista de Equipos que vamos a pasar a la tabla
        private string nombre; //<-- Nombre escrito por el usuario
        private string mesNacimiento; //<-- Grupo Seleccionada por el usuario

        //VARIABLES PRIVADAS DE FILTRADO *****************************
        private List<Predicate<clienta>> criterios;
        private Predicate<clienta> criterioMesNacimiento;
        private Predicate<clienta> criterioNombre;
        private Predicate<object> predicadoFiltro;

        // CONSTRUCTOR ******************************************
        public MVClienta(tiendaEntities ent)
        {
            tieEnt = ent;
            inicializa();
        }

        // Metodos PRIVADOS *************************************
        private void inicializa()
        {
            servCli = new ServicioClienta(tieEnt);
            servicio = servCli;
            _clienteNuevo = new clienta();
            listaAux = new ListCollectionView(servicio.getAll().ToList()); //<-- Lista Criterios Filtrado

            criterios = new List<Predicate<clienta>>();
            criterioNombre = new Predicate<clienta>(c => !string.IsNullOrEmpty(c.nombre) && c.nombre.ToUpper().Contains(nombreClienta.ToUpper())); //<--Criterios Filtrado Nombre
            criterioMesNacimiento = new Predicate<clienta>(c => !string.IsNullOrEmpty(c.mesNacimiento) && c.mesNacimiento.ToUpper().Equals(mesSeleccionado.ToUpper())); //<--Criterio Filtrado Grupo

            predicadoFiltro = new Predicate<object>(FiltroCriterios);
        }

        //VARIABLES PUBLICAS *****************************************

        public ListCollectionView listaClientas { get { return listaAux; } } // <-- Nos devuelve una lista de objetos Clientas, donde podemos pedir que muestre nombre etc...
        public List<string> listaMeses { get { return servCli.getMeses(); } } // <--- Nos devuelve una lista de Meses

        public string nombreClienta { get { return nombre; } set { nombre = value; NotifyPropertyChanged(nameof(nombreClienta)); } } //<-- Propiedad publica que coge del xaml el nombre escrito por el usuario
        public string mesSeleccionado { get { return mesNacimiento; } set { mesNacimiento = value; NotifyPropertyChanged(nameof(mesSeleccionado)); } }//<-- Propiedad publica que coge del xaml la provincia seleccionado por el usuario

        public clienta clienteNuevo
        {
            get { return _clienteNuevo; }
            set { _clienteNuevo = value; NotifyPropertyChanged(nameof(clienteNuevo)); }
        }

        public clienta clienteSeleccionado { 
            get { return _clienteSeleccionado; }  
            set { _clienteSeleccionado = value; NotifyPropertyChanged(nameof(clienteSeleccionado)); } 
        }

        public ListCollectionView Refresca()
        {
            return new ListCollectionView(servCli.getAll().ToList());
        }
        
        public bool guarda { get { return add(clienteNuevo); } } // <-- Nos permite guardar Nadadores

        public bool edita { get { return update(clienteNuevo); } } // <-- Nos permite editar Nadadores

        // VARIABLES PUBLICAS FILTRADO *************************************************

        public void Filtra() //<-- Disparador de la funcion Filtrar, podemos filtrar cuando por ejemplo pinchemos un boton, escribamos...
        {
            addCriterios();
            listaClientas.Filter = predicadoFiltro;
        }

        private void addCriterios()
        {
            criterios.Clear();
            if (!string.IsNullOrEmpty(mesSeleccionado)) { criterios.Add(criterioMesNacimiento); }
            if (!string.IsNullOrEmpty(nombreClienta)) { criterios.Add(criterioNombre); }
        }

        public void quitaFiltros()
        {
            listaClientas.Filter = null;
            mesSeleccionado = null;
            nombreClienta = "";
        }

        private bool FiltroCriterios(object item)
        {
            bool correcto = true;
            clienta entity = (clienta)item;
            if (criterios.Count() != 0)
            {
                correcto = criterios.TrueForAll(x => x(entity));
            }
            return correcto;
        }
    }
}
