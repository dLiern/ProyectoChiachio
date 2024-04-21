using ProyectoFede.Backend.Servicios;
using ProyectoTienda.Backend.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace ProyectoTienda.Backend.Servicios
{
    public class ServicioClienta : ServicioGenerico<clienta>
    {

        private tiendaEntities contexto;

        public ServicioClienta(tiendaEntities context) : base(context)
        {
            contexto = context;
        }

        //SELECT MONTH(fechaNacimiento) AS mes FROM clienta;

        /*public List<string> getMeses() {
            List<string> listaMeses = new List<string>() { "Enero", "Febrero", "Marzo", "Abril", 
                                                            "Mayo", "Junio", "Julio", "Agosto", "Septiembre", 
                                                            "Octubre", "Noviembre", "Diciembre"};

            return listaMeses;
        }*/

        public List<string> getMeses()
        {
            var result = contexto.Database.SqlQuery<string>("SET lc_time_names = 'es_ES';" +
                "\r\n SELECT DISTINCT UPPER(mesNacimiento) FROM clienta;").ToList();

            return result;
        }
    }
}
