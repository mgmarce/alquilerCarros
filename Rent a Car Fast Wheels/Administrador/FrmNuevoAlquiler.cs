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
    public partial class FrmNuevoAlquiler : Form
    {
        public FrmNuevoAlquiler()
        {
            InitializeComponent();
            //llamamos al metodo de llenado de grid view para llenar el grid view cuando el formulario se cargue
            LLenarGrid(GridViewAlquiler);
        }
        SqlConnection cn = new SqlConnection("Data Source=localhost; Initial Catalog = rentcar; Integrated Security=True;");


        public DataTable dt;
        public SqlDataAdapter sda;
        public SqlDataReader dr;

        //evento que se incia al carga el formulario
        void CalcularFechas()
        {
            if (string.IsNullOrEmpty(txtSeguro.Text))
            {
                MessageBox.Show("Debe llenar el campo Seguro para hacer los cálculos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }
            else
            {
                DateTime fechaSalida = TimeSalida.Value.Date;
                DateTime fechaEntrada = TimeEntrada.Value.Date;
                TimeSpan diferenciaDias = fechaEntrada - fechaSalida;
                int totalDias = diferenciaDias.Days;
                if (totalDias < 0)
                {
                    MessageBox.Show("Error en las fechas, por favor vuelve a ingresarlas nuevamente", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    txtCantidadDias.Text = totalDias.ToString();
                    lblDias.Visible = true;
                    txtCantidadDias.Visible = true;
                    decimal precio = Convert.ToDecimal(txtPrecio.Text), totalAlquilar, DepositoSeguro = Convert.ToDecimal(txtSeguro.Text), totalGlobal;
                    totalAlquilar = totalDias * precio;
                    txtTotalAlquilar.Text = totalAlquilar.ToString();
                    totalGlobal = totalAlquilar + DepositoSeguro;
                    txtTotalGlobal.Text = totalGlobal.ToString();
                }

            }

        }

        public void LLenarGrid(DataGridView dgv)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("ListarAlquileres", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                dt = new DataTable();
                sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);

                dgv.DataSource = dt;

            }
            catch (Exception ex)
            {
                //mensajes de error
                MessageBox.Show("Error en la conexion, No se pudo obtener la informacion" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnCalcular_Click(object sender, EventArgs e)
        {
            btnCancelar.Visible = true;
            btnActualizar.Visible = true;
            CalcularFechas();

        }


        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Limpiar();
            btnCancelar.Visible = false;
            btnActualizar.Visible = false;
            btnEliminar.Visible = true;
            btnEditar.Visible = true;
            btnCalcular.Visible = false;
        }

        private void Limpiar()
        {
            txtCodigo.Text = "";
            txtCodC.Text = "";
            txtDui.Text = "";
            txtNombrec.Text = "";
            txtApellidoc.Text = "";
            txtLicencia.Text = "";
            txtCodEmpleado.Text = "";
            txtCodAuto.Text = "";
            txtMarca.Text = "";
            txtModelo.Text = "";
            txtTipo.Text = "";
            txtPlaca.Text = "";

            TimeSalida.Value = DateTime.Today;
            TimeEntrada.Value = DateTime.Today;
            txtPrecio.Text = "";
            txtSeguro.Text = "";
            txtCantidadDias.Text = "";
            txtTotalAlquilar.Text = "";
            txtTotalGlobal.Text = "";

        }

        private void btnActualizar_Click_1(object sender, EventArgs e)
        {
            //nos aseguramos que todos los textbox esten llenos y los combo box esten seleccionados con algo
            if (string.IsNullOrEmpty(TimeSalida.Text)
               || string.IsNullOrEmpty(TimeEntrada.Text) || string.IsNullOrEmpty(txtSeguro.Text)
               || string.IsNullOrEmpty(txtPrecio.Text) || string.IsNullOrEmpty(txtTotalGlobal.Text)
               || string.IsNullOrEmpty(txtTotalAlquilar.Text)
                )
            {
                //mensaje de error
                MessageBox.Show("Debe llenar los campos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }
            //sino dejamos que guarde los cambios
            else
            {
                string codigo = txtCodigo.Text;
                decimal precio = Convert.ToDecimal(txtPrecio.Text);
                DateTime diaretiro = TimeSalida.Value.Date;
                DateTime diaentrada = TimeEntrada.Value.Date;
                string diasalquilado = txtCantidadDias.Text;
                decimal seguro = Convert.ToDecimal(txtSeguro.Text);
                decimal total = Convert.ToDecimal(txtTotalAlquilar.Text);
                decimal totalGlobal = Convert.ToDecimal(txtTotalGlobal.Text);


                try
                {
                    SqlCommand cmd1 = new SqlCommand("ActualizarAlquiler", cn);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.AddWithValue("@codalquiler", codigo);
                    cmd1.Parameters.AddWithValue("@diaretiro", diaretiro);
                    cmd1.Parameters.AddWithValue("@diaentrada", diaentrada);
                    cmd1.Parameters.AddWithValue("@diasalquilado", diasalquilado);
                    cmd1.Parameters.AddWithValue("@precio", precio);
                    cmd1.Parameters.AddWithValue("@total", total);
                    cmd1.Parameters.AddWithValue("@depositoSeguro", seguro);
                    cmd1.Parameters.AddWithValue("@totalGlobal", totalGlobal);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd1);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    LLenarGrid(GridViewAlquiler);

                    //otra vez ocultamos el textbox de codigo y el boton guardar cambios, y hacemos visible el de agregar nuevo
                    btnActualizar.Visible = false;
                    Limpiar();

                    //mensaje de confirmacion
                    MessageBox.Show("Actualizado correctamente", "Actualizacion de Alquiler", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error, datos no válidos" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                btnEditar.Visible = true;
                btnEliminar.Visible = true;
                btnCancelar.Visible = false;
                btnActualizar.Visible = false;
                btnCalcular.Visible = false;
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (GridViewAlquiler.SelectedRows.Count == 1)
            {
                string modo = GridViewAlquiler.CurrentRow.Cells[19].Value.ToString();
            
                if (modo != "Activo")
                {
                    if (MessageBox.Show("¿Esta seguro que desea eliminar el registro?", "Alerta", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        string cod = GridViewAlquiler.CurrentRow.Cells[0].Value.ToString();

                        SqlCommand cmd = new SqlCommand("EliminarAlquiler", cn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@codalquiler", cod);
                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        MessageBox.Show("Alquiler Eliminado Correctamente", "Borrar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LLenarGrid(GridViewAlquiler);
                    }
                }
                else
                {
                    MessageBox.Show("El alquiler aun no fue emitido, no se puede eliminar", "Edicion no posible", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        { 
            if (GridViewAlquiler.SelectedRows.Count == 1)
            {
                string modo= GridViewAlquiler.CurrentRow.Cells[19].Value.ToString(); 

                if (modo == "Activo")
                {
                    txtCodigo.Text = GridViewAlquiler.CurrentRow.Cells[0].Value.ToString();
                    txtCodEmpleado.Text = GridViewAlquiler.CurrentRow.Cells[1].Value.ToString();
                    txtCodC.Text = GridViewAlquiler.CurrentRow.Cells[2].Value.ToString();
                    txtNombrec.Text = GridViewAlquiler.CurrentRow.Cells[3].Value.ToString();
                    txtApellidoc.Text = GridViewAlquiler.CurrentRow.Cells[4].Value.ToString();
                    txtDui.Text = GridViewAlquiler.CurrentRow.Cells[5].Value.ToString();
                    txtLicencia.Text = GridViewAlquiler.CurrentRow.Cells[6].Value.ToString();
                    txtCodAuto.Text = GridViewAlquiler.CurrentRow.Cells[7].Value.ToString();
                    txtTipo.Text = GridViewAlquiler.CurrentRow.Cells[8].Value.ToString();
                    txtMarca.Text = GridViewAlquiler.CurrentRow.Cells[9].Value.ToString();
                    txtModelo.Text = GridViewAlquiler.CurrentRow.Cells[10].Value.ToString();
                    txtPlaca.Text = GridViewAlquiler.CurrentRow.Cells[11].Value.ToString();
                    TimeSalida.Text = GridViewAlquiler.CurrentRow.Cells[12].Value.ToString();
                    TimeEntrada.Text = GridViewAlquiler.CurrentRow.Cells[13].Value.ToString();
                    txtPrecio.Text = GridViewAlquiler.CurrentRow.Cells[15].Value.ToString();
                    txtSeguro.Text = GridViewAlquiler.CurrentRow.Cells[17].Value.ToString();
                    txtCantidadDias.Text = GridViewAlquiler.CurrentRow.Cells[14].Value.ToString();
                    txtTotalAlquilar.Text = GridViewAlquiler.CurrentRow.Cells[16].Value.ToString();
                    txtTotalGlobal.Text = GridViewAlquiler.CurrentRow.Cells[18].Value.ToString();
                    btnActualizar.Visible = true;
                    btnEliminar.Visible = false;
                    btnCalcular.Visible = true;
                    btnCancelar.Visible = true;
                    btnEditar.Visible = false;

                }
                else
                {
                    MessageBox.Show("El alquiler ya fue emitido, no se pueden hacer cambios", "Edicion no posible", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            else
                MessageBox.Show("Seleccione una fila por favor", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string busqueda = " ";
            switch (cboBuscar.Text)
            {
                case "Codigo Alquiler":
                    busqueda = "codentrealquiler";
                    break;

                case "Codigo Empleado":
                    busqueda = "codempleado";
                    break;

                case "Codigo Cliente":
                    busqueda = "codcliente";
                    break;
                case "Codigo Auto":
                    busqueda = "codigoauto";
                    break;
            }
            if (string.IsNullOrEmpty(txtBusqueda.Text) || cboBuscar.Text == "Seleccione...")
            {
                MessageBox.Show("Debe seleccionar y escribir algo para buscar", "Buscar", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {
                SqlCommand co = new SqlCommand("select codentrealquiler as 'ID', codempleado as 'Cod. empleado',codcliente as 'Codigo Cliente', nombrec as 'Nombre', apellidoc as 'Apellido',DUI as 'DUI', numlicencia as 'N° Licencia', codigoauto as 'Codigo Auto',tipoauto as 'Tipo auto', marca as 'Marca', modelo as 'Modelo', placa as 'Placa', diaretiro as 'Fecha Retiro',diaentrada as 'Fecha entrada', diasalquilado as 'Dias por alquilar', precio as 'Precio por dia', total as 'Total por los dias',depositoSeguro as 'Deposito Seguro', totalGlobal as 'Total Global', modo as 'Modo' from alquiler where estado = 'disponible' and " + busqueda + " like '%" + txtBusqueda.Text + "%'", cn);
                SqlDataAdapter sd = new SqlDataAdapter(co);
                DataTable dt = new DataTable();
                sd.Fill(dt);
                GridViewAlquiler.DataSource = dt;

            }
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            LLenarGrid(GridViewAlquiler);
            cboBuscar.Text = "Seleccione...";
            txtBusqueda.Clear();
            btnActualizar.Visible = false;
            btnCancelar.Visible = false;
            btnEditar.Visible = true;
            btnCalcular.Visible = false;
        }
        private void soloNumeros_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void cboBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            Reportes.ReporteAlquilerAdmin rep = new Reportes.ReporteAlquilerAdmin();
            rep.ShowDialog();
        }
    }
    }
