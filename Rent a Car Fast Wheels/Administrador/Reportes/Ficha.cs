using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rent_a_Car_Fast_Wheels.Administrador.Reportes
{
   public class Ficha
    {
        public string codigo { get; set; }
        public string tipo { get; set; }
        public string transmision { get; set; }
        public string modelo { get; set; }
        public string pasajeros { get; set; }
        public string precio { get; set; }
        public string marca { get; set; }
        public string motor { get; set; }
        public string placa { get; set; }
        public string estado { get; set; }




        public Byte[] imagen { get; set; }
    }
}
