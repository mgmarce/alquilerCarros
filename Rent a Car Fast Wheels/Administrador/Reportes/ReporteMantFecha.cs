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
    public partial class ReporteMantFecha : Form
    {
        public ReporteMantFecha()
        {
            InitializeComponent();
        }
        public DateTime fecha1 { get; set; }
        public DateTime fecha2 { get; set; }

        private void ReporteMantFecha_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'DataSetMant.ReporteMantenimiento_Fecha' Puede moverla o quitarla según sea necesario.
            this.ReporteMantenimiento_FechaTableAdapter.Fill(this.DataSetMant.ReporteMantenimiento_Fecha, fecha1, fecha2);

            this.reportViewer1.RefreshReport();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            DateTime fecha1 = dateTimePicker1.Value;
            DateTime fecha2 = dateTimePicker2.Value;

            fecha1.ToString("dd/MM/yyyy");
            fecha2.ToString("dd/MM/yyyy");
            // TODO: esta línea de código carga datos en la tabla 'DataSetMant.ReporteMantenimiento_Fecha' Puede moverla o quitarla según sea necesario.
            this.ReporteMantenimiento_FechaTableAdapter.Fill(this.DataSetMant.ReporteMantenimiento_Fecha, fecha1, fecha2);


            this.reportViewer1.RefreshReport();
        }
    }
}
