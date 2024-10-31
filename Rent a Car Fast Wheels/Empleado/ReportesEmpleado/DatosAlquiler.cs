using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rent_a_Car_Fast_Wheels.Empleado.ReportesEmpleado
{
    public class DatosAlquiler
    {
        public string codigocliente { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string dui { get; set; }
        public string licencia { get; set; }
        public string codigoauto { get; set; }
        public string marca { get; set; }
        public string modelo { get; set; }
        public string tipo { get; set; }
        public string placa { get; set; }
        public string diaretiro { get; set; }
        public string diaentrada { get; set; }
        public string diasalquilado { get; set; }
        public decimal precio { get; set; }
        public decimal seguro { get; set; }
        public decimal total { get; set; }
        public decimal totalglobal { get; set; }
        public string codigoempleado { get; set; }
    }
}
