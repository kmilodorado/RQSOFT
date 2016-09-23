using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RqSoft.Entidades
{
   public class Usuario
    {
        string id = string.Empty;
        string password = string.Empty;
        string nom = string.Empty;
        string ape = string.Empty;
        string correo = string.Empty;
        string org = string.Empty;



        public string id_usuario
        {
            get { return id; }
            set { id = value; }
        }
        public string contraseña
        {
            get { return password; }
            set { password = value; }
        }
        public string nombre
        {
            get { return nom; }
            set { nom = value; }
        }
        public string apellido
        {
            get { return ape; }
            set { ape = value; }
        }
        public string email
        {
            get { return correo; }
            set { correo = value; }
        }

        public string organizacion
        {
            get { return org; }
            set { org = value; }
        }
    }
}
