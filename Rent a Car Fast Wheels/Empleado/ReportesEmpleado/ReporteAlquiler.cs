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
    public partial class ReporteAlquiler : Form
    {
        public ReporteAlquiler()
        {
            InitializeComponent();
        }

        public List<DatosAlquiler> Datos = new List<DatosAlquiler>();
        private void ReporteAlquiler_Load(object sender, EventArgs e)
        {
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSetAlquiler", Datos));
            this.reportViewer1.RefreshReport();
        }
    }
}
