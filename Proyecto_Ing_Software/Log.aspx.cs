using Ext.Net;
using RqSoft.Entidades;
using RqSoft.LogicaDeNegocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proyecto_Ing_Software
{
    public partial class Log : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
               
            }
        }
        [DirectMethod(Msg = "Iniciando Sesión", ShowMask = true, Target = MaskTarget.Page)]
        public void IniciarSession()
        {
            try
            {
                
                if (txt_Usuario.Text != string.Empty && txt_Password.Text != string.Empty)
                {
                    Usuario us = new Usuario();

                    us.contraseña = txt_Password.Text;
                    us.id_usuario = txt_Usuario.Text;
                   

                    DataTable d = admin.Iniciar_Session(us);

                    if (d.Rows.Count > 0)
                    {
                        Session["usuario"] = us.id_usuario;
                        Session["nombre"] = d.Rows[0]["nombre"].ToString();
                        Response.Redirect("Principal.aspx");
                    }
                    else
                    {
                        throw new Exception("Usuario incorrecto");
                    }
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
        [DirectMethod(Msg = "Registrando", ShowMask = true, Target = MaskTarget.Page)]
        public void registrar()
        {
            try
            {
                Usuario user = new Usuario();
                user.nombre = nombre.Text;
                user.apellido = apel.Text;
                user.contraseña = con.Text;
                user.email =email.Text;

                user.id_usuario = Usuario(user.nombre, user.apellido);
                user.organizacion = or.Text;

                admin.ingresar_usuario(user);
                en_correo(user.email, user.contraseña, user.id_usuario);



                /*   X.Msg.Show(new MessageBoxConfig
                   {
                       Title = "Confirmación",
                       Message = "Usuario Registrado Con Éxito",
                       Buttons = MessageBox.Button.OK,
                       Icon = (MessageBox.Icon)Enum.Parse(typeof(MessageBox.Icon), "INFO"),

                   });*/

                Response.Write("<script language=javascript>if(confirm('Tu registro a sido exitoso, se han enviado datos a tu correo electronico')==true){ location.href='Log.aspx';}else { location.href='Index.html';}</script>");


                //  X.Js.AddScript("parentAutoLoadControl.hide();");
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


        public static string Usuario(string nombre, string apellido)
        {
            string[] nomb = nombre.Split(' ');
            string[] ape = apellido.Split(' ');
            string usuario= string.Empty;

            for (int i=0; i<nomb[0].Length; i++)
           {
                usuario = nomb[0].Substring(0, i+1) + "_" + ape[0];
                string sql = "select * from usuario where id_usuario ='" + usuario + "'";
                if (admin.existe(sql)==false)
                {
                    i = nomb[0].Length;
                }
          }
          return usuario;
        }

        public void en_correo(string correo, string contra, string usuario)
        {
            DataTable corre = admin.correo();
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(corre.Rows[0]["smtp"].ToString());
                mail.From = new MailAddress(corre.Rows[0]["correo"].ToString(), corre.Rows[0]["nombre_sw"].ToString(), Encoding.UTF8);
                mail.Subject = "Registro RQSoft";
                string html = "<h1>Registro Exitoso:</h1>" +
               "<h3> usted se ha  registrado  satisfactoriamente en la plataforma RQSOFT </h3>" +
               "<h3> Su usuario y contraseña son los siguientes: </h3>" +
               "<h3> Usuario: " + usuario + "  </h3> " +
               "<h3> Contraseña: " + contra + " </h3> " + "</BR>" + "</BR>" +

               "<img src='cid:logo' />";

                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(html, Encoding.UTF8, MediaTypeNames.Text.Html);
                LinkedResource img = new LinkedResource(HttpRuntime.AppDomainAppPath + @corre.Rows[0]["dir_logo"].ToString(),
                                        MediaTypeNames.Image.Jpeg);
                img.ContentId = "logo";
                htmlView.LinkedResources.Add(img);
                mail.AlternateViews.Add(htmlView);
                mail.To.Add(correo);
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

        [DirectMethod(Msg = "Enviando Correo", ShowMask = true, Target = MaskTarget.Page)]
        public void correo()
        {

            try
            {
                contr(TextField1.Text);

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

        public void contr(string correo)
        {
            Random rnd = new Random();
            int tok = rnd.Next(10000, 90000);
            DataTable dt = admin.consultas("select * from usuario where email='" + correo + "'");
            if (dt.Rows.Count > 0)
            {


                admin.insert("update usuario set tok='" + tok + "' where email='" + correo + "'");
                DataTable corre = admin.correo();
                try
                {
                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient(corre.Rows[0]["smtp"].ToString());
                    mail.From = new MailAddress(corre.Rows[0]["correo"].ToString(), corre.Rows[0]["nombre_sw"].ToString(), Encoding.UTF8);
                    mail.Subject = "Olvido su contraseña RQSoft";
                    string html = "<h1>Puedes recuperar tu contraseña con el siguiente link</h1>" +
                        "</br> <h3> <a href=" + corre.Rows[0]["url"] + "contras.aspx?tok=" + tok + "> Link para recuperar tu contraseña</a></h3>" +
                        "</br> <h3> Usuario:'"+dt.Rows[0]["id_usuario"]+"'</h3>" +

                   "<img src='cid:logo' />";

                    AlternateView htmlView = AlternateView.CreateAlternateViewFromString(html, Encoding.UTF8, MediaTypeNames.Text.Html);
                    LinkedResource img = new LinkedResource(HttpRuntime.AppDomainAppPath + @corre.Rows[0]["dir_logo"].ToString(),
                                            MediaTypeNames.Image.Jpeg);
                    img.ContentId = "logo";
                    htmlView.LinkedResources.Add(img);
                    mail.AlternateViews.Add(htmlView);
                    mail.To.Add(correo);
                    SmtpServer.Port = Int32.Parse(corre.Rows[0]["puerto"].ToString());
                    SmtpServer.Credentials = new System.Net.NetworkCredential(corre.Rows[0]["correo"].ToString(), corre.Rows[0]["contrase"].ToString());
                    SmtpServer.EnableSsl = true;
                   
                    SmtpServer.Send(mail);
                    TextField1.Text = "";
                    ResourceManager1.AddScript("Ext.Msg.notify('Recuperar contraseña', 'Se han enviado datos a tu correo!');");

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
            else
            {


                X.Msg.Show(new MessageBoxConfig
                {
                    Title = "Error",
                    Message = "Este correo no existe en la base de datos",
                    Buttons = MessageBox.Button.OK,
                    Icon = (MessageBox.Icon)Enum.Parse(typeof(MessageBox.Icon), "ERROR"),

                });
            }
        }

        
    }
}
