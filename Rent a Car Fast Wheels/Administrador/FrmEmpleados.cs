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
    public partial class FrmEmpleados : Form
    {
        //evento que se incia al carga el formulario

        public FrmEmpleados()
        {
            InitializeComponent();
            LLenarGrid(GridViewEmpleados);
        }

        SqlConnection cn = new SqlConnection(@"server=localhost; Initial Catalog = rentcar; Integrated Security=True;");

        public DataTable dt;
        public SqlDataAdapter sda;
        public SqlDataReader dr;



        public void LLenarGrid(DataGridView dgv)
        {
            try
            {
                //llamamos al procedimiento almacenado
                SqlCommand cmd = new SqlCommand("ListarEmpleados", cn);
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

            txtNEmpleado.Clear();
            cboBuscar.Text = "Seleccione...";
            txtApellidoEm.Clear();
            txtUsuario.Clear();
            txtContrasena.Clear();
            cboTipo.Text = "Seleccione...";


        }

        //metodo del boton cancelar
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            VaciarCampos();
            btnAgregar.Visible = true;
            btnGuardarCam.Visible = false;
            VaciarCampos();
        }

        //metodo dentro del boton agregar
        public void btnAgregar_Click(object sender, EventArgs e)
        {
            //condicion para que ningun text box este vacio o ningun combo box este sin seleccionar
            if (cboTipo.Text == "Seleccione..." || string.IsNullOrEmpty(txtNEmpleado.Text) || string.IsNullOrEmpty(txtApellidoEm.Text)
                || string.IsNullOrEmpty(txtUsuario.Text) || string.IsNullOrEmpty(txtContrasena.Text))

            {
                //mensaje de error
                MessageBox.Show("Debe llenar los campos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }
            else
            {
                //declaracion de variables que se enviaran como parametros al procedimiento almacenado de sql
                string usuario = txtUsuario.Text;
                string contrasena = txtContrasena.Text;
                string nombreEm = txtNEmpleado.Text;
                string apellido = txtApellidoEm.Text;
                string tipo = cboTipo.Text;
                try
                {
                    //llamamos al procedimiento buscar para que no deje ingresar un registro si ya existe
                    SqlCommand cmd = new SqlCommand("BuscarEmpleado", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //le mandamos parametros
                    cmd.Parameters.AddWithValue("@usuario", usuario);
                    dt = new DataTable();
                    sda = new SqlDataAdapter(cmd);
                    sda.Fill(dt);

                    //si encontro un registro existente envia un mensaje y no deja ingresar
                    if (dt.Rows.Count == 1)
                    {
                        //mesaje de error
                        MessageBox.Show("Ya existe un usuario con este nombre", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);


                    }

                    //sino encontro ningun registron duplicado deja ingresar uno nuevo
                    else
                    {
                        //llamamos al procedimiento y le mandamos parametros
                        SqlCommand cmd1 = new SqlCommand("AgregarEmpleado", cn);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.AddWithValue("@nombreemp", nombreEm);
                        cmd1.Parameters.AddWithValue("@apellidoemp", apellido);
                        cmd1.Parameters.AddWithValue("@usuemp", usuario);
                        cmd1.Parameters.AddWithValue("@contemp", contrasena);
                        cmd1.Parameters.AddWithValue("@tipoemp", tipo);
                        SqlDataAdapter sda = new SqlDataAdapter(cmd1);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);

                        //llenamos el datgrid otra vez para que se actualize con el nuevo registro
                        LLenarGrid(GridViewEmpleados);
                        //vaciamos los campos
                        VaciarCampos();

                        //mensaje de confirmacion
                        MessageBox.Show("Ingresado correctamente", "Ingreso de empleado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                }



                catch (Exception ex)
                {
                    MessageBox.Show("Error, datos no válidos" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        //metodo del boton ver todos


        public void btnVerTodos_Click(object sender, EventArgs e)
        {
            LLenarGrid(GridViewEmpleados);
            cboBuscar.Text = "Seleccione..";
            txtBusqueda.Clear();
        }


        private void btnEditar_Click(object sender, EventArgs e)
        {
            //comprobamos que el usuario selecciono una fila
            if (GridViewEmpleados.SelectedRows.Count == 1)
            {
                //le enviamos los datos que estan en las celdas de la fila seleccionada hacia los textbox para que los pueda editar
                txtCodigo.Text = GridViewEmpleados.CurrentRow.Cells[0].Value.ToString(); ;
                txtNEmpleado.Text = GridViewEmpleados.CurrentRow.Cells[1].Value.ToString();
                txtApellidoEm.Text = GridViewEmpleados.CurrentRow.Cells[2].Value.ToString();
                txtUsuario.Text = GridViewEmpleados.CurrentRow.Cells[3].Value.ToString();
                txtContrasena.Text = GridViewEmpleados.CurrentRow.Cells[4].Value.ToString();
                cboTipo.Text = GridViewEmpleados.CurrentRow.Cells[5].Value.ToString();


                //hacemos visible el texbox de codigo y el boton de editar, pero ocultamos el boton de agregar nuevo
                btnAgregar.Visible = false;
                btnEditar.Visible = true;
                btnGuardarCam.Visible = true;



            }
            else
                //si no selecciono ninguna fila le mandamos un mensaje
                MessageBox.Show("Seleccione una fila por favor", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnGuardarCam_Click(object sender, EventArgs e)
        {
            {
                //condicion para que ningun text box este vacio o ningun combo box este sin seleccionar
                if (cboTipo.Text == "Seleccione..." || string.IsNullOrEmpty(txtNEmpleado.Text) || string.IsNullOrEmpty(txtApellidoEm.Text)
                    || string.IsNullOrEmpty(txtUsuario.Text) || string.IsNullOrEmpty(txtContrasena.Text))

                {
                    //mensaje de error
                    MessageBox.Show("Debe llenar los campos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }
                else
                {
                    //declaracion de variables que se enviaran como parametros al procedimiento almacenado de sql
                    string codigo = txtCodigo.Text;
                    string usuario = txtUsuario.Text;
                    string contrasena = txtContrasena.Text;
                    string nombreEm = txtNEmpleado.Text;
                    string apellido = txtApellidoEm.Text;
                    string tipo = cboTipo.Text;

                    //llamamos al procedimiento y le mandamos parametros
                    SqlCommand cmd1 = new SqlCommand("ModificarEmpleado", cn);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.AddWithValue("@codigo", codigo);
                    cmd1.Parameters.AddWithValue("@nombreemp", nombreEm);
                    cmd1.Parameters.AddWithValue("@apellidoemp", apellido);
                    cmd1.Parameters.AddWithValue("@usuemp", usuario);
                    cmd1.Parameters.AddWithValue("@contemp", contrasena);
                    cmd1.Parameters.AddWithValue("@tipoemp", tipo);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd1);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    //llenamos el datgrid otra vez para que se actualize con el nuevo registro
                    LLenarGrid(GridViewEmpleados);
                    //vaciamos los campos
                    VaciarCampos();

                    //mensaje de confirmacion
                    MessageBox.Show("Usuario modificado correctamente", "Ingreso de empleado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnGuardarCam.Visible = false;
                    btnAgregar.Visible = true;

                  }
                }   
            }
        

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string cod = GridViewEmpleados.CurrentRow.Cells[0].Value.ToString();

            SqlCommand cmd1 = new SqlCommand("BusquedaEmpleado_alquiler", cn);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.AddWithValue("@cod", cod);
            SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);

            if (dt1.Rows.Count >= 1)
            {
                MessageBox.Show("No se puede eliminar al empleado, por que está relacionado con uno o mas alquileres", "Borrar Empleado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            else
            {
                if (GridViewEmpleados.SelectedRows.Count == 1)
                {
                    if (MessageBox.Show("¿Esta seguro que desea eliminar el usuario?", "Alerta", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {

                        SqlCommand cmd = new SqlCommand("BorrarEmpleado", cn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@codempleado", cod);
                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);


                        MessageBox.Show("Usuario Eliminado Correctamente", "Borrar Usuario", MessageBoxButtons.OK, MessageBoxIcon.Information);



                        LLenarGrid(GridViewEmpleados);
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
                case "Nombre":
                    busqueda = "nombreemple";
                    break;

                case "Apellido":
                    busqueda = "apellidoemple";
                    break;

                case "Usuario":
                    busqueda = "usuario";
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
                SqlCommand co = new SqlCommand("select codempleado as 'Codigo', nombreemple as 'Nombre', apellidoemple as 'Apellido', usuario as 'Usuario', contraseña as 'Contraseña', tipousuario as 'Tipo Usuario' from empleados where " + busqueda + " like '%" + txtBusqueda.Text + "%'", cn);
                SqlDataAdapter sd = new SqlDataAdapter(co);
                DataTable dt = new DataTable();
                sd.Fill(dt);
                //llenamos el grid view con la informacion que arroje la consulta
                GridViewEmpleados.DataSource = dt;

            }
        }


        //Metodo para impedir que se ingresen datos al combobox
        private void cboTipo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        private void cboBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void btnGenRep_Click(object sender, EventArgs e)
        {
           
            Reportes.ReporteEmpleados report = new Reportes.ReporteEmpleados();

            report.ShowDialog();
        }

       
    }
}
