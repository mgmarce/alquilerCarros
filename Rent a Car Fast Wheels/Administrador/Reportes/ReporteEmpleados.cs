using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rent_a_Car_Fast_Wheels.Administrador.Reportes
{
    public partial class ReporteEmpleados : Form
    {
        public ReporteEmpleados()
        {
            InitializeComponent();
        }

        public string tipo { get; set; }
        private void ReporteEmpleados_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'DataSetEmpleados.ReporteEmpleados' Puede moverla o quitarla según sea necesario.
            this.ReporteEmpleadosTableAdapter.Fill(this.DataSetEmpleados.ReporteEmpleados,tipo);

            this.reportViewer1.RefreshReport();
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            tipo = cboTipo.Text;
            this.ReporteEmpleadosTableAdapter.Fill(this.DataSetEmpleados.ReporteEmpleados, tipo);

            this.reportViewer1.RefreshReport();
        }

        private void cboTipo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
    }
}
