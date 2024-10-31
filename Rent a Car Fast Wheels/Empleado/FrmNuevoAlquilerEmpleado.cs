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
    public partial class FrmNuevoAlquilerEmpleado : Form
    {
        string codEm;
        public FrmNuevoAlquilerEmpleado(string cod)
        {
            InitializeComponent();

            codEm = cod;
            txtCodEmpleado.Text = codEm.ToString();
        }
        SqlConnection cn = new SqlConnection("Data Source=localhost; Initial Catalog = rentcar; Integrated Security=True;");
        public DataTable dt;
        public SqlDataAdapter sda;
        public SqlDataReader dr;

        void CalcularFechas()
        {
            DateTime fechaSalida = TimeSalida.Value.Date;
            DateTime fechaEntrada = TimeEntrada.Value.Date;
            TimeSpan diferenciaDias = fechaEntrada - fechaSalida;
            int totalDias = diferenciaDias.Days;
            if (totalDias < 0 || totalDias == 0) 
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

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSeguro.Text)|| string.IsNullOrEmpty(txtPrecio.Text))
            {
                MessageBox.Show("Debe llenar los campos para hacer los cálculos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }
            else
            {
                btnCancelar.Visible = true;
                btnGuardar.Visible = true;
                CalcularFechas();
            }

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            

            //condicion para que ningun text box este vacio o ningun combo box este sin seleccionar
            if (string.IsNullOrEmpty(txtMarca.Text) || string.IsNullOrEmpty(txtModelo.Text)
               || string.IsNullOrEmpty(txtTipo.Text) || string.IsNullOrEmpty(txtCodAuto.Text) ||
               string.IsNullOrEmpty(txtPlaca.Text) || string.IsNullOrEmpty(txtPrecio.Text)
               || string.IsNullOrEmpty(txtCodC.Text) || string.IsNullOrEmpty(txtNombrec.Text)
               || string.IsNullOrEmpty(txtApellidoc.Text) || string.IsNullOrEmpty(txtDui.Text)
               || string.IsNullOrEmpty(txtLicencia.Text)
               || string.IsNullOrEmpty(TimeSalida.Text)
               || string.IsNullOrEmpty(txtCantidadDias.Text) || string.IsNullOrEmpty(TimeEntrada.Text)
               || string.IsNullOrEmpty(txtSeguro.Text) || string.IsNullOrEmpty(txtCodEmpleado.Text)
                )
            {
                //mensaje de error
                MessageBox.Show("Debe llenar los campos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }
            else
            {
                SqlCommand cmd2 = new SqlCommand("IdMaxAlquiler", cn);
                SqlDataAdapter sda2 = new SqlDataAdapter(cmd2);
                DataTable dt2 = new DataTable();
                sda2.Fill(dt2);
                string id = (dt2.Rows[0][0].ToString());
                if (id == "")
                {
                    id = "1";
                }
                //declaracion de variables que se enviaran como parametros al procedimiento almacenado de sql
                string codC = txtCodC.Text;
                string codA = txtCodAuto.Text;
                string codE = txtCodEmpleado.Text;
                string nombre = txtNombrec.Text;
                string apellido = txtApellidoc.Text;
                string dui = txtDui.Text;
                string licencia = txtLicencia.Text;
                string tipo = txtTipo.Text;
                string marca = txtMarca.Text;
                string modelo = txtModelo.Text;
                string placa = txtPlaca.Text;
                decimal precio = Convert.ToDecimal(txtPrecio.Text);
                DateTime diaretiro = TimeSalida.Value.Date;
                DateTime diaentrada = TimeEntrada.Value.Date;
                string diasalquilado = txtCantidadDias.Text;
                decimal seguro = Convert.ToDecimal(txtSeguro.Text);
                decimal total = Convert.ToDecimal(txtTotalAlquilar.Text);
                decimal totalGlobal = Convert.ToDecimal(txtTotalGlobal.Text);

                try
                {

                        SqlCommand cmd1 = new SqlCommand("AgregarAlquiler", cn);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.AddWithValue("@codcliente", codC);
                        cmd1.Parameters.AddWithValue("@nombrec", nombre);
                        cmd1.Parameters.AddWithValue("@apellidoc", apellido);
                        cmd1.Parameters.AddWithValue("@DUI", dui);
                        cmd1.Parameters.AddWithValue("@numlicencia", licencia);
                        cmd1.Parameters.AddWithValue("@codigoauto", codA);
                        cmd1.Parameters.AddWithValue("@tipoauto", tipo);
                        cmd1.Parameters.AddWithValue("@marca", marca);
                        cmd1.Parameters.AddWithValue("@modelo", modelo);
                        cmd1.Parameters.AddWithValue("@placa", placa); 
                        cmd1.Parameters.AddWithValue("@diaretiro", diaretiro);
                        cmd1.Parameters.AddWithValue("@diaentrada", diaentrada);
                        cmd1.Parameters.AddWithValue("@diasalquilado", diasalquilado);
                        cmd1.Parameters.AddWithValue("@precio", precio);
                        cmd1.Parameters.AddWithValue("@total", total);
                        cmd1.Parameters.AddWithValue("@depositoSeguro", seguro);
                        cmd1.Parameters.AddWithValue("@totalGlobal", totalGlobal);
                        cmd1.Parameters.AddWithValue("@codempleado", codE);

                        SqlDataAdapter sda = new SqlDataAdapter(cmd1);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        MessageBox.Show("Ingresado correctamente", "Ingreso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    GridViewAlquiler.Rows.Add(id,txtNombrec.Text, txtApellidoc.Text, txtDui.Text, txtLicencia.Text, txtTipo.Text, txtPlaca.Text, txtMarca.Text, txtModelo.Text,
                diaretiro, diaentrada, txtCantidadDias.Text, txtPrecio.Text, txtTotalAlquilar.Text, txtSeguro.Text, txtTotalGlobal.Text);
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error, datos no válidos" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            
            btnGuardar.Visible = false;
            btnCancelar.Visible = false;
            btnCancelar.Visible = false;
            Limpiar();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void Limpiar()
        {
            txtCodC.Text = "";
            txtDui.Text = "";
            txtNombrec.Text = "";
            txtApellidoc.Text = "";
            txtLicencia.Text = "";
            //txtCodEmpleado.Text = "";
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

        private void btnAgregarCliente_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCodC.Text))
            {
                MessageBox.Show("Debe llenar el campo para buscar los respectivos datos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }
            else
            {
                int c;
                c = Convert.ToInt32(txtCodC.Text);

                SqlCommand cmd = new SqlCommand("BuscarCliente", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@codCliente", c);
                dt = new DataTable();
                sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);

                if (dt.Rows.Count == 1)
                {
                    txtCodC.Text = dt.Rows[0][0].ToString();
                    txtNombrec.Text = dt.Rows[0][1].ToString();
                    txtApellidoc.Text = dt.Rows[0][2].ToString();
                    txtDui.Text = dt.Rows[0][3].ToString();
                    txtLicencia.Text = dt.Rows[0][4].ToString();
                }
                else
                {
                    MessageBox.Show("No se encontro ningun registro con ese codigo", "Sin resultados", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnAgregarAuto_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCodAuto.Text))
            {
                MessageBox.Show("Debe llenar el campo para buscar los respectivos datos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }
            else
            {
                int c;
                c = Convert.ToInt32(txtCodAuto.Text);

                SqlCommand cmd = new SqlCommand("BuscarAuto", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@codAuto", c);
                dt = new DataTable();
                sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                

                if (dt.Rows.Count == 1)
                {
                    string estado = dt.Rows[0][10].ToString().ToLower();
                    if (estado == "disponible" )
                    {
                        txtCodAuto.Text = dt.Rows[0][0].ToString();
                        txtTipo.Text = dt.Rows[0][1].ToString();
                        txtMarca.Text = dt.Rows[0][2].ToString();
                        txtModelo.Text = dt.Rows[0][3].ToString();
                        txtPlaca.Text = dt.Rows[0][8].ToString();
                        txtPrecio.Text = dt.Rows[0][9].ToString();
                    }
                    else
                    {
                        //MessageBox.Show("Error en las fechas, por favor vuelve a ingresarlas nuevamente", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        MessageBox.Show("El auto que solicita no esta disponible", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }



                }
                else
                {
                    MessageBox.Show("No se encontro ningun registro con ese codigo", "Sin resultados", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
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

        private void txtCodC_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtCodAuto_KeyPress(object sender, KeyPressEventArgs e)
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

        private void btnReporte_Click(object sender, EventArgs e)
        {
            ReportesEmpleado.ReporteAlquiler rep = new ReportesEmpleado.ReporteAlquiler();
            for (int i = 0; i < GridViewAlquiler.Rows.Count; i++)
            {
                ReportesEmpleado.DatosAlquiler dat = new ReportesEmpleado.DatosAlquiler();

                dat.codigoauto = (string)(this.GridViewAlquiler.Rows[i].Cells[0].Value);
                dat.nombre = (string)(this.GridViewAlquiler.Rows[i].Cells[1].Value);
                dat.apellido = (string)(this.GridViewAlquiler.Rows[i].Cells[2].Value);
                dat.dui = (string)(this.GridViewAlquiler.Rows[i].Cells[3].Value);
                dat.licencia = (string)(this.GridViewAlquiler.Rows[i].Cells[4].Value);
                dat.tipo = (string)(this.GridViewAlquiler.Rows[i].Cells[5].Value);
                dat.placa = (string)(this.GridViewAlquiler.Rows[i].Cells[6].Value);
                dat.marca = (string)(this.GridViewAlquiler.Rows[i].Cells[7].Value);
                dat.modelo = (string)(this.GridViewAlquiler.Rows[i].Cells[8].Value);
                dat.diaretiro = Convert.ToString(this.GridViewAlquiler.Rows[i].Cells[9].Value);
                dat.diaentrada = Convert.ToString(this.GridViewAlquiler.Rows[i].Cells[10].Value);
                dat.diasalquilado = (string)(this.GridViewAlquiler.Rows[i].Cells[11].Value);
                dat.precio = Convert.ToDecimal(this.GridViewAlquiler.Rows[i].Cells[12].Value);
                dat.seguro = Convert.ToDecimal(this.GridViewAlquiler.Rows[i].Cells[14].Value);
                dat.totalglobal = Convert.ToDecimal(this.GridViewAlquiler.Rows[i].Cells[15].Value);

                rep.Datos.Add(dat);
            }
            rep.ShowDialog();
            GridViewAlquiler.Rows.Clear();

        }
    }
}

