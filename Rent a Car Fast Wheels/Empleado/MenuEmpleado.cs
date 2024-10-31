using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rent_a_Car_Fast_Wheels.Empleado
{
    public partial class MenuEmpleado : Form
    {
        string n, ap, tp, cod;
        public MenuEmpleado(string nombre, string apellido, string tipo, string c)
        {
            InitializeComponent();
            n = nombre;
            ap = apellido;
            tp = tipo;
            cod = c;
        }


        //metodo para abrir un formulario hijo dentro del panel del formulario padre
        private void AbrirFormhijo(object formhijo)
        {
            if (this.panelContenedor.Controls.Count > 0)
                this.panelContenedor.Controls.RemoveAt(0);

            Form fh = formhijo as Form;
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            this.panelContenedor.Controls.Add(fh);
            this.panelContenedor.Tag = fh;
            fh.Show();

        }

        //metodo para abrir el inicio al dar click en el logo

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            AbrirFormhijo(new FrmInicioEmpleado(n,ap,tp,cod));
        }
        //metodo para que al inicio del sistema cargue en el formulario inicio
        private void MenuEmpleado_Load(object sender, EventArgs e)
        {
            pictureBox2_Click(null, e);
        }

        //metodo para mostrar submenus
        private void MostrarSubmenu(Panel Submenu)
        {
            if (SubMenuAlquiler.Visible == false)
            {
                OcultarSubmenu();
                Submenu.Visible = true;
            }
            else
                Submenu.Visible = false;
        }

        //metodo para ocultar submenus
        private void OcultarSubmenu()
        {
            if (SubMenuAlquiler.Visible == true)
            {
                SubMenuAlquiler.Visible = false;
            }
        }

        private void btnAutos_Click(object sender, EventArgs e)
        {
            AbrirFormhijo(new FrmAutosEmpleado());
            OcultarSubmenu();
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            AbrirFormhijo(new FrmClientesEmpleado());
            OcultarSubmenu();
        }

        private void btnAlquileres_Click(object sender, EventArgs e)
        {
            
            MostrarSubmenu(SubMenuAlquiler);
        }

        private void btnEntrega_Click(object sender, EventArgs e)
        {
            AbrirFormhijo(new FrmNuevoAlquilerEmpleado(cod));
        }

        private void btnDevolucion_Click(object sender, EventArgs e)
        {
            AbrirFormhijo(new FrmDevolucionEmpleado());
        }

        private void btnMantenimiento_Click(object sender, EventArgs e)
        {
            AbrirFormhijo(new FrmMantenimientoEmpleado());
            OcultarSubmenu();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {

            if (panelSesion.Visible == true)
            {
                panelSesion.Visible = false;
            }
            else
            {
                panelSesion.Visible = true;
            }
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Esta seguro de cerrar sesion?", "Alerta", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)

            {
                this.Hide();
                new Login.Login().Show();
            }
        }

        private void btnSalir1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Esta seguro de cerrar la aplicacion?", "Alerta", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)

            {
                Application.Exit();
            }
        }

       
    }
}
