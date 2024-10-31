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
    public partial class ReporteautosAdmin : Form
    {
        public ReporteautosAdmin()
        {
            InitializeComponent();
        }
        public string estado { get; set; }

        private void ReporteautosAdmin_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'DataSetAutos.ReporteAutosAdmin' Puede moverla o quitarla según sea necesario.
            this.ReporteAutosAdminTableAdapter.Fill(this.DataSetAutos.ReporteAutosAdmin,estado);

            this.reportViewer1.RefreshReport();
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            estado = cboEstado.Text;

            this.ReporteAutosAdminTableAdapter.Fill(this.DataSetAutos.ReporteAutosAdmin, estado);

            this.reportViewer1.RefreshReport();
        }

        private void cboEstado_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
    }
}
