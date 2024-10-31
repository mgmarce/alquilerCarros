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

namespace Rent_a_Car_Fast_Wheels.Empleado
{
    public partial class FrmAutosEmpleado : Form
    {
        public FrmAutosEmpleado()
        {
            InitializeComponent();
        }

        SqlConnection cn = new SqlConnection("Data Source=localhost; Initial Catalog = rentcar; Integrated Security=True;");
        public DataTable dt;
        public SqlDataAdapter sda;
        public SqlDataReader dr;
        public void LLenarGrid(DataGridView dgv)
        {
            try
            {

                SqlCommand cmd = new SqlCommand("ListarAutosEmpleado", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                dt = new DataTable();
                sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);

                dgv.DataSource = dt;

            }
            catch (Exception ex)
            {

                MessageBox.Show("Error en la conexion, No se pudo obtener la informacion" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void VaciarCampos()
        {

            txtCod.Clear();
            txtTipoAuto.Clear();
            txtMarca.Clear();
            txtModelo.Clear();
            txtColor.Clear();
            txtTransmision.Clear();
            txtMotor.Clear();
            txtPasajeros.Clear();
            txtPlaca.Clear();
            txtPrecio.Clear();
            txtEstado.Clear();
            txtMarca.Focus();
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            foto.Image = null;
            VaciarCampos();
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            LLenarGrid(GridViewAutos);
            cboBuscar.Text = "Seleccione..";
            txtBusqueda.Clear();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string busqueda = " ";

            switch (cboBuscar.Text)
            {
                case "Marca":
                    busqueda = "marca";
                    break;

                case "Tipo de Auto":
                    busqueda = "tipoauto";
                    break;

                case "Transmision":
                    busqueda = "transmision";
                    break;
                case "Numero de Pasajeros":
                    busqueda = "cantpasajeros";
                    break;

                case "Estado":
                    busqueda = "estado";
                    break;
            }
            if (string.IsNullOrEmpty(txtBusqueda.Text) || cboBuscar.Text == "Seleccione..")
            {
                MessageBox.Show("Debe seleccionar y escribir algo para buscar", "Buscar", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {
                SqlCommand co = new SqlCommand("select codigoauto as 'Codigo', tipoauto as 'Tipo',marca as 'Marca', modelo as 'Modelo', color as 'Color', transmision as 'Transmision', motor as 'Motor', cantpasajeros as 'Pasajeros', placa as 'Placa', precio as 'Precio', estado as 'Estado' from autos where " + busqueda + " like '%" + txtBusqueda.Text + "%'", cn);
                SqlDataAdapter sd = new SqlDataAdapter(co);
                DataTable dt = new DataTable();
                sd.Fill(dt);
                GridViewAutos.DataSource = dt;

            }
        }

        private void btnVer_Click(object sender, EventArgs e)
        {

            if (GridViewAutos.SelectedRows.Count == 1)
            {
                txtCod.Text = GridViewAutos.CurrentRow.Cells[0].Value.ToString();
                txtTipoAuto.Text = GridViewAutos.CurrentRow.Cells[1].Value.ToString();
                txtMarca.Text = GridViewAutos.CurrentRow.Cells[2].Value.ToString();
                txtModelo.Text = GridViewAutos.CurrentRow.Cells[3].Value.ToString();
                txtColor.Text = GridViewAutos.CurrentRow.Cells[4].Value.ToString();
                txtTransmision.Text = GridViewAutos.CurrentRow.Cells[5].Value.ToString();
                txtMotor.Text = GridViewAutos.CurrentRow.Cells[6].Value.ToString();
                txtPasajeros.Text = GridViewAutos.CurrentRow.Cells[7].Value.ToString();
                txtPlaca.Text = GridViewAutos.CurrentRow.Cells[8].Value.ToString();
                txtPrecio.Text = GridViewAutos.CurrentRow.Cells[9].Value.ToString();
                txtEstado.Text = GridViewAutos.CurrentRow.Cells[10].Value.ToString();


                DataSet ds;
                SqlDataAdapter da;
                DataRow dr;


                da = new SqlDataAdapter("Select imagen from autos where codigoauto = '" + GridViewAutos.CurrentRow.Cells[0].Value.ToString() + "'", cn);
                ds = new DataSet();
                da.Fill(ds, "autos");
                byte[] datos = new byte[0];
                dr = ds.Tables["autos"].Rows[0];
                datos = (byte[])dr["imagen"];
                System.IO.MemoryStream ms = new System.IO.MemoryStream(datos);
                foto.Image = System.Drawing.Bitmap.FromStream(ms);


            }

            else

                MessageBox.Show("Seleccione una fila por favor", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void FrmAutosEmpleado_Load(object sender, EventArgs e)
        {
            LLenarGrid(GridViewAutos);
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            ReportesEmpleado.ReporteAutoEmpleado report = new ReportesEmpleado.ReporteAutoEmpleado();

            report.ShowDialog();
        }
    }
}
