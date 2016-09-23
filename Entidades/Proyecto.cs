using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RqSoft.Entidades
{
   public class Proyecto
    {

        string cod = string.Empty;
        string nom = string.Empty;
        string fec = string.Empty;
        string ver = string.Empty;
        string log = string.Empty;
        string fecr = string.Empty;


        public string codigo
        {
            get { return cod; }
            set { cod = value; }
        }

        public string nombre
        {
            get { return nom; }
            set { nom = value; }
        }
        public string fecha
        {
            get { return fec; }
            set { fec = value; }
        }

        public string version
        {
            get { return ver; }
            set { ver = value; }
        }
        public string login
        {
            get { return log; }
            set { log = value; }
        }
        public string fecha_real
        {
            get { return fecr; }
            set { fecr = value; }
        }
    }
}
