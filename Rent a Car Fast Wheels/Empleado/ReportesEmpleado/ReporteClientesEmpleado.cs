using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rent_a_Car_Fast_Wheels.Empleado.ReportesEmpleado
{
    public partial class ReporteClientesEmpleado : Form
    {
        public ReporteClientesEmpleado()
        {
            InitializeComponent();
        }

        private void ReporteClientesEmpleado_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'DataSetClientesEmpleado.ListarClientesReporte' Puede moverla o quitarla según sea necesario.
            this.ListarClientesReporteTableAdapter.Fill(this.DataSetClientesEmpleado.ListarClientesReporte);

            this.reportViewer1.RefreshReport();
        }
    }
}
