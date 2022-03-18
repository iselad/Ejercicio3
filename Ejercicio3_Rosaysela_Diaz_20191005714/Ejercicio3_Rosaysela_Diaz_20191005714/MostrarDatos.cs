using conexion.AccesoMotor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using conexion.Entidades;


namespace Ejercicio3_Rosaysela_Diaz_20191005714
{
    public partial class MostrarDatos : Form
    {
        public MostrarDatos()
        {
            InitializeComponent();
        }
        //objeto
        UsuarioBD usuarioBD = new UsuarioBD();
        
        string operacion = " ";
        Usuario dato = new Usuario();


        private void MostrarDatos_Load(object sender, EventArgs e)
        {
            ListarUsuarios();
        }
        //procedimiento 
        private void ListarUsuarios()
        {
            dataGridView1.DataSource = usuarioBD.ListarUsuario();

        }
        //Desabilitar botones 
        private void Desabilitar()
        {
            //le ponemos true porque lo tenemos en false Enabled
            CodigotextBox1.Enabled = false;
            NombretextBox2.Enabled = false;
            ClavetextBox3.Enabled = false;
            CargotextBox4.Enabled = false;
            RolcomboBox1.Enabled = false;
            EstaActivocheckBox1.Enabled = false;

            Nuevobutton1.Enabled = true;
            //habilitar botones  al darle clic al nuevo
            Guardarbutton3.Enabled = false;
            Cancelarbutton5.Enabled = false;
        }

        //procedimiento 
        private void habilitarcontroles()
        {
            //le ponemos true porque lo tenemos en false Enabled
            CodigotextBox1.Enabled = true;
            NombretextBox2.Enabled = true;
            ClavetextBox3.Enabled= true;
            CargotextBox4.Enabled= true;
            RolcomboBox1.Enabled = true;
            EstaActivocheckBox1.Enabled = true;

            Nuevobutton1.Enabled = false;
            //habilitar botones  al darle clic al nuevo
            Guardarbutton3.Enabled = true;
            Cancelarbutton5.Enabled = true;

        }


        //limpiar los botones 
        private void limpiarcontroles()
        {
            
            CodigotextBox1.Clear();//limpiar cadena 
            NombretextBox2.Text = " ";//limpiar cadena
            ClavetextBox3.Text = String.Empty;//limpiar una cadena
            CargotextBox4.Text = String.Empty;  
            RolcomboBox1.Text = String.Empty;//limpiar una cadena
           EstaActivocheckBox1.Enabled= false;  
        }
        private void Guardarbutton3_Click(object sender, EventArgs e)
        {
          dato.Codigo = CodigotextBox1.Text;
          dato.Nombre = NombretextBox2.Text;
          dato.Clave =   ClavetextBox3.Text;
          dato.Cargo = CargotextBox4.Text;
          dato.Rol = RolcomboBox1.Text;
          dato.EstaActivo = EstaActivocheckBox1.Checked;

            if (operacion == "Nuevo")
            {

                bool insertar = usuarioBD.InsertarUsuario(dato);

                if (insertar)
                {
                    //mandar mensaje 
                    MessageBox.Show(" Usuario Creado ");
                    ListarUsuarios();// llamo al procedimiento listarUsuario para que refresque la base
                    limpiarcontroles();
                    Desabilitar();
                }
                else
                {
                    //mandar mensaje 
                    MessageBox.Show(" Usuario no se pudo crear ");
                }
            }

            else if (operacion == "Modificar")
            {
                //Boton modifico
                bool modifico = usuarioBD.ModificarUsuario(dato);
                if (modifico)
                {
                    //mandar mensaje 

                    MessageBox.Show(" Usuario Modificado ");
                    ListarUsuarios();// llamo al procedimiento listarUsuario para que refresque la base
                    limpiarcontroles();
                    Desabilitar();
                }
                else
                {
                    //mandar mensaje 
                    MessageBox.Show(" Usuario no se pudo Modificar");
                }
            }
        }
        private void Nuevobutton1_Click(object sender, EventArgs e)
        {
            //llamar procedimiento 
            habilitarcontroles();
            //llamamos la variable operacion que definimos al inicio
            operacion = "Nuevo";
        }

        private void Modificarbutton2_Click(object sender, EventArgs e)
        {
            //propiedad que permite si tenemos seleccionado algun registro 

            operacion = "Modificar";

            if (dataGridView1.SelectedRows.Count >= 1)
            {


                //pasar los datos
                //CurrentRow.Cells significa celda actual
                CodigotextBox1.Text = dataGridView1.CurrentRow.Cells["Codigo"].Value.ToString();
                NombretextBox2.Text = dataGridView1.CurrentRow.Cells["Nombre"].Value.ToString();
                ClavetextBox3.Text = dataGridView1.CurrentRow.Cells["Clave"].Value.ToString();
                CargotextBox4.Text = dataGridView1.CurrentRow.Cells["Cargo"].Value.ToString();
                RolcomboBox1.Text = dataGridView1.CurrentRow.Cells["Rol"].Value.ToString();
                EstaActivocheckBox1.Checked = Convert.ToBoolean(dataGridView1.CurrentRow.Cells["EstaActivo"].Value);
                //Habilitamos los controles
                habilitarcontroles();
            }

        }

        private void Eliminarbutton4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count >= 1)
            {

                bool elimino = usuarioBD.EliminarUsuario(dataGridView1.CurrentRow.Cells["Codigo"].Value.ToString());
                if (elimino)
                {
                    //mandar mensaje 

                    MessageBox.Show(" Usuario Eliminado");
                    ListarUsuarios();// llamo al procedimiento listarUsuario para que refresque la base

                }
                else
                {
                    MessageBox.Show(" Usuario No se pudo Eliminar");
                }
            }
        }
        private void Cancelarbutton5_Click(object sender, EventArgs e)
        {
            Desabilitar();
            limpiarcontroles();
        }

        private void salirbutton6_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }
    }
}
