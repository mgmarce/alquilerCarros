using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Rent_a_Car_Fast_Wheels.Administrador
{
    public partial class FrmClientes : Form
    {
        public FrmClientes()
        {
            InitializeComponent();
            LLenarGrid(grvClientes);
        }

        SqlConnection cn = new SqlConnection("Data Source=localhost; Initial Catalog = rentcar; Integrated Security=True;");
        public DataTable dt;
        public SqlDataAdapter sda;
        public SqlDataReader dr;

        //evento que se incia al carga el formulario
        private void FrmAutos_Load(object sender, EventArgs e)
        {
            //llamamos al metodo de llenado de grid view para llenar el grid view cuando el formulario se cargue
            LLenarGrid(grvClientes);
        }

        //metodo para llenar el grid view cuando el formulario se cargue
        public void LLenarGrid(DataGridView dgv)
        {
            try
            {
                //llamamos al procedimiento almacenado
                SqlCommand cmd = new SqlCommand("ListarClientes", cn);
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

            txtCodCliente.Clear();
            txtNombreCliente.Clear();
            txtApellidoCliente.Clear();
            txtDUI.Clear();
            txtLicencia.Clear();
            cboTipoLicencia.Text = "Seleccione...";
            dateVencimiento.Value = DateTime.Today;
            cboGenero.Text = "Seleccione...";
            txtTelefono.Clear();
            txtCorreo.Clear();

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            VaciarCampos();
            btnAgregar.Visible = true;
            btnActualizar.Visible = false;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            //condicion para que ningun text box este vacio o ningun combo box este sin seleccionar
            if (string.IsNullOrEmpty(txtNombreCliente.Text) || string.IsNullOrEmpty(txtApellidoCliente.Text)
                || string.IsNullOrEmpty(txtDUI.Text) || string.IsNullOrEmpty(txtLicencia.Text) || cboTipoLicencia.Text == "Seleccione..." ||
                cboGenero.Text == "Seleccione..." || string.IsNullOrEmpty(txtTelefono.Text) || string.IsNullOrEmpty(txtCorreo.Text))

            {
                //mensaje de error
                MessageBox.Show("Debe llenar los campos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }
            else
            {
                //declaracion de variables que se enviaran como parametros al procedimiento almacenado de sql
                string nombre = txtNombreCliente.Text;
                string apellido = txtApellidoCliente.Text;
                string dui = txtDUI.Text;
                string licencia = txtLicencia.Text.ToUpper();
                string tipolicencia = cboTipoLicencia.Text;
                DateTime vencimiento = dateVencimiento.Value;
                string genero = cboGenero.Text;
                string telefono = txtTelefono.Text;
                string correo = txtCorreo.Text;


                try
                {
                    //llamamos al procedimiento buscar para que no deje ingresar un registro si ya existe
                    SqlCommand cmd = new SqlCommand("BuscarClientes", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //le mandamos parametros
                    cmd.Parameters.AddWithValue("@dui", dui);
                    dt = new DataTable();
                    sda = new SqlDataAdapter(cmd);
                    sda.Fill(dt);
                    //si encontro un registro existente envia un mensaje y no deja ingresar
                    if (dt.Rows.Count == 1)
                    {
                        //mesaje de error
                        MessageBox.Show("Ya existe un clkiente con ese DUI", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //los txtbox de codigo los vaciamos y los ocultamos
                        txtCodCliente.Clear();
                        txtCodCliente.Visible = false;
                        lblCodCliente.Visible = false;
                    }

                    //sino encontro ningun registron duplicado deja ingresar uno nuevo
                    else
                    {
                        //llamamos al procedimiento y le mandamos parametros
                        SqlCommand cmd1 = new SqlCommand("AgregarCliente", cn);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.AddWithValue("@nombre", nombre);
                        cmd1.Parameters.AddWithValue("@apellido", apellido);
                        cmd1.Parameters.AddWithValue("@dui", dui);
                        cmd1.Parameters.AddWithValue("@licencia", licencia);
                        cmd1.Parameters.AddWithValue("@tipolicencia", tipolicencia);
                        cmd1.Parameters.AddWithValue("@vencimiento", vencimiento);
                        cmd1.Parameters.AddWithValue("@genero", genero);
                        cmd1.Parameters.AddWithValue("@telefono", telefono);
                        cmd1.Parameters.AddWithValue("@correo", correo);

                        SqlDataAdapter sda = new SqlDataAdapter(cmd1);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);

                        //llenamos el datgrid otra vez para que se actualize con el nuevo registro
                        LLenarGrid(grvClientes);
                        //vaciamos los campos
                        VaciarCampos();

                        //mensaje de confirmacion
                        MessageBox.Show("Ingresado correctamente", "Ingreso de Cliente", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                }



                catch (Exception ex)
                {
                    MessageBox.Show("Error, datos no válidos" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            //comprobamos que el usuario selecciono una fila
            if (grvClientes.SelectedRows.Count == 1)
            {
                //le enviamos los datos que estan en las celdas de la fila seleccionada hacia los textbox para que los pueda editar
                txtCodCliente.Text = grvClientes.CurrentRow.Cells[0].Value.ToString();
                txtNombreCliente.Text = grvClientes.CurrentRow.Cells[1].Value.ToString();
                txtApellidoCliente.Text = grvClientes.CurrentRow.Cells[2].Value.ToString();
                txtDUI.Text = grvClientes.CurrentRow.Cells[3].Value.ToString();
                txtLicencia.Text = grvClientes.CurrentRow.Cells[4].Value.ToString();
                cboTipoLicencia.Text = grvClientes.CurrentRow.Cells[5].Value.ToString();
                dateVencimiento.Text = grvClientes.CurrentRow.Cells[6].Value.ToString();
                cboGenero.Text = grvClientes.CurrentRow.Cells[7].Value.ToString();
                txtTelefono.Text = grvClientes.CurrentRow.Cells[8].Value.ToString();
                txtCorreo.Text = grvClientes.CurrentRow.Cells[9].Value.ToString();


                //hacemos visible el texbox de codigo y el boton de editar, pero ocultamos el boton de agregar nuevo
                btnAgregar.Visible = false;
                btnActualizar.Visible = true;
                txtCodCliente.Visible = true;
                lblCodCliente.Visible = true;



            }
            else
                //si no selecciono ninguna fila le mandamos un mensaje
                MessageBox.Show("Seleccione una fila por favor", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            //nos aseguramos que todos los textbox esten llenos y los combo box esten seleccionados con algo
            if (string.IsNullOrEmpty(txtNombreCliente.Text) || string.IsNullOrEmpty(txtApellidoCliente.Text)
               || string.IsNullOrEmpty(txtDUI.Text) || string.IsNullOrEmpty(txtLicencia.Text) || cboTipoLicencia.Text == "Seleccione..." ||
               cboGenero.Text == "Seleccione..." || string.IsNullOrEmpty(txtTelefono.Text) || string.IsNullOrEmpty(txtCorreo.Text))

            {
                //mensaje de error
                MessageBox.Show("Debe llenar los campos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }
            //sino dejamos que guarde los cambios
            else
            {
                string cod = txtCodCliente.Text;
                string nombre = txtNombreCliente.Text;
                string apellido = txtApellidoCliente.Text;
                string dui = txtDUI.Text;
                string licencia = txtLicencia.Text;
                string tipolicencia = cboTipoLicencia.Text;
                DateTime vencimiento = dateVencimiento.Value;
                string genero = cboGenero.Text;
                string telefono = txtTelefono.Text;
                string correo = txtCorreo.Text;
                try
                {
                    SqlCommand cmd1 = new SqlCommand("ActualizarCliente", cn);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.AddWithValue("@cod", cod);
                    cmd1.Parameters.AddWithValue("@nombre", nombre);
                    cmd1.Parameters.AddWithValue("@apellido", apellido);
                    cmd1.Parameters.AddWithValue("@dui", dui);
                    cmd1.Parameters.AddWithValue("@licencia", licencia);
                    cmd1.Parameters.AddWithValue("@tipolicencia", tipolicencia);
                    cmd1.Parameters.AddWithValue("@vencimiento", vencimiento);
                    cmd1.Parameters.AddWithValue("@genero", genero);
                    cmd1.Parameters.AddWithValue("@telefono", telefono);
                    cmd1.Parameters.AddWithValue("@correo", correo);

                    SqlDataAdapter sda = new SqlDataAdapter(cmd1);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    LLenarGrid(grvClientes);

                    //otra vez ocultamos el textbox de codigo y el boton guardar cambios, y hacemos visible el de agregar nuevo
                    btnAgregar.Visible = true;
                    btnActualizar.Visible = false;
                    txtCodCliente.Visible = false;
                    lblCodCliente.Visible = false;
                    VaciarCampos();

                    //mensaje de confirmacion
                    MessageBox.Show("Actualizado correctamente", "Actualizacion de cliente", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error, datos no válidos" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

            string cod = grvClientes.CurrentRow.Cells[0].Value.ToString();

            SqlCommand cmd1 = new SqlCommand("BusquedaCliente_alquiler", cn);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.AddWithValue("@cod", cod);
            SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);

            if (dt1.Rows.Count >= 1)
            {
                MessageBox.Show("No se puede eliminar al cliente, por que está relacionado con uno o mas alquileres", "Borrar cliente", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            else
            {
                if (grvClientes.SelectedRows.Count == 1)
                {
                    if (MessageBox.Show("¿Esta seguro que desea eliminar el registro?", "Alerta", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {

                        SqlCommand cmd = new SqlCommand("EliminarCliente", cn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@cod", cod);
                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);


                        MessageBox.Show("Cliente Eliminado Correctamente", "Borrar cliente", MessageBoxButtons.OK, MessageBoxIcon.Information);



                        LLenarGrid(grvClientes);
                    }
                }

                else
                {

                    MessageBox.Show("Seleccione una fila por favor", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }





         
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            //declaramos una variable
            string busqueda = " ";

            //con un switch cambiamos el texto de la variable por lo que queremos buscar
            switch (cboBuscar.Text)
            {
                case "DUI":
                    busqueda = "dui";
                    break;

                case "Nombres":
                    busqueda = "nombrec";
                    break;

                case "Apellidos":
                    busqueda = "apellidoc";
                    break;
                case "Telefono":
                    busqueda = "tel";
                    break;

            }
            //comprobamos que el textbox de buscar este lleno
            if (string.IsNullOrEmpty(txtBuscar.Text) || cboBuscar.Text == "Seleccione..")
            {
                MessageBox.Show("Debe seleccionar y escribir algo para buscar", "Buscar", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {
                //ejecutamos una consulta sql
                SqlCommand co = new SqlCommand(" select codcliente as 'Codigo', nombrec as 'Nombre', apellidoc as 'Apellidos', DUI as   'DUI', numlicencia as 'Numero de Licencia', tipolicencia as 'Tipo de Licencia', fechavenci as 'Fecha de Vencimiento', genero as 'Género',  tel as 'Télefono', correo as 'Correo' from clientes where " + busqueda + " like '%" + txtBuscar.Text + "%'", cn);
                SqlDataAdapter sd = new SqlDataAdapter(co);
                DataTable dt = new DataTable();
                sd.Fill(dt);
                //llenamos el grid view con la informacion que arroje la consulta
                grvClientes.DataSource = dt;

            }
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            LLenarGrid(grvClientes);
            cboBuscar.Text = "Seleccione..";
            txtBuscar.Clear();
        }

        //validaciones para los textbox y comboboxs
        private void cboTipoLicencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cboGenero_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        //metodo del evento keypress para que solo deje meter texto y espacios
        private void txtNombreCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) &&
                                      (e.KeyChar != ' '))
            {
                e.Handled = true;
            }
        }

        private void txtApellidoCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) &&
                          (e.KeyChar != ' '))
            {
                e.Handled = true;
            }
        }

        //metodo del evento keypress para que el usuario no pueda meter letras o otras cosas en el DUI y Telefono
        private void txtDUI_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
             (e.KeyChar != '-'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
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
            Reportes.ReporteClientes report = new Reportes.ReporteClientes();

            report.ShowDialog();
        }
    }
}
