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
    public partial class ReporteAlquilerAdmin : Form
    {
        public ReporteAlquilerAdmin()
        {
            InitializeComponent();
        }

        public DateTime fecha1 { get; set; }
        public DateTime fecha2 { get; set; }

        private void ReporteAlquilerAdmin_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'DataSetAlquiler.ReporteAlquiler' Puede moverla o quitarla según sea necesario.
            this.ReporteAlquilerTableAdapter.Fill(this.DataSetAlquiler.ReporteAlquiler,fecha1,fecha2);

            this.reportViewer1.RefreshReport();
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            DateTime fecha1 = dtpFecha1.Value;
            DateTime fecha2 = dtpFecha2.Value;
            this.ReporteAlquilerTableAdapter.Fill(this.DataSetAlquiler.ReporteAlquiler, fecha1, fecha2);
            this.reportViewer1.RefreshReport();

        }
    }
}
