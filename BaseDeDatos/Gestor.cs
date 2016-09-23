using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;
using MySql.Data.MySqlClient;
using RqSoft.Entidades;

namespace RqSoft.BaseDeDatos
{
    public class Gestor
    {

        public static DataTable encr()
        {
            List<MySqlParameter> lista = new List<MySqlParameter>();
            string sql = "select id_usuario as id, contra from usuario";
            return Conexion.EjecutarConsulta(sql,lista, System.Data.CommandType.Text);
        }

        public static void agregar_usuario(Usuario user)
        {
            List<MySqlParameter> lista = new List<MySqlParameter>();

            string sql = "insert into usuario(id_usuario,nombres,apellidos,contra, email, organizacion) "
          + "  values('{0}','{1}','{2}',md5('{3}'),'{4}','{5}')";
            sql = string.Format(sql, user.id_usuario, user.nombre, user.apellido, user.contraseña, user.email, user.organizacion);

            Conexion.EjecutarOperacion(sql, lista, System.Data.CommandType.Text);
        }

        public static DataTable iniciar_sesion(Usuario obj)
        {
            List<MySqlParameter> lista = new List<MySqlParameter>();


            string sql = "select id_usuario, contra as password, concat( nombres,' ',apellidos) as nombre from usuario where id_usuario='" + obj.id_usuario + "' and contra=md5('"+obj.contraseña+"')";


            DataTable d = Conexion.EjecutarConsulta(sql, lista, System.Data.CommandType.Text);

            return d;
        }

        public static string agregar_proyecto(Proyecto obj)
        {
            List<MySqlParameter> lista = new List<MySqlParameter>();

            string sql = "insert into proyecto(nombre,login,fecha,version,fechaReal) values('{0}','{1}','{2}','{3}', now())";
            sql = string.Format(sql, obj.nombre,obj.login, obj.fecha, obj.version);

            Conexion.EjecutarOperacion(sql, lista, System.Data.CommandType.Text);

            sql = "select codigo from proyecto where login='"+obj.login+"' and nombre='"+obj.nombre+"'";
             DataTable d = Conexion.EjecutarConsulta(sql, lista, System.Data.CommandType.Text);

             return d.Rows[0]["codigo"].ToString();
        }

        public static DataTable consultas(string sent)
        {
            List<MySqlParameter> lista = new List<MySqlParameter>();
            DataTable d = Conexion.EjecutarConsulta(sent, lista, System.Data.CommandType.Text);
            return d;
        }


        public static DataSet consultarDS(string sent)
        {
            List<MySqlParameter> lista = new List<MySqlParameter>();
            DataSet d = Conexion.EjecutarConsultaDS(sent, lista, System.Data.CommandType.Text);
            return d;
        }

        public static Boolean existe(string obj)
        {
            List<MySqlParameter> lista = new List<MySqlParameter>();
            DataTable d = Conexion.EjecutarConsulta(obj, lista, System.Data.CommandType.Text);
            if (d.Rows.Count >= 1)
            {
                return true;
            }
            else  return false;
        }


        public static void insert(string obj)
        {
            List<MySqlParameter> lista = new List<MySqlParameter>();
 
            Conexion.EjecutarOperacion(obj, lista, System.Data.CommandType.Text);
        }
    }
}
