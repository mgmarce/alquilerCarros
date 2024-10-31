using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rent_a_Car_Fast_Wheels.Empleado
{
    public partial class FrmInicioEmpleado : Form
    {
        public FrmInicioEmpleado(string nombre, string apellido, string tipo, string c)
        {
            InitializeComponent();
            lblNombre.Text = nombre + " " + apellido;
            lbltp.Text = tipo;
        }
    }
}
