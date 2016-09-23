using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace Proyecto_Ing_Software
{
    public class Global : System.Web.HttpApplication
    {
        string username = string.Empty;
        string nombre = string.Empty;
        string pro = string.Empty;
        string acc = string.Empty;
        string refe = string.Empty;

        public static String path = "";

        void Application_Start(object sender, EventArgs e)
        {
            // Código que se ejecuta al iniciarse la aplicación

        }

        void Application_End(object sender, EventArgs e)
        {
            //  Código que se ejecuta cuando se cierra la aplicación

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Código que se ejecuta al producirse un error no controlado

        }

        void Session_Start(object sender, EventArgs e)
        {
            Session["usuario"] = username;
            Session["nombre"] = nombre;
            Session["proyecto"] = pro;
            Session["accion"] = acc;
            Session["plantilla"] = refe;

        }

        void Session_End(object sender, EventArgs e)
        {
            // Código que se ejecuta cuando finaliza una sesión.
            // Nota: el evento Session_End se desencadena sólo cuando el modo sessionstate
            // se establece como InProc en el archivo Web.config. Si el modo de sesión se establece como StateServer 
            // o SQLServer, el evento no se genera.

        }

    }
}
