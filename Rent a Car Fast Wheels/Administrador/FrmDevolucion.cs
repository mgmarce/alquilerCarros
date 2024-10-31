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
    public partial class FrmDevolucion : Form
    {
        public FrmDevolucion()
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
                SqlCommand cmd = new SqlCommand("ListarDevolucion", cn);
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

        private void btnListar_Click(object sender, EventArgs e)
        {
            LLenarGrid(GridViewDevolucion);
            borrar();
            btnEliminar.Visible = true;
            btnEditar.Visible = true;
            btnVer.Visible = true;
        }

        private void FrmDevolucion_Load(object sender, EventArgs e)
        {
            btnBorrar.Visible = false;
            btnGuardar.Visible = false;
            LLenarGrid(GridViewDevolucion);
        }

        private void btnVer_Click(object sender, EventArgs e)
        {

            if (GridViewDevolucion.SelectedRows.Count == 1)
            {
                txtCodDevl.Text = GridViewDevolucion.CurrentRow.Cells[0].Value.ToString();
                txtCodAlq.Text = GridViewDevolucion.CurrentRow.Cells[1].Value.ToString();
                txtNomCliente.Text = GridViewDevolucion.CurrentRow.Cells[2].Value.ToString();
                txtApellidos.Text = GridViewDevolucion.CurrentRow.Cells[3].Value.ToString();
                txtDui.Text = GridViewDevolucion.CurrentRow.Cells[4].Value.ToString();
                txtLicencia.Text = GridViewDevolucion.CurrentRow.Cells[5].Value.ToString();

                txtTipoAuto.Text = GridViewDevolucion.CurrentRow.Cells[6].Value.ToString();
                txtMarca.Text = GridViewDevolucion.CurrentRow.Cells[7].Value.ToString();
                txtModelo.Text = GridViewDevolucion.CurrentRow.Cells[8].Value.ToString();
                txtPlaca.Text = GridViewDevolucion.CurrentRow.Cells[9].Value.ToString();

                txtDiaRetiro.Text = GridViewDevolucion.CurrentRow.Cells[10].Value.ToString();
                txtDiaEntrega.Text = GridViewDevolucion.CurrentRow.Cells[11].Value.ToString();
                txtDiasAlquilados.Text = GridViewDevolucion.CurrentRow.Cells[12].Value.ToString();
                txtDiaDevol.Text = GridViewDevolucion.CurrentRow.Cells[13].Value.ToString();
                txtTotalDiasAl.Text = GridViewDevolucion.CurrentRow.Cells[14].Value.ToString();
                txtPrecio.Text = GridViewDevolucion.CurrentRow.Cells[15].Value.ToString();
                txtMora.Text = GridViewDevolucion.CurrentRow.Cells[16].Value.ToString();
                txtSeguro.Text = GridViewDevolucion.CurrentRow.Cells[17].Value.ToString();
                txtCostoAccidente.Text = GridViewDevolucion.CurrentRow.Cells[18].Value.ToString();
                txtDevSeguro.Text = GridViewDevolucion.CurrentRow.Cells[19].Value.ToString();
                txtExtra.Text = GridViewDevolucion.CurrentRow.Cells[20].Value.ToString();
                txtTotalPagar.Text = GridViewDevolucion.CurrentRow.Cells[21].Value.ToString();
                txtObservacion.Text = GridViewDevolucion.CurrentRow.Cells[22].Value.ToString();
                CamposReadOnly();
                btnGuardar.Visible = false;
                btnVer.Visible = false;
                btnBorrar.Visible = true;
                btnEditar.Visible = false;
                btnEliminar.Visible = false;
            }
            else

                MessageBox.Show("Seleccione una fila por favor", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public void CamposReadOnly()
        {
            txtDiaRetiro.ReadOnly = true;
            txtDiaEntrega.ReadOnly = true;
            txtDiasAlquilados.ReadOnly = true;
            txtDiaDevol.ReadOnly = true;
            txtTotalDiasAl.ReadOnly = true;
            txtPrecio.ReadOnly = true;
            txtMora.ReadOnly = true;
            txtSeguro.ReadOnly = true;
            txtCostoAccidente.ReadOnly = true;
            txtDevSeguro.ReadOnly = true;
            txtExtra.ReadOnly = true;
            txtTotalPagar.ReadOnly = true;
            txtObservacion.ReadOnly = true;
        }
         
        public void borrar()
        {
            txtCodDevl.Text = "";
            txtCodAlq.Text = "";
            txtNomCliente.Text = "";
            txtApellidos.Text = "";
            txtDui.Text = "";
            txtLicencia.Text = "";

            txtTipoAuto.Text = "";
            txtMarca.Text = "";
            txtModelo.Text = "";
            txtPlaca.Text = "";

            txtDiaRetiro.Text = "";
            txtDiaEntrega.Text = "";
            txtDiasAlquilados.Text = "";
            txtDiaDevol.Text = "";
            txtTotalDiasAl.Text = "";
            txtPrecio.Text = "";
            txtMora.Text = "";
            txtSeguro.Text = "";
            txtCostoAccidente.Text = "";
            txtDevSeguro.Text = "";
            txtExtra.Text = "";
            txtTotalPagar.Text = "";
            txtObservacion.Text = "";

            cboBuscar.Text = "Seleccione...";
            txtBusqueda.Text = "";
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            borrar();
            btnGuardar.Visible = false;
            btnVer.Visible = true;
            btnBorrar.Visible = false;
            btnEditar.Visible = true;
            btnEliminar.Visible = true;
            txtObservacion.ReadOnly = true;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            txtObservacion.ReadOnly = false;
            btnBorrar.Visible = true;
            btnGuardar.Visible = true;
            btnVer.Visible = false;
            btnEliminar.Visible = false;
            btnEditar.Visible = false;
            txtCodDevl.Text = GridViewDevolucion.CurrentRow.Cells[0].Value.ToString();
            txtCodAlq.Text = GridViewDevolucion.CurrentRow.Cells[1].Value.ToString();
            txtNomCliente.Text = GridViewDevolucion.CurrentRow.Cells[2].Value.ToString();
            txtApellidos.Text = GridViewDevolucion.CurrentRow.Cells[3].Value.ToString();
            txtDui.Text = GridViewDevolucion.CurrentRow.Cells[4].Value.ToString();
            txtLicencia.Text = GridViewDevolucion.CurrentRow.Cells[5].Value.ToString();

            txtTipoAuto.Text = GridViewDevolucion.CurrentRow.Cells[6].Value.ToString();
            txtMarca.Text = GridViewDevolucion.CurrentRow.Cells[7].Value.ToString();
            txtModelo.Text = GridViewDevolucion.CurrentRow.Cells[8].Value.ToString();
            txtPlaca.Text = GridViewDevolucion.CurrentRow.Cells[9].Value.ToString();

            txtDiaRetiro.Text = GridViewDevolucion.CurrentRow.Cells[10].Value.ToString();
            txtDiaEntrega.Text = GridViewDevolucion.CurrentRow.Cells[11].Value.ToString();
            txtDiasAlquilados.Text = GridViewDevolucion.CurrentRow.Cells[12].Value.ToString();
            txtDiaDevol.Text = GridViewDevolucion.CurrentRow.Cells[13].Value.ToString();
            txtTotalDiasAl.Text = GridViewDevolucion.CurrentRow.Cells[14].Value.ToString();
            txtPrecio.Text = GridViewDevolucion.CurrentRow.Cells[15].Value.ToString();
            txtMora.Text = GridViewDevolucion.CurrentRow.Cells[16].Value.ToString();
            txtSeguro.Text = GridViewDevolucion.CurrentRow.Cells[17].Value.ToString();
            txtCostoAccidente.Text = GridViewDevolucion.CurrentRow.Cells[18].Value.ToString();
            txtDevSeguro.Text = GridViewDevolucion.CurrentRow.Cells[19].Value.ToString();
            txtExtra.Text = GridViewDevolucion.CurrentRow.Cells[20].Value.ToString();
            txtTotalPagar.Text = GridViewDevolucion.CurrentRow.Cells[21].Value.ToString();
            txtObservacion.Text = GridViewDevolucion.CurrentRow.Cells[22].Value.ToString();






        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (GridViewDevolucion.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("¿Esta seguro que desea eliminar el registro?", "Alerta", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {

                    string cod = GridViewDevolucion.CurrentRow.Cells[0].Value.ToString();

                    SqlCommand cmd = new SqlCommand("EliminarDevolucion", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@codevoalquiler", cod);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    MessageBox.Show("Registro Eliminado Correctamente", "Borrar", MessageBoxButtons.OK, MessageBoxIcon.Information);



                    LLenarGrid(GridViewDevolucion);
                }
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string observacion = txtObservacion.Text;
            int cod = Convert.ToInt32(txtCodDevl.Text); 


            try
            {
                SqlCommand cmd1 = new SqlCommand("ActualizarDevolucion", cn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@cod", cod);
                cmd1.Parameters.AddWithValue("@observacion", observacion);
                SqlDataAdapter sda = new SqlDataAdapter(cmd1);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                LLenarGrid(GridViewDevolucion);
                btnGuardar.Visible = false;
                btnBorrar.Visible = false;
                btnVer.Visible = true;
                btnEditar.Visible = true;
                btnEliminar.Visible = true;
                borrar();

                //mensaje de confirmacion
                MessageBox.Show("Actualizado correctamente", "Actualizacion de Devolucion", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error, datos no válidos" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string busqueda = " ";
            switch (cboBuscar.Text)
            {
                case "Cod. Devolucion":
                    busqueda = "codevoalquiler";
                    break;

                case "Cod. Alquiler":
                    busqueda = "codentrealquiler";
                    break;

                case "Nombre Cliente":
                    busqueda = "nombrec";
                    break;
                case "Dui":
                    busqueda = "DUI";
                    break;

                case "Placa":
                    busqueda = "placa";
                    break;
                    
            }
            if (string.IsNullOrEmpty(txtBusqueda.Text) || cboBuscar.Text == "Seleccione...")
            {
                MessageBox.Show("Debe seleccionar y escribir algo para buscar", "Buscar", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {
                SqlCommand co = new SqlCommand("select codevoalquiler as 'ID devolucion', codentrealquiler as 'Id alquiler', nombrec as 'Nombres', apellidoc as 'Apellidos',DUI as 'DUI', numlicencia AS 'Licencia', tipoauto as 'Tipo Auto', marca as 'Marca', modelo as 'Modelo', placa as 'Placa', diasalida as 'Fecha Salida', diaentrega as 'Fecha Entrega', diasalquilado as 'Dias alquilados', diaentrada as 'Fecha entrada', ttlDiasAlquilados as 'Total dias retraso', precio as 'Precio', mora as 'Mora', depositoSeguro as 'Deposito Seguro', costodaño as 'Costo Daño', devloseguro as 'Devolucion seguro', pagoExtraDaño as 'Pagos Extras', totalpagar 'Total de pagos', observacion as 'Observacion' from devolucion where estado = 'disponible' and " + busqueda + " like '%" + txtBusqueda.Text + "%'", cn);
                SqlDataAdapter sd = new SqlDataAdapter(co);
                DataTable dt = new DataTable();
                sd.Fill(dt);
                GridViewDevolucion.DataSource = dt;

            }
        }

        private void cboBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            Reportes.ReporteDevolucionAdmin rep = new Reportes.ReporteDevolucionAdmin();
            rep.ShowDialog();
        }
    }
}
