using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using conexion;
using conexion.AccesoMotor;//clase accesoMotor 
using conexion.Entidades;//clase Entidad

namespace Ejercicio3_Rosaysela_Diaz_20191005714
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Aceptarbutton1_Click(object sender, EventArgs e)
        {
            //crear objetos de la clase usuario 
            //UuarioBD es el que accede al motor de base de datos
            UsuarioBD usuarioBD = new UsuarioBD();

            Usuario usuario = new Usuario();
            usuario = usuarioBD.login(UsuariotextBox1.Text, contratextBox2.Text);



            if (usuario == null)
            {
                MessageBox.Show("Datos Erroneos");
                return;
            }
            else if (!usuario.EstaActivo)
            {
                MessageBox.Show("Usuario inactivo");
                return;
            }

            //Llamar formulario mostrar datos

            MostrarDatos mostrarDatos = new MostrarDatos();
            mostrarDatos.Show();
            this.Hide();


        }

        private void Cancelarbutton2_Click(object sender, EventArgs e)
        {
            //cancelar 
            this.Close();
        }
    }
}
