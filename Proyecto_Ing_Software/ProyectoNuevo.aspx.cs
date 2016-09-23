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
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace Proyecto_Ing_Software
{

    public partial class ProyectoNuevo : System.Web.UI.Page
    {
      public static string val1 = string.Empty;
      public static Boolean actfuente = false;
  

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {

                DataTable dt = admin.consultas("select concat( nombres,' ',apellidos) as Nombre,id_usuario as Login, organizacion as Organizacion from usuario");
                store_autores.DataSource = dt;
                store_autores.DataBind();

                if (Session["accion"].Equals("crear"))
                {

                }
                else
                {

                    //**** cargar datos del proyecto********

                    DataSet ds = admin.consultarDS("select p.nombre, p.fecha, p.version from proyecto p where p.codigo='" + Session["proyecto"] + "'; "
                    + " SELECT u.id_usuario AS Login, u.organizacion AS Organizacion, "
                        + " CONCAT(u.nombres,' ',u.apellidos) AS Nombre "
                        + " FROM usuario u inner join autor_proyecto ap on u.id_usuario= ap.usuario where ap.proyecto='" + Session["proyecto"] + "'; "
                    + " SELECT f.id AS Identificacion, CONCAT(f.nombre,' ',f.apellido) AS Nombre, "
                        + "f.organizacion AS Organizacion "
                        + " FROM fuente f inner join proyecto_fuente pf on f.id= pf.id_fuente where pf.proyecto='" + Session["proyecto"] + "';");


                    dt = ds.Tables[0];
                    txt_nombre.Text = dt.Rows[0]["nombre"].ToString();
                    txt_fecha.Text = Convert.ToDateTime(dt.Rows[0]["fecha"]).ToString("yyyy-MM-dd");
                    txt_version.Text = dt.Rows[0]["version"].ToString();


                    dt = ds.Tables[1];
                    grilla_autores.GetStore().DataSource = dt;
                    grilla_autores.GetStore().DataBind();
                    grilla_autores.Collapsed = false;


                    dt = ds.Tables[2];

                    grilla_fuentes.GetStore().DataSource = dt;
                    grilla_fuentes.GetStore().DataBind();
                    grilla_fuentes.Collapsed = false;




                    //dt = admin.consultas("select p.nombre, p.fecha, p.version from proyecto p where p.codigo='" + Session["proyecto"] + "'");
                    //txt_nombre.Text = dt.Rows[0]["nombre"].ToString();
                    //txt_fecha.Text = Convert.ToDateTime(dt.Rows[0]["fecha"]).ToString("yyyy-MM-dd");
                    //txt_version.Text = dt.Rows[0]["version"].ToString();


                    //dt = admin.consultas("SELECT u.id_usuario AS Login, u.organizacion AS Organizacion, "
                    //    + " CONCAT(u.nombres,' ',u.apellidos) AS Nombre "
                    //    + " FROM usuario u inner join autor_proyecto ap on u.id_usuario= ap.usuario where ap.proyecto='" + Session["proyecto"] + "'");

                    //grilla_autores.GetStore().DataSource = dt;
                    //grilla_autores.GetStore().DataBind();
                    //grilla_autores.Collapsed = false;


                    //dt = admin.consultas("SELECT f.id AS Identificacion, CONCAT(f.nombre,' ',f.apellido) AS Nombre, "
                    //    + "f.organizacion AS Organizacion "
                    //    + " FROM fuente f inner join proyecto_fuente pf on f.id= pf.id_fuente where pf.proyecto='" + Session["proyecto"] + "'");

                    //grilla_fuentes.GetStore().DataSource = dt;
                    //grilla_fuentes.GetStore().DataBind();
                    //grilla_fuentes.Collapsed = false;


                    //*****************************************

                    FormPanel1.TopBar.Add(toolbar1);
                    Button1.Hidden = true;
                    btn_act.Hidden = false;
                    btn_eliminar1.Disabled=false;
                    Button2.Disabled = true;
                    txt_nombre.ReadOnly = true;


                }
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

       /* public void registrar(object sender, DirectEventArgs e)
        {
            try
            {
              
                string dd = txt_fecha.Text;
                Proyecto obj = new Proyecto();
                obj.nombre = txt_nombre.Text;
                obj.fecha = Convert.ToDateTime(txt_fecha.Text).ToString("yyyy-MM-dd");
                obj.version = txt_version.Text;

                admin.ingresar_proyecto(obj);

                X.Msg.Show(new MessageBoxConfig
                {
                    Title = "Confirmación",
                    Message = "Proyecto creado Con Éxito",
                    Buttons = MessageBox.Button.OK,
                    Icon = (MessageBox.Icon)Enum.Parse(typeof(MessageBox.Icon), "INFO"),

                });

                FormPanel1.Reset();
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
        }*/


         [DirectMethod(ShowMask = true, Msg = "Creando Proyecto ...", Target = MaskTarget.Page)]
        public void CrearProyecto(string nombre, string fecha, Newtonsoft.Json.Linq.JArray autores, Newtonsoft.Json.Linq.JArray fuentes)
        {
            try
            {

                if (autores.Count >= 1 && fuentes.Count >= 1)
                {

                    Proyecto obj = new Proyecto();
                    obj.nombre = nombre;
                    obj.fecha = Convert.ToDateTime(fecha.Replace('"', ' ')).ToString("yyyy-MM-dd");
                    obj.version = txt_version.Text;
                    obj.login = Session["usuario"].ToString();

                 string codigo=  admin.ingresar_proyecto(obj);

                    for (int i = 0; i < autores.Count; i++)
                    {

                        admin.insert("insert into autor_proyecto(proyecto, usuario, fecha) values('"+codigo+"','"+autores[i]["Login"]+"', now())");
                        en_correo(autores[i]["Login"].ToString(),codigo);
                    }


                    for (int i = 0; i < fuentes.Count; i++)
                    {


                        admin.insert("insert into proyecto_fuente(id_fuente, id_organizacion, proyecto, fecha) "
                       +" values('"+fuentes[i]["Identificacion"]+"','" + fuentes[i]["Organizacion"] + "','"+codigo+"' , now())");


                    }

                    grilla_autores.GetStore().RemoveAll();
                    grilla_fuentes.GetStore().RemoveAll();

                 /*   X.Msg.Show(new MessageBoxConfig
                    {
                        Title = "Confirmación",
                        Message = "Proyecto creado Con Éxito",
                        Buttons = MessageBox.Button.OK,
                        Icon = (MessageBox.Icon)Enum.Parse(typeof(MessageBox.Icon), "INFO"),

                    });*/
                    Session["accion"] = string.Empty;

                    FormPanel1.Reset();

                    X.Msg.Alert("Notificación", "Proyecto creado con éxito.", new JFunction { Fn = "showResult" }).Show();

                 //   X.Js.AddScript("parentAutoLoadControl.hide();");
                }
                else
                {                   
                    if (autores.Count <= 0 && fuentes.Count<=0) throw new Exception("Error, el proyecto debe contener fuentes y autores");
                    else if (fuentes.Count <= 0) throw new Exception("Error, el proyecto debe contener fuentes");
                    else throw new Exception("Error, el proyecto debe contener autores");
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

        public void en_correo(  string usuario, string proyecto)
        {
            DataTable corre = admin.correo();
            
            DataSet ds = admin.consultarDS("select nombre from proyecto where codigo='" + proyecto + "';"+
                "select email from usuario where id_usuario='"+usuario+"';");
            DataTable datos_proyec = ds.Tables[0];

            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(corre.Rows[0]["smtp"].ToString());
                mail.From = new MailAddress(corre.Rows[0]["correo"].ToString(), corre.Rows[0]["nombre_sw"].ToString(), Encoding.UTF8);
                mail.Subject = "Notificación RQSoft";
                string html = "<h1>Has sido vinculado al siguiente proyecto:"+ datos_proyec.Rows[0]["nombre"]+"</h1>" +
               "<h3> Pudes verificar ingresando a la plataforma para obtener mas información </h3>" +
                "</br> <h3> <a href="+corre.Rows[0]["url"]+"> Link para acceder a la plataforma</a></h3>"+
                "</br>" +


               "<img src='cid:logo' />";

                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(html, Encoding.UTF8, MediaTypeNames.Text.Html);
                LinkedResource img = new LinkedResource(HttpRuntime.AppDomainAppPath + @corre.Rows[0]["dir_logo"].ToString(),
                                        MediaTypeNames.Image.Jpeg);
                img.ContentId = "logo";
                htmlView.LinkedResources.Add(img);
                mail.AlternateViews.Add(htmlView);
                datos_proyec = ds.Tables[1];
                mail.To.Add(datos_proyec.Rows[0]["email"].ToString());
                SmtpServer.Port = Int32.Parse(corre.Rows[0]["puerto"].ToString());
                SmtpServer.Credentials = new System.Net.NetworkCredential(corre.Rows[0]["correo"].ToString(), corre.Rows[0]["contrase"].ToString());
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                X.Msg.Show(new MessageBoxConfig
                {
                    Title = "Error",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = (MessageBox.Icon)Enum.Parse(typeof(MessageBox.Icon), "ERROR"),

                });
            }
        }
    



    [DirectMethod(ShowMask = true, Msg = "Actualizando Proyecto ...", Target = MaskTarget.Page)]
         public void actualizarProyecto(string fecha, string v,Newtonsoft.Json.Linq.JArray autores, Newtonsoft.Json.Linq.JArray fuentes)
         {
             try
             {

             if (autores.Count >= 1 && fuentes.Count >= 1 && fecha!= string.Empty)
             {

                 string ver = version(v);

                 admin.insert("update proyecto set version='"+ver+"', fecha='"+Convert.ToDateTime(fecha.Replace('"', ' ')).ToString("yyyy-MM-dd")+"' where codigo='"+Session["proyecto"]+"'  ");

             
                 //****** actualizar autores*******
                 DataTable aut = admin.consultas("select ap.usuario from autor_proyecto ap where ap.proyecto='"+Session["proyecto"]+"'");

                 if (autores.Count > aut.Rows.Count)
                 {
                     for (int i = aut.Rows.Count; i < autores.Count; i++)
                     {

                         admin.insert("insert into autor_proyecto(proyecto, usuario, fecha) values('" + Session["proyecto"] + "','" + autores[i]["Login"] + "', now())");
                            en_correo(Session["proyecto"].ToString(),autores[i]["Login"].ToString());
                        }
                 }

                 //***** actualizar fuentes/********

                 DataTable fue = admin.consultas("select pf.id_fuente from proyecto_fuente pf where pf.proyecto='" + Session["proyecto"] + "'");
                 if (fuentes.Count > fue.Rows.Count)
                 {
                     for (int i = fue.Rows.Count; i < fuentes.Count; i++)
                     {
                         admin.insert("insert into proyecto_fuente(id_fuente, id_organizacion, proyecto, fecha) "
                                            + " values('" + fuentes[i]["Identificacion"] + "','" + fuentes[i]["Organizacion"] + "','" + Session["proyecto"] + "' , now())");
                     }
                 }



                 txt_version.Text = ver;
                 admin.insert("insert into historial_proyecto(proyecto, login, fecha, comentario) "
             + " values('" + Session["proyecto"] + "','" + Session["usuario"] + "',now(), null)");


                  X.Msg.Show(new MessageBoxConfig
                    {
                        Title = "Confirmación",
                        Message = "Proyecto "+txt_nombre.Text+" Actualizado Con Éxito",
                        Buttons = MessageBox.Button.OK,
                        Icon = (MessageBox.Icon)Enum.Parse(typeof(MessageBox.Icon), "INFO"),

                    });
             }
             else
             {
                 if (autores.Count <= 0 && fuentes.Count <= 0) throw new Exception("Error, el proyecto debe contener fuentes y autores");
                 else if (fuentes.Count <= 0) throw new Exception("Error, el proyecto debe contener fuentes");
                 else throw new Exception("Error, el proyecto debe contener autores");
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
                 if (txt_idfuente.Text !=string.Empty && txt_nombre_fuente.Text != string.Empty &&
                     txt_apellido_fuente.Text != string.Empty && cbx_fuente_org.Text !=string.Empty)
                 {
                     if (admin.existe("select nombre from fuente where id='" + txt_idfuente.Text + "' and organizacion='" + cbx_fuente_org.Text + "'") != true)
                     {
                         admin.insert("insert into fuente(id,organizacion,nombre,apellido,login, fecha) "
                        + " values('" + txt_idfuente.Text + "','" + cbx_fuente_org.Text + "','" + txt_nombre_fuente.Text + "', "
                         + " '" + txt_apellido_fuente.Text + "', '" + Session["usuario"].ToString() + "', now())");


                     }
                     else
                     {
                         if (actfuente == true)
                         {
                             admin.insert("update fuente set nombre='"+txt_nombre_fuente.Text+"', "
                             +"  apellido='"+txt_apellido_fuente.Text+"' where id='"+txt_idfuente.Text+"' "
                             +"  and organizacion='"+cbx_fuente_org.Text+"'");

                         }
                     }

                     string script2 = "App.direct.validar2(App.grilla_fuentes.getRowsValues(),'" + txt_idfuente.Text + "');";
                            X.Js.AddScript(script2);

                         grilla_fuentes.GetStore().Add(new { Identificacion = txt_idfuente.Text,
                         Organizacion = cbx_fuente_org.Text, Nombre = txt_nombre_fuente.Text + " " + txt_apellido_fuente.Text
                         });
                         grilla_fuentes.Collapsed = false;
                         txt_nombre_fuente.Reset();
                         txt_apellido_fuente.Reset();
                         txt_idfuente.Reset();                        
                         cbx_fuente_org.Reset();

                         txt_nombre_fuente.Disabled = true;
                         txt_apellido_fuente.Disabled = true;
                         cbx_fuente_org.Disabled = true;
                         cbx_fuente_org.GetStore().RemoveAll();

                         btn_ingresar_fuente.Disabled = true;
                     
                 }
                 else throw new Exception("Error, favor llenar todos los campos");
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

         public void buscarFuente(object sender, DirectEventArgs e)
         {


             try
             {
                 if (txt_idfuente.Text != string.Empty)
                 {
                     DataTable dt = admin.consultas("select  * from fuente where id='"+txt_idfuente.Text+"'");

                     if (dt.Rows.Count <= 0)
                     {
                         txt_nombre_fuente.Disabled = false;
                         txt_apellido_fuente.Disabled = false;
                         cbx_fuente_org.Disabled = false;

                         txt_nombre_fuente.ReadOnly = false;
                         txt_apellido_fuente.ReadOnly = false;

                     }
                     else 
                     {
                        txt_nombre_fuente.Text = dt.Rows[0]["nombre"].ToString();
                         txt_apellido_fuente.Text = dt.Rows[0]["apellido"].ToString();

                         if (Session["usuario"].ToString().Equals(dt.Rows[0]["login"].ToString()))
                         {
                             txt_nombre_fuente.ReadOnly = false;
                             txt_apellido_fuente.ReadOnly = false;
                             actfuente = true;

                         }
                         else
                         {
                             txt_nombre_fuente.ReadOnly = true;
                             txt_apellido_fuente.ReadOnly = true;
                         }

                         txt_nombre_fuente.Disabled = false;
                         txt_apellido_fuente.Disabled = false;
                        

                         for (int i = 0; i < dt.Rows.Count; i++)
                         {
                             cbx_fuente_org.AddItem(dt.Rows[i]["organizacion"].ToString(), dt.Rows[i]["organizacion"].ToString());                         
                         
                         }
                         cbx_fuente_org.Disabled = false;
                     }

                     btn_ingresar_fuente.Disabled = false;
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


        //*****************
        
         [DirectMethod]
         public void valor(string valor)
         {
             val1 = valor;
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


         [DirectMethod(Msg = "Creando documento de requisitos...")]
         public void Crear(object sender, DirectEventArgs e)
         {
             try
             {
                 ReportesPDF.reporte documento = new ReportesPDF.reporte();              
                   string ruta= documento.documento(Session["usuario"].ToString(), Session["proyecto"].ToString());

                // ClientScript.RegisterStartupScript(this.GetType(), "abrir", "abrir('" + ruta+ "', '_Blank');", true);
             //  Response.Write("<script language=javascript>if(confirm('alerta "+ruta+"')==true){ location.href='Log.aspx';}else { location.href='Index.html';}</script>");
             

                Ext.Net.X.Js.Call("window.open", ruta, "_Blank");

                


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

         [DirectMethod(Msg = "Cargando historial...")]
         public void Historial(object sender, DirectEventArgs e)
         {
             try
             {
                 string id = Session["proyecto"].ToString();
                 string hi = string.Empty;
                 DataTable dt = admin.consultas("SELECT CONCAT(u.nombres,' ', u.apellidos) nombre, pa.fechaReal "
                  + " FROM usuario u INNER JOIN proyecto pa ON u.id_usuario= pa.login where pa.codigo='" + id + "' ");

                 hi = "Creado por: " + dt.Rows[0]["nombre"].ToString() + " el " + dt.Rows[0]["fechaReal"].ToString() + "\n" + "<br/>";


                DataTable Creados = admin.consultas("SELECT CONCAT(u.nombres,' ', u.apellidos) nombre, pa.fecha,pa.referencia "
                 + " FROM usuario u INNER JOIN plantilla_rnf pa ON u.id_usuario= pa.login where pa.proyecto='" + Session["proyecto"] + "'");

                dt = admin.consultas("SELECT CONCAT(u.nombres,' ', u.apellidos) nombre, pa.fecha ,pa.referencia"
                 + " FROM usuario u INNER JOIN plantilla_req_funcional pa ON u.id_usuario= pa.login where pa.proyecto='" + Session["proyecto"] + "'");

                Creados.Merge(dt);

                dt = admin.consultas("SELECT CONCAT(u.nombres, ' ', u.apellidos) nombre, pa.fecha, pa.referencia "
                 + " FROM usuario u INNER JOIN plantilla_objetivo pa ON u.id_usuario= pa.login where pa.proyecto='" + Session["proyecto"] + "'");

                Creados.Merge(dt);

                dt =admin.consultas("SELECT CONCAT(u.nombres,' ', u.apellidos) nombre, pa.fecha, pa.referencia "
                + " FROM usuario u INNER JOIN plantilla_actor pa ON u.id_usuario= pa.login where pa.proyecto='" + Session["proyecto"] + "'");

                Creados.Merge(dt);

                dt =admin.consultas("SELECT CONCAT(u.nombres,' ', u.apellidos) nombre, pa.fecha, pa.referencia "
                 + " FROM usuario u INNER JOIN plantilla_ri pa ON u.id_usuario= pa.login where pa.proyecto='" + Session["proyecto"] + "'");

                Creados.Merge(dt);
                

                for (int i = 0; i <Creados.Rows.Count; i++)
                {
                    hi += "Plantilla: "+Creados.Rows[i]["referencia"].ToString()  + " creada por: " + Creados.Rows[i]["nombre"].ToString() +" el "
                        
                        + Creados.Rows[i]["fecha"]  +  "\n" + "<br/>";
                }


                dt = admin.consultas("SELECT CONCAT(u.nombres,' ', u.apellidos) nombre, pa.fecha ,pa.objetivo as refe"
                  + " FROM usuario u INNER JOIN historial_objetivo pa ON u.id_usuario= pa.login where pa.proyecto_objetivo='" + id + "' ");
                DataTable Acto = admin.consultas("SELECT CONCAT(u.nombres,' ', u.apellidos) nombre, pa.fecha,pa.actor as refe "
                + " FROM usuario u INNER JOIN historial_actor pa ON u.id_usuario= pa.login where pa.proyecto_actor='" + Session["proyecto"] + "' ");
                dt.Merge(Acto);

                DataTable RF = admin.consultas("SELECT CONCAT(u.nombres,' ', u.apellidos) nombre, pa.fecha, pa.rf as refe"
                 + " FROM usuario u INNER JOIN historial_rf pa ON u.id_usuario= pa.login where pa.proyecto_rf='" + Session["proyecto"] + "'");
                dt.Merge(RF);

                DataTable RNF = admin.consultas("SELECT CONCAT(u.nombres,' ', u.apellidos) nombre, pa.fecha,pa.rnf as refe "
                 + " FROM usuario u INNER JOIN historial_rnf pa ON u.id_usuario= pa.login where pa.proyecto_rnf='" + Session["proyecto"] + "'");

                dt.Merge(RNF);

                DataTable RI2 = admin.consultas("SELECT CONCAT(u.nombres,' ', u.apellidos) nombre, pa.fecha , pa.ri as refe"
                 + " FROM usuario u INNER JOIN historial_ri pa ON u.id_usuario= pa.login where pa.proyecto_ri='" + Session["proyecto"] + "'");
                dt.Merge(RI2);

                for (int i = 0; i < dt.Rows.Count; i++)
                 {
                     hi +="Referencia: " + dt.Rows[i]["refe"].ToString() + " modificado por: " + dt.Rows[i]["nombre"].ToString() + " el " + dt.Rows[i]["fecha"].ToString() + "\n" + "<br/>";
                 }

                Notification.Show(new NotificationConfig
                {
                    Title = "Historial",
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
    }
}