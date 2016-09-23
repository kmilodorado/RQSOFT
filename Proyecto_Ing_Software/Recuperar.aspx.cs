using Ext.Net;
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
    public partial class Recuperar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                en_correo(TextBox1.Text);
                 Response.Write("<script language=javascript>if(confirm('Se ha enviado a su correo un mensaje con los datos')==true){ location.href='Log.aspx';}else { location.href='Index.html';}</script>");
            }
            catch(Exception ex)
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

        public void en_correo(string correo)
        {
            Random rnd = new Random();
            int tok = rnd.Next(10000, 90000);
            DataTable dt= admin.consultas("select * from usuario where email='"+correo+"'");
            if (dt.Rows.Count>0)
            {

            
            admin.insert("update usuario set tok='"+tok+"' where email='"+correo+"'");
            DataTable corre = admin.correo();
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(corre.Rows[0]["smtp"].ToString());
                mail.From = new MailAddress(corre.Rows[0]["correo"].ToString(), corre.Rows[0]["nombre_sw"].ToString(), Encoding.UTF8);
                mail.Subject = "Olvido su contraseña RQSoft";
                string html = "<h1>Puedes recuperar tu contraseña con el siguiente link</h1>" +
                    "</br> <h3> <a href="+corre.Rows[0]["url"]+"contras.aspx?tok="+tok+"> Link para recuperar tu contraseña</a></h3>" +

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
            else
            {
                Response.Write("<script language=javascript>if(confirm('No existe este correo en la Base de Datos. por favor verifica')==true){ location.href='';}else { location.href='Index.html';}</script>");

            }
        }


    }
}
