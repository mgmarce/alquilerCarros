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

namespace Rent_a_Car_Fast_Wheels.Administrador
{
    public partial class FrmMantenimiento : Form
    {
        public FrmMantenimiento()
        {
            InitializeComponent();
        }

        private void FrmMantenimiento_Load(object sender, EventArgs e)
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            VaciarCampos();

            btnActualizar.Visible = false;
            txtCodman.Visible = false;
            lblCodigo.Visible = false;
            //btnActualizar.Visible = true;
            VaciarCampos(); 
        }

        private void btnEditar_Click(object sender, EventArgs e)
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

                btnActualizar.Visible = true;
                txtCodman.Visible = true;
                lblCodigo.Visible = true;
            }
            else

                MessageBox.Show("Seleccione una fila por favor", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtCodauto.Text) || cboTipo.Text == "Seleccione..." || string.IsNullOrEmpty(txtMarca.Text) ||
                string.IsNullOrEmpty(txtModelo.Text) || cboEstado.Text == "Seleccione..." ||
                string.IsNullOrEmpty(txtPlaca.Text) || string.IsNullOrEmpty(txtArreglo.Text) )
            {

                MessageBox.Show("Debe llenar los campos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

            else
            {
                string codman = txtCodman.Text;
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
                    SqlCommand cmd1 = new SqlCommand("ActualizarMantoAuto", cn);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.AddWithValue("@codmanto", codman);
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

                    btnActualizar.Visible = false;
                    txtCodman.Visible = false;
                    lblCodigo.Visible = false;
                    VaciarCampos();
                    
                    MessageBox.Show("Actualizado correctamente", "Actualizacion de Auto", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error, datos no válidos" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (GridViewManto.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("¿Esta seguro que desea eliminar el registro?", "Alerta", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    string cod = GridViewManto.CurrentRow.Cells[0].Value.ToString();

                    SqlCommand cmd = new SqlCommand("EliminarMantoAuto", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@cod", cod);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);


                    MessageBox.Show("Auto Eliminado Correctamente", "Borrar Producto", MessageBoxButtons.OK, MessageBoxIcon.Information);



                    LLenarGrid(GridViewManto);
                }
            }

            else
            {

                MessageBox.Show("Seleccione una fila por favor", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

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

        private void cboEstado_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cboBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            Reportes.ReporteMantFecha report = new Reportes.ReporteMantFecha();

            report.ShowDialog();
        }
    }
}
