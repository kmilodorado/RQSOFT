using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace RqSoft.BaseDeDatos
{
   internal class Conexion
    {
        private static MySqlConnection conex = new MySqlConnection(ConfigurationManager.ConnectionStrings["conexion_mysql"].ConnectionString);
       
         private static bool Conectar()
        {
           
            try
            {
                conex.Open();
                return true;
            }
            catch (Exception)
            {
                Desconectar();
                return false;
            }
            

        }
        private static void Desconectar()
        {
            conex.Close();
        }

        public static void EjecutarOperacion(string sentencia, List<MySqlParameter> ListaParametros, CommandType TipoComando)
        {
            if (Conectar())
            {
                try
                {
                    MySqlCommand comando = new MySqlCommand();
                    comando.CommandText = sentencia;
                    comando.CommandType = TipoComando;
                    comando.Connection = conex;
                    foreach (MySqlParameter parametro in ListaParametros)
                    {
                        comando.Parameters.Add(parametro);
                    }
                    comando.ExecuteNonQuery();
                    Desconectar();
                }
                catch (Exception s)
                 
                {
                    Desconectar();
                    throw new Exception("No se ha podido realizar la operación");

                }
                
            }
            else
            {
                Desconectar();
                throw new Exception("No ha sido posible conectarse a la Base de Datos");
            }
            
        }

        public static DataTable EjecutarConsulta(string sentencia, List<MySqlParameter> ListaParametros, CommandType TipoComando)
        {
            MySqlDataAdapter adaptador = new MySqlDataAdapter();
            adaptador.SelectCommand = new MySqlCommand(sentencia, conex);
            adaptador.SelectCommand.CommandType = TipoComando;

            foreach (MySqlParameter parametro in ListaParametros)
            {
                adaptador.SelectCommand.Parameters.Add(parametro);
            }
            DataSet resultado = new DataSet();
            adaptador.Fill(resultado);

            return resultado.Tables[0];
        }


        public static DataSet EjecutarConsultaDS(string sentencia, List<MySqlParameter> ListaParametros, CommandType TipoComando)
        {
            MySqlDataAdapter adaptador = new MySqlDataAdapter();
            adaptador.SelectCommand = new MySqlCommand(sentencia, conex);
            adaptador.SelectCommand.CommandType = TipoComando;

            foreach (MySqlParameter parametro in ListaParametros)
            {
                adaptador.SelectCommand.Parameters.Add(parametro);
            }
            DataSet resultado = new DataSet();
            adaptador.Fill(resultado);

            return resultado;
        }

   
    }
    }

