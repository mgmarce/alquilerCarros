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

namespace Rent_a_Car_Fast_Wheels.Administrador.Reportes
{
    public partial class FichaAuto : Form
          
    {

        public List<Ficha> Datos = new List<Ficha>();
        public FichaAuto()
        {
            InitializeComponent();
        }

        private void FichaAuto_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", Datos));
            this.reportViewer1.RefreshReport();
        }
    }
}
