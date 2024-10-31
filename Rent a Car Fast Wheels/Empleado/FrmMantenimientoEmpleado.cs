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
    public partial class FrmMantenimientoEmpleado : Form
    {
        public FrmMantenimientoEmpleado()
        {
            InitializeComponent();
        }

        private void FrmMantenimientoEmpleado_Load(object sender, EventArgs e)
        {
            LLenarGrid(GridViewManto);
        }

        SqlConnection cn = new SqlConnection(@"server=localhost; Initial Catalog = rentcar; Integrated Security=True;");
        public DataTable dt;
        public SqlDataAdapter sda;
        public SqlDataReader dr;

        public void LLenarGrid(DataGridView dgv)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("ListarMantenimiento", cn);
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

            txtCodauto.Clear();
            cboTipo.Text = "Seleccione...";
            txtMarca.Clear();
            txtModelo.Clear();
            txtPlaca.Clear();
            cboEstado.Text = "Seleccione...";
            dateSalida.Value = DateTime.Today;
            txtArreglo.Clear();
            
            txtCodauto.Focus();

        }

        public void AgregarAutos(string c)
        {
            try
            {

                SqlCommand cmd = new SqlCommand("BuscarAuto", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@codAuto", c);
                dt = new DataTable();
                sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);



            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en la conexion, No se pudo obtener la informacion" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            if (dt.Rows.Count == 1)
            {
                string estado = dt.Rows[0][10].ToString();
                if (estado == "Disponible" || estado == "disponible")
                {
                    cboTipo.Text = dt.Rows[0][1].ToString();
                    txtMarca.Text = dt.Rows[0][2].ToString();
                    txtModelo.Text = dt.Rows[0][3].ToString();
                    txtPlaca.Text = dt.Rows[0][8].ToString();
                }
                else
                {
                    MessageBox.Show("El auto esta en alquiler o en mantenimiento, por favor consulte", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            else
            {
                MessageBox.Show("No se encontro ningun auto con ese codigo", "Sin resultado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            AgregarAutos(txtCodauto.Text);
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCodauto.Text) || cboTipo.Text == "Seleccione..." || string.IsNullOrEmpty(txtMarca.Text) ||
                string.IsNullOrEmpty(txtModelo.Text) ||
                string.IsNullOrEmpty(txtPlaca.Text) || cboEstado.Text == "Seleccione..."
                 || string.IsNullOrEmpty(txtArreglo.Text))
            {
                MessageBox.Show("Debe llenar los campos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }
            else
            {
                string codauto = txtCodauto.Text;
                string tipo = cboTipo.Text;
                string marca = txtMarca.Text;
                string modelo = txtModelo.Text;
                string placa = txtPlaca.Text;
                string estado = cboEstado.Text;

                DateTime salida = dateSalida.Value;
                string arreglo = txtArreglo.Text;

                try
                {
                    SqlCommand cmd = new SqlCommand("BuscarMantoAuto", cn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@cod", codauto);
                    dt = new DataTable();
                    sda = new SqlDataAdapter(cmd);
                    sda.Fill(dt);
                    if (dt.Rows.Count == 1)
                    {

                        MessageBox.Show("Ya existe un auto con ese codigo", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        txtCodman.Clear();
                        txtCodman.Visible = false;
                        lblCodigo.Visible = false;
                    }

                    else
                    {
                        SqlCommand cmd1 = new SqlCommand("AgregarMantoAuto", cn);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.AddWithValue("@codigoauto", codauto);
                        cmd1.Parameters.AddWithValue("@tipoauto", tipo);
                        cmd1.Parameters.AddWithValue("@marca", marca);
                        cmd1.Parameters.AddWithValue("@modelo", modelo);
                        cmd1.Parameters.AddWithValue("@placa", placa);
                        cmd1.Parameters.AddWithValue("@estado", estado);
                        cmd1.Parameters.AddWithValue("@fechsaltaller ", salida);
                        cmd1.Parameters.AddWithValue("@tiparreglo ", arreglo);

                        SqlDataAdapter sda = new SqlDataAdapter(cmd1);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);

                        LLenarGrid(GridViewManto);
                        VaciarCampos();

                        MessageBox.Show("Ingresado correctamente", "Ingreso de Auto", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                }


                catch (Exception ex)
                {
                    MessageBox.Show("Error, datos no válidos" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            VaciarCampos();
            btnAgregar.Visible = true;
            txtCodman.Visible = false;
            lblCodigo.Visible = false;
            VaciarCampos();
            btnInsertar.Visible = true;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string busqueda = " ";

            switch (cboBuscar.Text)
            {
                case "Modelo":
                    busqueda = "modelo";
                    break;
                case "Tipo de Auto":
                    busqueda = "tipoauto";
                    break;
                case "Placa":
                    busqueda = "placa";
                    break;
            }
            if (string.IsNullOrEmpty(txtBusqueda.Text) || cboBuscar.Text == "Seleccione..")
            {
                MessageBox.Show("Debe seleccionar y escribir algo para buscar", "Buscar", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {

                SqlCommand co = new SqlCommand("select codmantenimiento as 'Codigo Mantenimiento', codigoauto as 'Codigo', tipoauto as 'Tipo',marca as 'Marca', modelo as 'Modelo', placa as 'Placa', estado as 'Estado', fechingtaller as 'Fecha Ingreso', fechsaltaller as 'Fecha Salida', tiparreglo as 'Tipo Arreglo' from mantenimiento where " + busqueda + " like '%" + txtBusqueda.Text + "%'", cn);
                SqlDataAdapter sd = new SqlDataAdapter(co);
                DataTable dt = new DataTable();
                sd.Fill(dt);
                GridViewManto.DataSource = dt;

            }
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            LLenarGrid(GridViewManto);
            cboBuscar.Text = "Seleccione..";
            txtBusqueda.Clear();
        }

        private void txtArreglo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) &&
                (e.KeyChar != ' '))
            {
                e.Handled = true;
            }
        }

        private void txtEstado_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) &&
                (e.KeyChar != ' '))
            {
                e.Handled = true;
            }

        }

        private void btnVer_Click(object sender, EventArgs e)
        {
            if (GridViewManto.SelectedRows.Count == 1)
            {
                txtCodman.Text = GridViewManto.CurrentRow.Cells[0].Value.ToString();
                txtCodauto.Text = GridViewManto.CurrentRow.Cells[1].Value.ToString();
                cboTipo.Text = GridViewManto.CurrentRow.Cells[2].Value.ToString();
                txtMarca.Text = GridViewManto.CurrentRow.Cells[3].Value.ToString();
                txtModelo.Text = GridViewManto.CurrentRow.Cells[4].Value.ToString();
                txtPlaca.Text = GridViewManto.CurrentRow.Cells[5].Value.ToString();

                cboEstado.Text = GridViewManto.CurrentRow.Cells[6].Value.ToString();
                dateSalida.Text = GridViewManto.CurrentRow.Cells[8].Value.ToString();
                txtArreglo.Text = GridViewManto.CurrentRow.Cells[9].Value.ToString();

                txtCodman.Visible = true;
                lblCodigo.Visible = true;
                //
                btnInsertar.Visible = false;
                btnAgregar.Visible = false;

            }

            else if (GridViewManto.SelectedRows.Count > 1)
                MessageBox.Show("Seleccione solo una fila por favor", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else

                MessageBox.Show("Seleccione una fila por favor", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }

        private void cboEstado_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cboBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void btnFicha_Click(object sender, EventArgs e)
        {
            ReportesEmpleado.ReporteMantFecha report = new ReportesEmpleado.ReporteMantFecha();

            report.ShowDialog();
        }
    }
}
