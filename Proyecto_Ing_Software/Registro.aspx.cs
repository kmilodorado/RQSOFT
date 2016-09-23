using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using RqSoft.LogicaDeNegocio;
using RqSoft.Entidades;
using System.Text;
using System.Net;
using System.Net.Mail;
using Ext.Net;
using System.Net.Mime;

namespace Proyecto_Ing_Software
{
    public partial class Registro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

         
        public void registrar(object sender, DirectEventArgs e)
        {
            try
            {
                Usuario user = new Usuario();
                user.nombre = txt_Nombre.Text;
                user.apellido = txt_Apellidos.Text;
                user.contraseña = txt_Password.Text;
                user.email = txt_email.Text;
                user.id_usuario = txt_Login.Text;
                user.organizacion = txt_org.Text;

               admin.ingresar_usuario(user);
                en_correo(user.email,user.contraseña,user.id_usuario);

             

             /*   X.Msg.Show(new MessageBoxConfig
                {
                    Title = "Confirmación",
                    Message = "Usuario Registrado Con Éxito",
                    Buttons = MessageBox.Button.OK,
                    Icon = (MessageBox.Icon)Enum.Parse(typeof(MessageBox.Icon), "INFO"),

                });*/

                X.Msg.Alert("Notificación", "Usuario registrado con éxito.", new JFunction { Fn = "showResult" }).Show();

                FormPanel1.Reset();

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

        public void en_correo(string correo,string contra, string usuario)
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
               "<h3> Usuario: "+ usuario +"  </h3> "+
               "<h3> Contraseña: " + contra + " </h3> " + "</BR>"+ "</BR>" +

               "<img src='cid:logo' />";

                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(html, Encoding.UTF8,MediaTypeNames.Text.Html);
                LinkedResource img = new LinkedResource(HttpRuntime.AppDomainAppPath + @corre.Rows[0]["dir_logo"].ToString(),
                                        MediaTypeNames.Image.Jpeg);
                img.ContentId = "logo";
                htmlView.LinkedResources.Add(img);
                mail.AlternateViews.Add(htmlView);
                mail.To.Add(correo);
                SmtpServer.Port =Int32.Parse(corre.Rows[0]["puerto"].ToString());
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
    }
}