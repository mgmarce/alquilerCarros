
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rent_a_Car_Fast_Wheels.Administrador
{
    public partial class FrmAutos : Form
    {
        public FrmAutos()
        {
            InitializeComponent();
        }

        SqlConnection cn = new SqlConnection("Data Source=localhost; Initial Catalog = rentcar; Integrated Security=True;");
       
        public DataTable dt;
        public SqlDataAdapter sda;
        public SqlDataReader dr;

        //evento que se incia al carga el formulario
        private void FrmAutos_Load(object sender, EventArgs e)
        {
            //llamamos al metodo de llenado de grid view para llenar el grid view cuando el formulario se cargue
            LLenarGrid(GridViewAutos);
        }

        //metodo para llenar el grid view cuando el formulario se cargue
        public void LLenarGrid(DataGridView dgv)
        {
            try
            {
                //llamamos al procedimiento almacenado
                SqlCommand cmd = new SqlCommand("ListarAutos", cn);
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

        //Metodo para vaciar los campos
        public void VaciarCampos()
        {

        
            txtCod.Clear();
            cboTipo.Text = "Seleccione...";
            txtMarca.Clear();
            txtModelo.Clear();
            txtColor.Clear();
            cboTransmision.Text = "Seleccione...";
            txtMotor.Clear();
            numericPasajeros.Text = "0";
            txtPlaca.Clear();
            txtPrecio.Clear();
            cboEstado.Text = "Seleccione...";
            txtMarca.Focus();
        }

        //metodo del boton cancelar
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            btnFicha.Visible = false;
            label12.Visible = false;
            cboEstado.Visible = false;
            imgfondo.Visible = true;
            foto.Image = null;
            btnAgregar.Visible = true;
            btnActualizar.Visible = false;
            txtCod.Visible = false;
            lblCodigo.Visible = false;
            VaciarCampos();
        }




        //metodo dentro del boton agregar
        private void btnAgregar_Click(object sender, EventArgs e)
        {
             //condicion para que ningun text box este vacio o ningun combo box este sin seleccionar
             if (foto.Image == null || cboTipo.Text == "Seleccione..." || string.IsNullOrEmpty(txtMarca.Text) || string.IsNullOrEmpty(txtModelo.Text)
                 || string.IsNullOrEmpty(txtColor.Text) || cboTransmision.Text == "Seleccione..." ||
                 string.IsNullOrEmpty(txtMotor.Text) || string.IsNullOrEmpty(numericPasajeros.Text) ||
                 string.IsNullOrEmpty(txtPlaca.Text) || string.IsNullOrEmpty(txtPrecio.Text))
                
             {
                 //mensaje de error
                 MessageBox.Show("Debe llenar los campos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                 return;
             }
             else
             {
                 //declaracion de variables que se enviaran como parametros al procedimiento almacenado de sql
                 string tipo = cboTipo.Text;
                 string marca = txtMarca.Text;
                 string modelo = txtModelo.Text;
                 string color = txtColor.Text;
                 string transmision = cboTransmision.Text;
                 string motor = txtMotor.Text;
                 string pasajeros = numericPasajeros.Text;
                 string placa = txtPlaca.Text.ToUpper();
                 string precio = txtPrecio.Text;
                 string estado = "Disponible";
                 PictureBox pbImagen = foto;

                 try
                 {
                     //llamamos al procedimiento buscar para que no deje ingresar un registro si ya existe
                     SqlCommand cmd = new SqlCommand("BuscarAutos", cn);
                     cmd.CommandType = CommandType.StoredProcedure;
                     //le mandamos parametros
                     cmd.Parameters.AddWithValue("@placa", placa);
                     dt = new DataTable();
                     sda = new SqlDataAdapter(cmd);
                     sda.Fill(dt);
                     //si encontro un registro existente envia un mensaje y no deja ingresar
                     if (dt.Rows.Count == 1)
                     {
                         //mesaje de error
                         MessageBox.Show("Ya existe un auto con esa placa", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                         //los txtbox de codigo los vaciamos y los ocultamos
                         txtCod.Clear();
                         txtCod.Visible = false;
                         lblCodigo.Visible = false;
                     }

                     //sino encontro ningun registron duplicado deja ingresar uno nuevo
                     else
                     {
                         //llamamos al procedimiento y le mandamos parametros
                         SqlCommand cmd1 = new SqlCommand("AgregarAuto", cn);
                         cmd1.CommandType = CommandType.StoredProcedure;
                         cmd1.Parameters.AddWithValue("@tipo", tipo);
                         cmd1.Parameters.AddWithValue("@marca", marca);
                         cmd1.Parameters.AddWithValue("@modelo", modelo);
                         cmd1.Parameters.AddWithValue("@color", color);
                         cmd1.Parameters.AddWithValue("@transmision", transmision);
                         cmd1.Parameters.AddWithValue("@motor", motor);
                         cmd1.Parameters.AddWithValue("@pasajeros", pasajeros);
                         cmd1.Parameters.AddWithValue("@placa", placa);
                         cmd1.Parameters.AddWithValue("@precio", precio);
                         cmd1.Parameters.AddWithValue("@estado", estado);
                         cmd1.Parameters.AddWithValue("@imagen",foto);

                         System.IO.MemoryStream ms = new System.IO.MemoryStream();
                         pbImagen.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                         cmd1.Parameters["@imagen"].Value = ms.GetBuffer();

                         SqlDataAdapter sda = new SqlDataAdapter(cmd1);
                         DataTable dt = new DataTable();
                         sda.Fill(dt);

                         //llenamos el datgrid otra vez para que se actualize con el nuevo registro
                         LLenarGrid(GridViewAutos);
                         //vaciamos los campos
                         VaciarCampos();

                         //mensaje de confirmacion
                         MessageBox.Show("Ingresado correctamente", "Ingreso de Auto", MessageBoxButtons.OK, MessageBoxIcon.Information);

                     }
                 }



                 catch (Exception ex)
                 {
                     MessageBox.Show("Error, datos no válidos" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 }
             }

            imgfondo.Visible = true;
            foto.Image.Dispose();
            foto.Image = null;
            txtCod.Clear();
            txtCod.Visible = false;
            lblCodigo.Visible = false;
            LLenarGrid(GridViewAutos);
            VaciarCampos();
        }

        //metodo dentro del boton editar que selecciona la fila del grid view
        private void btnEditar_Click(object sender, EventArgs e)
        {
            imgfondo.Visible = false;
            //comprobamos que el usuario selecciono una fila
            if (GridViewAutos.SelectedRows.Count == 1)
            {
                //le enviamos los datos que estan en las celdas de la fila seleccionada hacia los textbox para que los pueda editar
                txtCod.Text = GridViewAutos.CurrentRow.Cells[0].Value.ToString();
                cboTipo.Text = GridViewAutos.CurrentRow.Cells[1].Value.ToString();
                txtMarca.Text = GridViewAutos.CurrentRow.Cells[2].Value.ToString();
                txtModelo.Text = GridViewAutos.CurrentRow.Cells[3].Value.ToString();
                txtColor.Text = GridViewAutos.CurrentRow.Cells[4].Value.ToString();
                cboTransmision.Text = GridViewAutos.CurrentRow.Cells[5].Value.ToString();
                txtMotor.Text = GridViewAutos.CurrentRow.Cells[6].Value.ToString();
                numericPasajeros.Text = GridViewAutos.CurrentRow.Cells[7].Value.ToString();
                txtPlaca.Text = GridViewAutos.CurrentRow.Cells[8].Value.ToString();
                txtPrecio.Text = GridViewAutos.CurrentRow.Cells[9].Value.ToString();
                cboEstado.Text = GridViewAutos.CurrentRow.Cells[10].Value.ToString();
            

          
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



                //hacemos visible el texbox de codigo y el boton de editar, pero ocultamos el boton de agregar nuevo
                btnFicha.Visible = true;
                label12.Visible = true;
                cboEstado.Visible = true;
                btnAgregar.Visible = false;
                btnActualizar.Visible = true;
                txtCod.Visible = true;
                lblCodigo.Visible = true;



            }
            else
                //si no selecciono ninguna fila le mandamos un mensaje
                MessageBox.Show("Seleccione una fila por favor", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        //metodo dentro del boton actualizar osea el de guardar cambios
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            
            //nos aseguramos que todos los textbox esten llenos y los combo box esten seleccionados con algo
            if (foto.Image == null ||  cboTipo.Text == "Seleccione..." || string.IsNullOrEmpty(txtMarca.Text) || string.IsNullOrEmpty(txtModelo.Text)
               || string.IsNullOrEmpty(txtColor.Text) || cboTransmision.Text == "Seleccione..." ||
               string.IsNullOrEmpty(txtMotor.Text) || string.IsNullOrEmpty(numericPasajeros.Text) ||
               string.IsNullOrEmpty(txtPlaca.Text) || string.IsNullOrEmpty(txtPrecio.Text) ||
                cboTipo.Text == "Seleccione...")
            {
                //mensaje de error
                MessageBox.Show("Debe llenar los campos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }
            //sino dejamos que guarde los cambios
            else
            {
                string cod = txtCod.Text;
                string tipo = cboTipo.Text;
                string marca = txtMarca.Text;
                string modelo = txtModelo.Text;
                string color = txtColor.Text;
                string transmision = cboTransmision.Text;
                string motor = txtMotor.Text;
                string pasajeros = numericPasajeros.Text;
                string placa = txtPlaca.Text;
                decimal precio = Convert.ToDecimal(txtPrecio.Text);
                string estado = cboEstado.Text;
                PictureBox pbImagen = foto;

                try
                {
                    SqlCommand cmd1 = new SqlCommand("ActualizarAuto", cn);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.AddWithValue("@cod", cod);
                    cmd1.Parameters.AddWithValue("@tipo", tipo);
                    cmd1.Parameters.AddWithValue("@marca", marca);
                    cmd1.Parameters.AddWithValue("@modelo", modelo);
                    cmd1.Parameters.AddWithValue("@color", color);
                    cmd1.Parameters.AddWithValue("@transmision", transmision);
                    cmd1.Parameters.AddWithValue("@motor", motor);
                    cmd1.Parameters.AddWithValue("@pasajeros", pasajeros);
                    cmd1.Parameters.AddWithValue("@placa", placa);
                    cmd1.Parameters.AddWithValue("@precio", precio);
                    cmd1.Parameters.AddWithValue("@estado", estado);
                    cmd1.Parameters.AddWithValue("@imagen", foto);


                    System.IO.MemoryStream ms = new System.IO.MemoryStream();
                    pbImagen.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    cmd1.Parameters["@imagen"].Value = ms.GetBuffer();


                    SqlDataAdapter sda = new SqlDataAdapter(cmd1);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    LLenarGrid(GridViewAutos);


                    //otra vez ocultamos el textbox de codigo y el boton guardar cambios, y hacemos visible el de agregar nuevo
                    btnFicha.Visible = false;
                    label12.Visible = false;
                    cboEstado.Visible = false;
                    imgfondo.Visible = true;
                    foto.Image.Dispose();
                    foto.Image = null;
                    btnAgregar.Visible = true;
                    btnActualizar.Visible = false;
                    txtCod.Visible = false;
                    lblCodigo.Visible = false;
                    VaciarCampos();

                    //mensaje de confirmacion
                    MessageBox.Show("Actualizado correctamente", "Actualizacion de Auto", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error, datos no válidos" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        //Metodo del boton eliminar
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string cod = GridViewAutos.CurrentRow.Cells[0].Value.ToString();

            SqlCommand cmd1 = new SqlCommand("BusquedaAuto_alquiler", cn);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.AddWithValue("@cod", cod);
            SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            cn.Close();

            SqlCommand cmd2 = new SqlCommand("BusquedaAuto_mantenimiento", cn);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.AddWithValue("@cod", cod);
            SqlDataAdapter sda2 = new SqlDataAdapter(cmd2);
            DataTable dt2 = new DataTable();
            sda.Fill(dt2);

            if (dt1.Rows.Count >= 1)
            {
                MessageBox.Show("No se puede eliminar el auto, por que está relacionado con un Alquiler", "Borrar Auto", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            else if (dt2.Rows.Count >= 1)
            {
                MessageBox.Show("No se puede eliminar el auto, por que está ralacionado con un mantenimiento", "Borrar Auto", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

            else
            {

                if (GridViewAutos.SelectedRows.Count == 1)
                {
                    if (MessageBox.Show("¿Esta seguro que desea eliminar el registro?", "Alerta", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        //le enviamos el valor del codigo que este en la columna de la fila seleccionada




                        SqlCommand cmd = new SqlCommand("EliminarAuto", cn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@cod", cod);
                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);


                        MessageBox.Show("Auto Eliminado Correctamente", "Borrar Producto", MessageBoxButtons.OK, MessageBoxIcon.Information);



                        LLenarGrid(GridViewAutos);
                    }
                }

                else
                {

                    MessageBox.Show("Seleccione una fila por favor", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

        }

        //metodo de busqueda dentro del boton buscar
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            //declaramos una variable
            string busqueda = " ";

            //con un switch cambiamos el texto de la variable por lo que queremos buscar
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

                case "Placa":
                    busqueda = "placa";
                    break;
            }
            //comprobamos que el textbox de buscar este lleno
            if (string.IsNullOrEmpty(txtBusqueda.Text) || cboBuscar.Text == "Seleccione..")
            {
                MessageBox.Show("Debe seleccionar y escribir algo para buscar", "Buscar", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {
                //ejecutamos una consulta sql
                SqlCommand co = new SqlCommand("select codigoauto as 'Codigo', tipoauto as 'Tipo',marca as 'Marca', modelo as 'Modelo', color as 'Color', transmision as 'Transmision', motor as 'Motor', cantpasajeros as 'Pasajeros', placa as 'Placa', precio as 'Precio', estado as 'Estado' from autos where " + busqueda + " like '%" + txtBusqueda.Text + "%'", cn);
                SqlDataAdapter sd = new SqlDataAdapter(co);
                DataTable dt = new DataTable();
                sd.Fill(dt);
                //llenamos el grid view con la informacion que arroje la consulta
                GridViewAutos.DataSource = dt;

            }


        }

        //metodo del boton ver todos
        private void btnListar_Click(object sender, EventArgs e)
        {
            LLenarGrid(GridViewAutos);
            cboBuscar.Text = "Seleccione..";
            txtBusqueda.Clear();
        }


        //validaciones para los textbox y comboboxs

            //metodo del evento keypress del comboboc tipo de auto, no deja que el usuario elimine el texto
        private void cboTipo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cboTransmision_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }


        //metodo del evento keypress para que el usuario no pueda meter letras o otras cosas en el precio
        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
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

        //metodo del evento keypress para que solo deje meter texto y espacios
        private void txtColor_KeyPress(object sender, KeyPressEventArgs e)
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

        //metodo para abrir el reporte
        private void btnReporte_Click(object sender, EventArgs e)
        {
            Reportes.ReporteautosAdmin report = new Reportes.ReporteautosAdmin();

            report.ShowDialog();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            imgfondo.Visible = false;

            this.openFileDialog1.ShowDialog();
            openFileDialog1.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";

            try
            {
                if (this.openFileDialog1.FileName.Equals("") == false)
                {
                    foto.Load(this.openFileDialog1.FileName);
                }
            }

            catch
            {
                imgfondo.Visible = true;
                MessageBox.Show("Debe seleccionar una imagen", "Seleccion Imagen", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {

            Reportes.FichaAuto report = new Reportes.FichaAuto();

            Reportes.Ficha ficha = new Reportes.Ficha();
            ficha.codigo = (txtCod.Text);
            ficha.tipo = (cboTipo.Text);
            ficha.transmision = (cboTransmision.Text);
            ficha.modelo = (txtModelo.Text);
            ficha.pasajeros = (numericPasajeros.Text);
            ficha.precio = (txtPrecio.Text);
            ficha.marca = (txtMarca.Text);
            ficha.motor = (txtMotor.Text);
            ficha.placa = (txtPlaca.Text);
            ficha.estado = (cboEstado.Text);
            ficha.imagen = GetBytes(foto.Image);
            report.Datos.Add(ficha);

            report.ShowDialog();
        }

        private byte[] GetBytes(Image image)
        {
            MemoryStream ms = new MemoryStream();
            image.Save(ms, ImageFormat.Png);

            return ms.ToArray();
        }
    }
}
