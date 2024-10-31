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

namespace Rent_a_Car_Fast_Wheels.Login
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        SqlConnection conec = new SqlConnection("Data Source=localhost; Initial Catalog = rentcar; Integrated Security=True;");


        public void logear(string usuario, string contrasena)
        {

            try
            {
                conec.Open();
                SqlCommand cmd = new SqlCommand("ProcedimientoLogin", conec);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@usuario", usuario);
                cmd.Parameters.AddWithValue("@pass", contrasena);


                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                if (dt.Rows.Count == 1)
                {
                    this.Hide();
                    if (dt.Rows[0][2].ToString() == "Administrador")
                    {

                        new Administrador.MenuAdmin(dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString(), dt.Rows[0][2].ToString(), dt.Rows[0][3].ToString()).Show();
                     
                    }
                    else if (dt.Rows[0][2].ToString() == "Empleado")
                    {
     
                        new Empleado.MenuEmpleado(dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString(), dt.Rows[0][2].ToString(), dt.Rows[0][3].ToString()).Show();
                        new Empleado.FrmNuevoAlquilerEmpleado(dt.Rows[0][3].ToString());
                    }


                }
                else
                {
                    MessageBox.Show("Error,Usuario y/o Contraseña incorrectos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                conec.Close();
            }
        }

        private void picCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            txtUsuario.Clear();
            txtContraseña.Clear();
            txtUsuario.Focus();
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            logear(this.txtUsuario.Text, this.txtContraseña.Text);
        }


    }
}
