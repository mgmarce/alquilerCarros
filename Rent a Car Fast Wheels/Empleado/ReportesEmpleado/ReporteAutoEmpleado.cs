using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rent_a_Car_Fast_Wheels.Empleado.ReportesEmpleado
{
    public partial class ReporteAutoEmpleado : Form
    {
        public ReporteAutoEmpleado()
        {
            InitializeComponent();
        }

        SqlConnection cn = new SqlConnection("Data Source=localhost; Initial Catalog = rentcar; Integrated Security=True;");
        public string marca { get; set; }
        private void ReporteAutoEmpleado_Load(object sender, EventArgs e)
        {

            SqlCommand comando = new SqlCommand("Select DISTINCT marca  from autos", cn);
            cn.Open();
            SqlDataReader marcas = comando.ExecuteReader();
            while (marcas.Read())
            {
                cboMarca.Items.Add(marcas["marca"].ToString());
            }
            cn.Close();

            // TODO: esta línea de código carga datos en la tabla 'DataSetAutosEmpleado.AutosporMarca' Puede moverla o quitarla según sea necesario.
            this.AutosporMarcaTableAdapter.Fill(this.DataSetAutosEmpleado.AutosporMarca,marca);

            this.reportViewer1.RefreshReport();

        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            marca = cboMarca.Text;
            this.AutosporMarcaTableAdapter.Fill(this.DataSetAutosEmpleado.AutosporMarca, marca);

            this.reportViewer1.RefreshReport();
        }
    }
}
