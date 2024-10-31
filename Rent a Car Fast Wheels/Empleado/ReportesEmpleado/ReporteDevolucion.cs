using Microsoft.Reporting.WinForms;
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
    public partial class ReporteDevolucion : Form
    {
        public ReporteDevolucion()
        {
            InitializeComponent();
        }
        public List<DatosDevolucion> Datos = new List<DatosDevolucion>();
        private void ReporteDevolucion_Load(object sender, EventArgs e)
        {
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSetDevolucion", Datos));
            this.reportViewer1.RefreshReport();
        }
    }
}
