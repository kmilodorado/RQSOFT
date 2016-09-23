using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;
using RqSoft.Entidades;
using RqSoft.BaseDeDatos;
using MySql.Data.MySqlClient;

namespace RqSoft.LogicaDeNegocio
{
  public  class admin
    {
     public static DataTable encryp()
        {
            return Gestor.encr();
        }



      public static void ingresar_usuario(Usuario obj)
      {
           Gestor.agregar_usuario(obj);
      } 
      public static DataTable Iniciar_Session(Usuario obj)
      {
          DataTable d = Gestor.iniciar_sesion(obj);
          return d;
      }
      public static string ingresar_proyecto(Proyecto obj)
      {
         return Gestor.agregar_proyecto(obj);
      }
      public static DataTable consultas(string sent)
      {
          DataTable d = Gestor.consultas(sent);
          return d;
      }
      public static DataSet consultarDS(string sent)
      {
           DataSet d = Gestor.consultarDS(sent);
          return d;
      }
      public static Boolean existe(string obj)
      {
          Boolean s = Gestor.existe(obj);
          return s;
      }
      public static void insert(string obj)
      {
          Gestor.insert(obj);
         
      }
 
     public static DataTable correo()
        {
            string sql = "select nombre_sw,correo,contrase,puerto,smtp,dir_logo,url from informacion_general where id_datosgen=1";
            DataTable d = Gestor.consultas(sql);
            return d;
        }

    }
}
