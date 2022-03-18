using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;//agregar
using conexion.Entidades;//agregar segun el nombre de la carpeta
using System.Data;

namespace conexion.AccesoMotor
{
  public class UsuarioBD
    {
        //Conexion al Servidor
        readonly string cadena = "server=localhost; port=3306;Database=Ejericico3_usuario;Uid=root;pwd=Alexisviera2612; ";
        MySqlConnection cone;
        MySqlCommand comando;

        public Usuario login(string codigo, string clave)
        {
          //Objeto
            Usuario dato = null; //null por si ingresa valores incorrectos

            //capturar errores para que no se cierre 
            //Codigo para consultar la tabla 
            try//donde programamos
            {
              
                string sql = "SELECT * FROM Usuario WHERE codigo=@Codigo AND clave=@Clave;";
                
                cone = new MySqlConnection(cadena);
                //abrimos la conexion a la base de datos
                cone.Open();
                
                comando = new MySqlCommand(sql, cone);
              //parametros
                comando.Parameters.AddWithValue("@Codigo", codigo);
                comando.Parameters.AddWithValue("@Clave", clave);
               //capturar datos
                MySqlDataReader reader = comando.ExecuteReader();
                //leemos el objeto reader con while 

                while (reader.Read())// osea mientras el objeto lea valores
                {
                    dato = new Usuario();//objeto dato es el que retorna 
                    dato.Codigo = reader[0].ToString();  //el primer valor sera 0 ya que en la tabla esta en la posicion 0 el codigo, las tablas se inicia de 0 a contar  ya que estamos en arreglos 
                    dato.Nombre = reader[1].ToString();
                    dato.Clave = reader[2].ToString();
                    dato.Cargo = reader[3].ToString();
                    dato.Rol = reader[4].ToString();
                    dato.EstaActivo = Convert.ToBoolean(reader[5]);
                }
                //cerramos reader
                reader.Close();
                //cerramos la conexion 
                cone.Close();


            }
            catch (Exception ex)//captura todos los Errores
            {

            }
            //retornar
            return dato;

        }


        public DataTable ListarUsuario()
        {
            DataTable ListaUsuarios = new DataTable();
            try
            {
                //atrae todos los usuarios en la tabla 

                string sql = "SELECT * FROM Usuario; ";
                //instanciar el objeto conexion 
                cone = new MySqlConnection(cadena);
                cone.Open();
                comando = new MySqlCommand(sql, cone);
                MySqlDataReader reader = comando.ExecuteReader();
                //Captura todos los datos 
                ListaUsuarios.Load(reader);
                reader.Close();
                cone.Close();
            }

            catch (Exception ex)//CAPTURA ERRORES
            {

            }
            return ListaUsuarios;
        }

        public bool InsertarUsuario(Usuario usuario)
        {
            //variable local 
            bool insertar= false;
            try
            {
                //Sirve para insertar tablas a la Base de Datos 
                
                string sql = "INSERT INTO Usuario VALUES(@Codigo,@Nombre,@Clave,@Cargo,@Rol,@EstaActivo);";
                //abrir la conexion de la base 
                cone = new MySqlConnection(cadena);
                cone.Open();

                comando = new MySqlCommand(sql, cone);
                //pasamos los parametros 
                comando.Parameters.AddWithValue("@Codigo", usuario.Codigo);
                comando.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                comando.Parameters.AddWithValue("@Clave", usuario.Clave);
                comando.Parameters.AddWithValue("@Cargo", usuario.Cargo);
                comando.Parameters.AddWithValue("@Rol", usuario.Rol);
                comando.Parameters.AddWithValue("@EstaActivo", usuario.EstaActivo);
                //ahora vamos A ejecutar ,ExecuteNonQuery(); ejecuta y trae datos
                comando.ExecuteNonQuery();
                insertar = true;

                cone.Close();


            }
            catch (Exception ex)
            {

            }
            return insertar;
        }

        public bool ModificarUsuario(Usuario usuario)
        {
            //variable local 
            bool modifico = false;
            try
            {
                //Sirve para insertar datos a la tabla
               
                string sql = "UPDATE Usuario SET Codigo = @Codigo, Nombre = @Nombre,Cargo=@Cargo,Clave = @Clave,Rol= @Rol,EstaActivo= @EstaActivo WHERE Codigo =@Codigo;";
                //abrir la conexion de la base 
                cone = new MySqlConnection(cadena);
                cone.Open();

                comando = new MySqlCommand(sql, cone);
                //pasamos los parametros 
                comando.Parameters.AddWithValue("@Codigo", usuario.Codigo);
                comando.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                comando.Parameters.AddWithValue("@Clave", usuario.Clave);
                comando.Parameters.AddWithValue("@Cargo", usuario.Cargo);
                comando.Parameters.AddWithValue("@Rol", usuario.Rol);
                comando.Parameters.AddWithValue("@EstaActivo", usuario.EstaActivo);
                //ahora vamos A ejecutar ,ExecuteNonQuery(); ejecuta y trae datos
                comando.ExecuteNonQuery();
                modifico = true;


                cone.Close();

            }
            catch (Exception ex)
            {

            }
            return modifico;

        }

        public bool EliminarUsuario(string codigo)
        {
            bool elimino = false;
            try
            {
                //Sirve para insertar para Eliminar datos de la tabla
                //DELETE FROM SIRVE PARA ELIMINAR UN REGISTRO
                
                string sql = "DELETE FROM Usuario  WHERE Codigo =@Codigo;";
                //abrir la conexion de la base 
                cone = new MySqlConnection(cadena);
                cone.Open();

                comando = new MySqlCommand(sql, cone);
                //pasamos los parametros 
                comando.Parameters.AddWithValue("@Codigo", codigo);

                //ahora vamos A ejecutar ,ExecuteNonQuery(); ejecuta y trae datos
                comando.ExecuteNonQuery();
                elimino = true;

                cone.Close();



            }
            catch (Exception ex)
            {

            }
            return elimino;



        }

    }
}
