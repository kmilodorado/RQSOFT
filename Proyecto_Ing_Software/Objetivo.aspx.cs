using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using RqSoft.LogicaDeNegocio;
using RqSoft.Entidades;
using Ext.Net;

namespace Proyecto_Ing_Software
{
    public partial class Objetivo : System.Web.UI.Page
    {
        public static string val1 = string.Empty;
        public static string val2 = string.Empty;
        public static string val3 = string.Empty;
        public static int objetivoc = 0;
       

        protected void Page_Load(object sender, EventArgs e)
        {


            if (!X.IsAjaxRequest)
            {

                if (Session["accion"].Equals("crear"))
            {

                DataTable dt = admin.consultas("select referencia from plantilla_objetivo where proyecto='" + Session["proyecto"] + "'");

                int num = dt.Rows.Count;
                num += 1;
                switch (num.ToString().Length)
                {
                    case 1:
                        txt_Identificador.Text = "OBJ-00" + (num );
                        break;
                    case 2:
                        txt_Identificador.Text = "OBJ-0" + (num);
                        break;
                    case 3:
                        txt_Identificador.Text = "OBJ-" + (num);
                        break;

                }

                dt = admin.consultas("SELECT CONCAT(nombres,' ',apellidos) AS Nombre,id_usuario AS Login, "
                          +" organizacion AS Organizacion FROM usuario u inner join autor_proyecto ap on u.id_usuario= ap.usuario "
                          +" where ap.proyecto='"+Session["proyecto"]+"'");

                store_autores.DataSource = dt;
                store_autores.DataBind();

                dt = admin.consultas("SELECT CONCAT(nombre,' ',apellido) AS Nombre,id , organizacion AS Organizacion "
                            +" FROM fuente f inner join proyecto_fuente pf on (f.id= pf.id_fuente and f.organizacion= pf.id_organizacion) "
                            + " where pf.proyecto='" + Session["proyecto"] + "'");

                store_fuente.DataSource = dt;
                store_fuente.DataBind();

                dt = admin.consultas("SELECT referencia AS id, nombre AS Nombre FROM plantilla_objetivo po WHERE  po.proyecto='" + Session["proyecto"] + "'");

                if (dt.Rows.Count > 0)
                {

                    store_objetivo.DataSource = dt;
                    store_objetivo.DataBind();
                }
                else
                {

                    cbx_objetivo.Disabled = true;                
                    btn_objetivo.Disabled = true;
                    cbx_objetivo.EmptyText = "No hay objetivos para registrar";
                }
               

            }
            else
            {

                txt_Identificador.Text = Session["accion"].ToString();

                DataTable dt = admin.consultas("select pa.nombre, pa.version, pa.comentarios, pa.descripcion , pa.importancia, pa.urgencia, pa.estado, pa.estabilidad, pa.comentarios"
                 + " from plantilla_objetivo pa where pa.proyecto='" + Session["proyecto"] + "' and pa.referencia='" + Session["accion"] + "'");

                txt_nombre.Text = dt.Rows[0]["nombre"].ToString();
                txt_descripcion.Text = dt.Rows[0]["descripcion"].ToString();
                txt_Comentarios.Text = dt.Rows[0]["comentarios"].ToString();
                txt_Version.Text = dt.Rows[0]["version"].ToString();
                cbx_Importancia.Text = dt.Rows[0]["importancia"].ToString();
                cbx_Urgencia.Text = dt.Rows[0]["urgencia"].ToString();
                cbx_Estado.Text = dt.Rows[0]["estado"].ToString();
                cbx_Estabilidad.Text = dt.Rows[0]["estabilidad"].ToString();
                txt_Comentarios.Text = dt.Rows[0]["comentarios"].ToString();
                

                dt = admin.consultas("SELECT u.id_usuario AS Login, u.organizacion AS Organizacion, CONCAT(u.nombres,' ',u.apellidos) Nombre "
                     + " FROM usuario u "
                    + " INNER JOIN autor_objetivo aa on u.id_usuario= aa.usuario where aa.proyecto='" + Session["proyecto"] + "' and aa.objetivo='" + Session["accion"] + "'");

                grilla_autores.GetStore().DataSource = dt;
                grilla_autores.GetStore().DataBind();
                grilla_autores.Collapsed = false;

                dt = admin.consultas(" SELECT f.id AS Identificacion, CONCAT(f.nombre,' ',f.apellido) AS Nombre, "
                 + " f.organizacion AS Organizacion "
                 + " FROM fuente f inner join fuente_objetivo pf on f.id= pf.fuente where pf.proyecto='" + Session["proyecto"] + "' and pf.objetivo='" + Session["accion"] + "'");


                grilla_fuentes.GetStore().DataSource = dt;
                grilla_fuentes.GetStore().DataBind();
                grilla_fuentes.Collapsed = false;

                Button1.Hidden = true;
                btn_act.Hidden = false;
                btn_eliminar1.Disabled = true;
                Button3.Disabled = true;
                txt_nombre.ReadOnly = false;

                FormPanel1.TopBar.Add(toolbar1);

                dt = admin.consultas("SELECT CONCAT(nombres,' ',apellidos) AS Nombre,id_usuario AS Login, "
                       + " organizacion AS Organizacion FROM usuario u inner join autor_proyecto ap on u.id_usuario= ap.usuario "
                       + " where ap.proyecto='" + Session["proyecto"] + "'");

                store_autores.DataSource = dt;
                store_autores.DataBind();

                    //Objetivos

                    dt = admin.consultas("select ob.objetivo as id, n.nombre as Nombre from objetivo_objetivo ob inner join plantilla_objetivo n on ob.objetivo=n.referencia and n.proyecto='"+Session["proyecto"]+"' where ob.proyecto='" + Session["proyecto"] + "'" +
                                        "and ob.objetivo2='" + Session["accion"] + "'");
                    grilla_objetivos.GetStore().DataSource = dt;
                    grilla_objetivos.GetStore().DataBind();
                    grilla_objetivos.Collapsed = false;
                    objetivoc = dt.Rows.Count;

                    dt = admin.consultas("SELECT referencia AS id, nombre AS Nombre FROM plantilla_objetivo po WHERE  po.proyecto='" + Session["proyecto"] + "'");

                    if (dt.Rows.Count > 0)
                    {

                        store_objetivo.DataSource = dt;
                        store_objetivo.DataBind();
                    }
                    else
                    {

                        cbx_objetivo.Disabled = true;
                        btn_objetivo.Disabled = true;
                        cbx_objetivo.EmptyText = "No hay objetivos para registrar";
                    }



                    dt = admin.consultas("SELECT CONCAT(nombre,' ',apellido) AS Nombre,id , organizacion AS Organizacion "
                            + " FROM fuente f inner join proyecto_fuente pf on (f.id= pf.id_fuente and f.organizacion= pf.id_organizacion) "
                            + " where pf.proyecto='" + Session["proyecto"] + "'");

                store_fuente.DataSource = dt;
                store_fuente.DataBind();


            }


        }
        }

        [DirectMethod(ShowMask = true, Msg = "Creando Plantilla ...", Target = MaskTarget.Page)]
        public void CrearPlantilla(string[] datos, Newtonsoft.Json.Linq.JArray [] lista)
        {
            try
            {

                if (lista[0].Count >= 1 && lista[1].Count >= 1)
                {

                    string sql = "insert into plantilla_objetivo(proyecto, referencia, nombre, version, descripcion, importancia, urgencia, estado, estabilidad, comentarios, login, fecha) "
                 + " values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}', now())";

                    admin.insert(string.Format(sql, Session["proyecto"], datos[0], datos[1], datos[2], datos[3], datos[4], datos[5], datos[6], datos[7], datos[8], Session["usuario"]));

                    //***ingresar autores
                    for (int i = 0; i < lista[0].Count; i++)
                    {

                        admin.insert("insert into autor_objetivo(proyecto, usuario, proyecto_objetivo, objetivo, fecha) "
                       + " values('" + Session["proyecto"] + "','" + lista[0][i]["Login"] + "','" + Session["proyecto"] + "','" + datos[0] + "' , now())");
                    }


                    //***ingresar fuentes
                    for (int i = 0; i < lista[1].Count; i++)
                    {

                        admin.insert("insert into fuente_objetivo(fuente, organizacion ,proyecto, proyecto_objetivo, objetivo, fecha) "
                       + " values('" + lista[1][i]["Identificacion"] + "', '" + lista[1][i]["Organizacion"] + "','" + Session["proyecto"] + "', "
                       + " '" + Session["proyecto"] + "' ,'" + datos[0] + "' , now())");
                    }


                    //***ingresar objetivos asociados
                    for (int i = 0; i < lista[2].Count; i++)
                    {

                        admin.insert("insert into objetivo_objetivo(proyecto, objetivo, proyecto2, objetivo2, fecha) "
                       + " values('" + Session["proyecto"] + "','" + lista[2][i]["id"] + "','" + Session["proyecto"] + "','" + datos[0] + "' , now())");
                    }

                    X.Msg.Alert("Notificación", "La plantilla se ha creado con éxito.", new JFunction { Fn = "showResult" }).Show();
                }
                else
                {
                    if (lista[0].Count <= 0 && lista[1].Count <= 0) throw new Exception("Error, la plantilla debe contener fuentes y autores");
                    else if (lista[1].Count <= 0) throw new Exception("Error, la plantilla debe contener fuentes");
                    else throw new Exception("Error, la plantilla debe contener autores");
                }

             }
            catch (Exception s)
            {
                X.Msg.Show(new MessageBoxConfig
                {
                    Title = "Error",
                    Message = s.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = (MessageBox.Icon)Enum.Parse(typeof(MessageBox.Icon), "ERROR"),

                });
            }
        }


         [DirectMethod]
        public void valor(string valor)
        {
            val1 = valor;
        }

         [DirectMethod]
        public void valor2(string valor)
        {
            val2 = valor;
        }

         [DirectMethod]
         public void valor3(string valor)
         {
             val3 = valor;
         }


        [DirectMethod(ShowMask = true, Msg = "Registrando Autor...", Target = MaskTarget.Page)]
        public void validar(Newtonsoft.Json.Linq.JArray autores, string l)
        {
            try
            {
                for (int j = 0; j < autores.Count; j++)
                {
                    if (autores[j]["Login"].ToString().Equals(l)) grilla_autores.GetStore().RemoveAt(j);
                }

            }
            catch (Exception s)
            {
                X.Msg.Show(new MessageBoxConfig
                {
                    Title = "Error",
                    Message = s.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = (MessageBox.Icon)Enum.Parse(typeof(MessageBox.Icon), "ERROR"),

                });
            }
        }


        [DirectMethod(ShowMask = true, Msg = "Registrando Fuente...", Target = MaskTarget.Page)]
        public void validar2(Newtonsoft.Json.Linq.JArray fuentes, string l)
        {
            try
            {
                for (int j = 0; j < fuentes.Count; j++)
                {
                    if (fuentes[j]["Identificacion"].ToString().Equals(l)) grilla_fuentes.GetStore().RemoveAt(j);
                }

            }
            catch (Exception s)
            {
                X.Msg.Show(new MessageBoxConfig
                {
                    Title = "Error",
                    Message = s.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = (MessageBox.Icon)Enum.Parse(typeof(MessageBox.Icon), "ERROR"),

                });
            }
        }



        [DirectMethod(ShowMask = true, Msg = "Registrando Objetivo...", Target = MaskTarget.Page)]
        public void validar3(Newtonsoft.Json.Linq.JArray objetivos, string l)
        {
            try
            {
                for (int j = 0; j < objetivos.Count; j++)
                {
                    if (objetivos[j]["id"].ToString().Equals(l)) grilla_objetivos.GetStore().RemoveAt(j);
                }

            }
            catch (Exception s)
            {
                X.Msg.Show(new MessageBoxConfig
                {
                    Title = "Error",
                    Message = s.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = (MessageBox.Icon)Enum.Parse(typeof(MessageBox.Icon), "ERROR"),

                });
            }
        }


        public void ingresarAutor(object sender, DirectEventArgs e)
        {
            try
            {

                if (cbx_autores.Text != string.Empty)
                {

                    string script2 = "App.direct.validar(App.grilla_autores.getRowsValues(),'" + val1 + "');";
                    X.Js.AddScript(script2);

                    DataTable dt = admin.consultas("select concat( nombres,' ',apellidos) as Nombre, "
                         + "  organizacion as Organizacion from usuario where id_usuario='" + val1 + "' ");

                    grilla_autores.GetStore().Add(new { Nombre = dt.Rows[0]["Nombre"], Organizacion = dt.Rows[0]["Organizacion"], Login = val1 });
                    cbx_autores.Reset();
                    grilla_autores.Collapsed = false;

                }
                else throw new Exception("Seleccione un autor");
            }
            catch (Exception s)
            {
                X.Msg.Show(new MessageBoxConfig
                {
                    Title = "Error",
                    Message = s.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = (MessageBox.Icon)Enum.Parse(typeof(MessageBox.Icon), "ERROR"),

                });
            }
        }

        public void ingresarFuente(object sender, DirectEventArgs e)
        {
            try
            {

                if (cbx_fuentes.Text != string.Empty)
                {

                    string script2 = "App.direct.validar2(App.grilla_fuentes.getRowsValues(),'" + val2 + "');";
                    X.Js.AddScript(script2);

                    DataTable dt = admin.consultas("SELECT CONCAT(nombre,' ',apellido) AS Nombre, id as Identificacion, "
                             +" organizacion AS Organizacion FROM fuente f inner join proyecto_fuente pf on (pf.id_fuente= f.id and "
                              + "   pf.id_organizacion= f.organizacion) where pf.id_fuente='" + val2 + "' and pf.proyecto='" + Session["proyecto"] + "'");

                    grilla_fuentes.GetStore().Add(new { Nombre = dt.Rows[0]["Nombre"], Organizacion = dt.Rows[0]["Organizacion"], Identificacion = val2 });
                    cbx_fuentes.Reset();
                    grilla_fuentes.Collapsed = false;

                }
                else throw new Exception("Seleccione una Fuente");
            }
            catch (Exception s)
            {
                X.Msg.Show(new MessageBoxConfig
                {
                    Title = "Error",
                    Message = s.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = (MessageBox.Icon)Enum.Parse(typeof(MessageBox.Icon), "ERROR"),

                });
            }
        }


        public void ingresarObjetivo(object sender, DirectEventArgs e)
        {
            try
            {

                if (cbx_objetivo.Text != string.Empty)
                {

                    string script2 = "App.direct.validar3(App.grilla_objetivos.getRowsValues(),'" + val3 + "');";
                    X.Js.AddScript(script2);

                    DataTable dt = admin.consultas("SELECT nombre as Nombre FROM plantilla_objetivo po WHERE po.referencia= '" + val3 + "' and po.proyecto='" + Session["proyecto"] + "' ");

                    grilla_objetivos.GetStore().Add(new { Nombre = dt.Rows[0]["Nombre"], id = val3});
                    cbx_objetivo.Reset();
                    grilla_objetivos.Collapsed = false;

                }
                else throw new Exception("Seleccione un autor");
            }
            catch (Exception s)
            {
                X.Msg.Show(new MessageBoxConfig
                {
                    Title = "Error",
                    Message = s.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = (MessageBox.Icon)Enum.Parse(typeof(MessageBox.Icon), "ERROR"),

                });
            }
        }

        [DirectMethod(Msg ="Cargando Historial")]
        public void Historial_obj(object sender, DirectEventArgs e)
          {
            try
            {
                string id = txt_Identificador.Text;
                string hi = string.Empty;
                DataTable dt = admin.consultas("SELECT CONCAT(u.nombres,' ', u.apellidos) nombre, pa.fecha "
                 + " FROM usuario u INNER JOIN plantilla_objetivo pa ON u.id_usuario= pa.login where pa.proyecto='" + Session["proyecto"] + "' and pa.referencia='" + id + "' ");

                hi = "Creado por: " + dt.Rows[0]["nombre"].ToString() + " el " + dt.Rows[0]["fecha"].ToString() + "\n" + "<br/>";

                dt = admin.consultas("SELECT CONCAT(u.nombres,' ', u.apellidos) nombre, pa.fecha "
                 + " FROM usuario u INNER JOIN historial_objetivo pa ON u.id_usuario= pa.login where pa.proyecto_objetivo='" + Session["proyecto"] + "' and pa.objetivo='" + id + "' ");


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    hi += "Modificado por: " + dt.Rows[i]["nombre"].ToString() + " el " + dt.Rows[i]["fecha"].ToString() + "\n" + "<br/>";
                }

                Notification.Show(new NotificationConfig
                {
                    Title = "Historial de " + id,
                    Icon = Icon.Information,
                    Width = 600,
                    Height = 400,
                    AutoHide = false,
                    AutoScroll = true,

                    Html = hi
                });
            }
            catch (Exception s)
            {
                X.Msg.Show(new MessageBoxConfig
                {
                    Title = "Error",
                    Message = s.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = (MessageBox.Icon)Enum.Parse(typeof(MessageBox.Icon), "ERROR"),

                });
            }


        }
        protected string version(string vers)
        {
            int ver = Convert.ToInt16(vers);
            ver = ver + 1;

            string v = string.Empty;

            switch (ver.ToString().Length)
            {
                case 1:
                    v = "00" + ver;
                    break;
                case 2:
                    v = "0" + ver;
                    break;
                case 3:
                    v = ver.ToString();
                    break;

            }

            return v;

        }

        [DirectMethod(ShowMask = true, Msg = "Actualizando Plantilla ...", Target = MaskTarget.Page)]
        public void Actualizar(string[] datos, Newtonsoft.Json.Linq.JArray[] lista)
        {

            try
            {

                if (lista[0].Count >= 1 && lista[1].Count >= 1 && datos[0] != string.Empty)
                {

                    string ver = version(datos[4]);

                    admin.insert("update plantilla_objetivo set nombre='" + datos[0] + "', version='" + ver + "', "
                  + " descripcion='" + datos[1] + "',comentarios='" + datos[2] + "', importancia = '" +datos[5] + "',"+
                  " urgencia = '" + datos[6] + "',estado ='" + datos[7] + "', estabilidad ='" + datos[8] + "' "
                  + " where proyecto='" + Session["proyecto"] + "' and referencia='" + datos[3] + "'");



                    //****** actualizar autores*******
                    DataTable aut = admin.consultas("select ap.usuario from autor_objetivo ap where ap.proyecto='" + Session["proyecto"] + "' and ap.objetivo='" + datos[3] + "' ");

                    if (lista[0].Count > aut.Rows.Count)
                    {
                        for (int i = aut.Rows.Count; i < lista[0].Count; i++)
                        {

                            admin.insert("insert into autor_objetivo(proyecto, usuario, proyecto_objetivo, objetivo, fecha) "
                           + " values('" + Session["proyecto"] + "','" + lista[0][i]["Login"] + "', '" + Session["proyecto"] + "', '" + datos[3] + "',now())");
                        }
                    }

                    //***** actualizar fuentes/********

                    DataTable fue = admin.consultas("select pf.fuente from fuente_objetivo pf where pf.proyecto='" + Session["proyecto"] + "' and pf.objetivo= '" + datos[3] + "'");
                    if (lista[1].Count > fue.Rows.Count)
                    {
                        for (int i = fue.Rows.Count; i < lista[1].Count; i++)
                        {
                            admin.insert("insert into fuente_objetivo(fuente, organizacion, proyecto, proyecto_objetivo, objetivo, fecha) "
                                               + " values('" + lista[1][i]["Identificacion"] + "','" + lista[1][i]["Organizacion"] + "','" + Session["proyecto"] + "' , '" + Session["proyecto"] + "', '" + datos[3] + "', now())");
                        }
                    }

                    // Actualizar objetivos

                    if (lista[2].Count > objetivoc)
                    {
                        for (int i = objetivoc; i < lista[2].Count; i++)
                        {
                            admin.insert("insert into objetivo_objetivo(proyecto, objetivo, proyecto2, objetivo2, fecha) "
                      + " values('" + Session["proyecto"] + "','" + lista[2][i]["id"] + "','" + Session["proyecto"] + "','" + datos[3] + "' , now())");


                        }

                    }


                    txt_Version.Text = ver;

                    admin.insert("insert into historial_objetivo(proyecto_objetivo, objetivo, login, fecha, comentario) "
                + " values('" + Session["proyecto"] + "','" + datos[3] + "','" + Session["usuario"] + "',now(), null)");

                    X.Msg.Show(new MessageBoxConfig
                    {
                        Title = "Confirmación",
                        Message = "Plantilla " + datos[3] + " Actualizado Con Éxito",
                        Buttons = MessageBox.Button.OK,
                        Icon = (MessageBox.Icon)Enum.Parse(typeof(MessageBox.Icon), "INFO"),

                    });
                }
                else
                {
                    if (datos[0] == string.Empty) throw new Exception("Error, favor ingresar un nombre");
                }

            }
            catch (Exception s)
            {
                X.Msg.Show(new MessageBoxConfig
                {
                    Title = "Error",
                    Message = s.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = (MessageBox.Icon)Enum.Parse(typeof(MessageBox.Icon), "ERROR"),

                });
            }




        }


    }

   
}
 
 