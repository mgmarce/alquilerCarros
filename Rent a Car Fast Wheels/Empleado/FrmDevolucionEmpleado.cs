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
    public partial class FrmDevolucionEmpleado : Form
    {
        SqlConnection cn = new SqlConnection("Data Source=localhost; Initial Catalog = rentcar; Integrated Security=True;");
        public DataTable dt;
        public SqlDataAdapter sda;
        public SqlDataReader dr;

        public FrmDevolucionEmpleado()
        {
            InitializeComponent();
            /*txtDiaRetiro.Text = DateTime.Now.ToString("dd/MM/yyyy");
            DateTime fechaActual;
            fechaActual = Convert.ToDateTime(txtDiaRetiro.Text);*/

        }

        private void btnBuscarCod_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCod.Text))
            {
                MessageBox.Show("Debe llenar los campos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                try
                {
                    int c = Convert.ToInt32(txtCod.Text);
                    SqlCommand cmd = new SqlCommand("BuscarAlquiler", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@codalquiler", c);
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
                    string modo = dt.Rows[0][21].ToString();
                    if (modo == "Activo")
                    {
                        //CLIENTES
                        txtNomCliente.Text = dt.Rows[0][3].ToString();
                        txtApellidos.Text = dt.Rows[0][4].ToString();
                        txtDui.Text = dt.Rows[0][5].ToString();
                        txtLicencia.Text = dt.Rows[0][6].ToString();
                        //AUTOS
                        txtTipoAuto.Text = dt.Rows[0][8].ToString();
                        txtMarca.Text = dt.Rows[0][9].ToString();
                        txtModelo.Text = dt.Rows[0][10].ToString();
                        txtPlaca.Text = dt.Rows[0][11].ToString();
                        //DATOS ALQUILER
                        txtDiaRetiro.Text = dt.Rows[0][13].ToString();
                        txtDiaEntrega.Text = dt.Rows[0][14].ToString();
                        txtDiasAlquilados.Text = dt.Rows[0][15].ToString();
                        txtPrecio.Text = dt.Rows[0][16].ToString();
                        txtSeguro.Text = dt.Rows[0][18].ToString();

                        string fechaHoy = DateTime.Now.ToString("dd/MM/yyyy");
                        DateTime fechaEntregaReal = Convert.ToDateTime(fechaHoy), fechaEntrega = Convert.ToDateTime(txtDiaEntrega.Text);

                        TimeSpan diferencia = fechaEntregaReal - fechaEntrega;
                        int diferenciaDias = diferencia.Days;

                        if (diferenciaDias > 0)
                        {
                            decimal mora, precio = Convert.ToDecimal(txtPrecio.Text);
                            mora = diferenciaDias * precio;
                            txtTotalDiasAl.Visible = true;
                            label17.Visible = true;
                            txtTotalDiasAl.Text = diferenciaDias.ToString();
                            label19.Visible = true;
                            txtMora.Visible = true;
                            txtMora.Text = mora.ToString();
                        }
                        else
                        {
                            txtMora.Text = 0.ToString();
                            txtTotalDiasAl.Text = 0.ToString();
                        }
                        btnCalcular.Visible = true;
                        btnCancelar.Visible = true;
                    }
                    else
                    {
                        MessageBox.Show("La devolucion del alquiler ya fue emitida", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                else
                {
                    MessageBox.Show("No se encontro ningun registro con ese codigo", "Sin resultado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Limpiar();
            btnAgregar.Visible = false;
            btnCancelar.Visible = false;
            btnCalcular.Visible = false;
            
        }


        void Limpiar()
        {
            txtCod.Text = "";
            txtDiaEntrega.Text = "";
            txtDiaRetiro.Text = "";
            txtDiasAlquilados.Text = "";
            txtTotalDiasAl.Text = "";
            txtPrecio.Text = "";
            txtMora.Text = "";
            txtSeguro.Text = "";
            txtDevSeguro.Text = "";
            txtCostoAccidente.Text = "";
            txtExtra.Text = "";
            txtTotalPagar.Text = "";
            txtObservacion.Text = "";
            txtTotalDiasAl.Visible = false;
            label17.Visible = false;
            txtMora.Visible = false;
            label19.Visible = false;
            lblE.Visible = false;
            txtExtra.Visible = false;
            label20.Visible = false;
            txtTotalPagar.Visible = false;
            
            txtNomCliente.Text = "";
            txtApellidos.Text = "";
            txtLicencia.Text = "";
            txtDui.Text = "";
            txtTipoAuto.Text = "";
            txtMarca.Text = "";
            txtPlaca.Text = "";
            txtModelo.Text = "";

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCod.Text))
                {
                    MessageBox.Show("Debe llenar los campos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }
                else
                {
                    btnCalcular.Visible = false;
                    
                    int codAlquiler = Convert.ToInt32(txtCod.Text), diasAlquilados = Convert.ToInt32(txtDiasAlquilados.Text), ttlDiasRetraso= Convert.ToInt32(txtTotalDiasAl.Text);
                    string fechaSalida = txtDiaRetiro.Text, fechaEntrega = txtDiaEntrega.Text, fechaEntrada = DateTime.Now.ToString("dd/MM/yyyy"), observacion = txtObservacion.Text, 
                        nombre = txtNomCliente.Text, apellidos = txtApellidos.Text, dui=txtDui.Text, licencia=txtLicencia.Text, 
                        tipoAuto=txtTipoAuto.Text, marca = txtMarca.Text, placa = txtPlaca.Text, modelo = txtModelo.Text;
                    decimal precioDia = Convert.ToDecimal(txtPrecio.Text), mora=Convert.ToDecimal(txtMora.Text), 
                        depositoSeguro = Convert.ToDecimal(txtSeguro.Text), CostoAccidente = Convert.ToDecimal(txtCostoAccidente.Text), 
                        DevolSeguro=Convert.ToDecimal(txtDevSeguro.Text), PagoExtra = Convert.ToDecimal(txtExtra.Text), TotalPagar=Convert.ToDecimal(txtTotalPagar.Text);


                    SqlCommand cmd = new SqlCommand("IngresarDevolucion", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@codentrealquiler", codAlquiler);
                    cmd.Parameters.AddWithValue("@nombrec", nombre);
                    cmd.Parameters.AddWithValue("@apellidoc", apellidos);
                    cmd.Parameters.AddWithValue("@DUI", dui);
                    cmd.Parameters.AddWithValue("@numlicencia", licencia);
                    cmd.Parameters.AddWithValue("@tipoauto", tipoAuto);
                    cmd.Parameters.AddWithValue("@marca", marca);
                    cmd.Parameters.AddWithValue("@modelo", modelo);
                    cmd.Parameters.AddWithValue("@placa", placa);
                    cmd.Parameters.AddWithValue("@diasalida", fechaSalida);
                    cmd.Parameters.AddWithValue("@diaentrega", fechaEntrega);
                    cmd.Parameters.AddWithValue("@diasalquilado", diasAlquilados);
                    cmd.Parameters.AddWithValue("@diaentrada", fechaEntrada);
                    cmd.Parameters.AddWithValue("@ttlDiasAlquilados", ttlDiasRetraso);
                    cmd.Parameters.AddWithValue("@precio", precioDia);
                    cmd.Parameters.AddWithValue("@mora", mora);
                    cmd.Parameters.AddWithValue("@depositoSeguro", depositoSeguro);
                    cmd.Parameters.AddWithValue("@costodaño", CostoAccidente);
                    cmd.Parameters.AddWithValue("@devloseguro", DevolSeguro);
                    cmd.Parameters.AddWithValue("@pagoExtraDaño", PagoExtra);
                    cmd.Parameters.AddWithValue("@totalPagar", TotalPagar);
                    cmd.Parameters.AddWithValue("@observacion", observacion);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    Limpiar();
                    btnCancelar.Visible = false;
                    btnAgregar.Visible = false;
                    btnCalcular.Visible = false;
                    GridViewDevolucion.Rows.Add(codAlquiler, nombre, apellidos, dui, licencia, tipoAuto, marca, modelo, placa, fechaSalida + " - " + fechaEntrega, diasAlquilados,
                        fechaEntrada, ttlDiasRetraso, precioDia, mora, depositoSeguro, CostoAccidente, DevolSeguro, PagoExtra, TotalPagar, observacion);
                    MessageBox.Show("Registro ingresado corractemente", "Ingreso de Devolucion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error en la conexion, No se pudo obtener la informacion" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            if (txtCostoAccidente.Text == "")
            {
                txtCostoAccidente.Text = 0.ToString();
            }
            
            if (txtDevSeguro.Text == "")
            {
                txtDevSeguro.Text = 0.ToString();
            }

            if (txtTotalPagar.Text == "")
            {
                txtTotalPagar.Text = 0.ToString();
            }

            decimal CostoAccidente, CostoSeguro, DevolucionSeguro, SeguroExtra = 0, total, mora = Convert.ToDecimal(txtMora.Text);
            //decimal DiasAdelantados = Convert.ToDecimal(txtDevolAntes.Text);  ///POR ACÁ
            CostoAccidente = Convert.ToDecimal(txtCostoAccidente.Text);
            CostoSeguro = Convert.ToDecimal(txtSeguro.Text);
            btnAgregar.Visible = true;
            DevolucionSeguro = CostoSeguro - CostoAccidente;

            if (DevolucionSeguro < 0)
            {
                SeguroExtra = DevolucionSeguro * -1;
                txtDevSeguro.Text = 0.ToString();
                txtExtra.Text = SeguroExtra.ToString();
                lblE.Visible = true;
                txtExtra.Visible = true;
            }
            else
            {

                txtDevSeguro.Text = DevolucionSeguro.ToString();
                txtExtra.Text = 0.ToString();
                lblE.Visible = false;
                txtExtra.Visible = false;
            }

            total = mora + SeguroExtra;

            if (total > 0)
            {
                label20.Visible = true;
                txtTotalPagar.Visible = true;
                txtTotalPagar.Text = total.ToString();
            }


        }

        private void txtCod_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
              (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            ReportesEmpleado.ReporteDevolucion rep = new ReportesEmpleado.ReporteDevolucion();
            for (int i = 0; i < GridViewDevolucion.Rows.Count; i++)
            {
                ReportesEmpleado.DatosDevolucion dat = new ReportesEmpleado.DatosDevolucion();

                dat.codAlquiler = Convert.ToString(this.GridViewDevolucion.Rows[i].Cells[0].Value);
                dat.nombre = (string)(this.GridViewDevolucion.Rows[i].Cells[1].Value);
                dat.apellidos = (string)(this.GridViewDevolucion.Rows[i].Cells[2].Value);
                dat.dui = (string)(this.GridViewDevolucion.Rows[i].Cells[3].Value);
                dat.licencia = (string)(this.GridViewDevolucion.Rows[i].Cells[4].Value);
                dat.tipoAuto = (string)(this.GridViewDevolucion.Rows[i].Cells[5].Value);
                dat.marca = (string)(this.GridViewDevolucion.Rows[i].Cells[6].Value);
                dat.modelo = (string)(this.GridViewDevolucion.Rows[i].Cells[7].Value);
                dat.placa = (string)(this.GridViewDevolucion.Rows[i].Cells[8].Value);
                dat.fechasAlquiler = (string)(this.GridViewDevolucion.Rows[i].Cells[9].Value);
                dat.diasAlquilados = Convert.ToString(this.GridViewDevolucion.Rows[i].Cells[10].Value);
                dat.diasEntrada = (string)(this.GridViewDevolucion.Rows[i].Cells[11].Value);
                dat.ttlDiasRetraso = Convert.ToString(this.GridViewDevolucion.Rows[i].Cells[12].Value);
                dat.precio = Convert.ToString(this.GridViewDevolucion.Rows[i].Cells[13].Value);
                dat.mora = Convert.ToString(this.GridViewDevolucion.Rows[i].Cells[14].Value);
                dat.depositoSeguro = Convert.ToString(this.GridViewDevolucion.Rows[i].Cells[15].Value);
                dat.costoDaño = Convert.ToString(this.GridViewDevolucion.Rows[i].Cells[16].Value);
                dat.devlSegusro = Convert.ToString(this.GridViewDevolucion.Rows[i].Cells[17].Value);
                dat.pagoExtra = Convert.ToString(this.GridViewDevolucion.Rows[i].Cells[18].Value);
                dat.ttlPagar = Convert.ToString(this.GridViewDevolucion.Rows[i].Cells[19].Value);
                dat.observacion = (string)(this.GridViewDevolucion.Rows[i].Cells[20].Value);
                rep.Datos.Add(dat);
            }
            rep.ShowDialog();
            GridViewDevolucion.Rows.Clear();
        }
    }
    
}
