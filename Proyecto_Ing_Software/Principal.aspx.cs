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
    public partial class Principal : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["usuario"].ToString() != string.Empty)
                {
                    lb_usuario.Text = Session["nombre"].ToString();
                }
                else
                {
                   

              X.Msg.Show(new MessageBoxConfig
                    {
                        Title = "Error",
                        Message = "Tu sesión ha caducado",
                        Buttons = MessageBox.Button.OK,
                        Icon = (MessageBox.Icon)Enum.Parse(typeof(MessageBox.Icon), "ERROR"),

                    });
                    
                    Response.Redirect("Index.html");
                }
            }
        }

        [DirectMethod(Msg = "Generando...")]
        public void Crear(object sender, DirectEventArgs e)
        {
            try
            {
                ReportesPDF.reporte documento = new ReportesPDF.reporte();
             //   documento.crearpdf();
                documento.documento(Session["usuario"].ToString(), "19");                
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

        public void salir(object sender, DirectEventArgs e)
        {
            try
            {

                Session["usuario"] = string.Empty;
                Response.Redirect("Index.html");
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



        [DirectMethod(ShowMask = true, Msg = "Cargando Proyectos...", Target = MaskTarget.CustomTarget, CustomTarget = "Panel01")]
        public void cargarProyecto()
        {
//            DataTable dt= admin.consultas("select codigo, nombre from proyecto where login='"+Session["usuario"].ToString()+"'");
//            DataTable dt2 = admin.consultas("select codigo, nombre from proyecto inner join autor_proyecto ap on proyecto.codigo= ap.proyecto  "
//+"                          where ap.usuario='" + Session["usuario"].ToString() + "'");
//            dt.Merge(dt2);

            DataSet ds = admin.consultarDS("select codigo, nombre from proyecto where login='" + Session["usuario"].ToString() + "'; "
            +" select codigo, nombre from proyecto inner join autor_proyecto ap on proyecto.codigo= ap.proyecto  "
            + "  where ap.usuario='" + Session["usuario"].ToString() + "';");

            ds.Tables[0].Merge(ds.Tables[1]);

            store_proyectos.DataSource = ds.Tables[0];
            store_proyectos.DataBind();
        
        }

        [DirectMethod(ShowMask = true, Msg = "Cargando los datos del proyecto...", Target = MaskTarget.Page)]
        public void selecProyecto(Boolean c)
        {
            try
            {
                string id_proyecto = cbx_proyecto.SelectedItem.Value;
                if (id_proyecto != string.Empty)
                {


                    borrarNodos();
                 

                    Session["proyecto"] = id_proyecto;
                    Session["accion"] = string.Empty;
                    Node nodo;
                    Node caso;
                    Node clase;

                    DataSet ds = admin.consultarDS("select referencia, nombre from plantilla_objetivo ob where ob.proyecto='" + id_proyecto + "'; "
                    + " select referencia, nombre from plantilla_actor ac where ac.proyecto='" + id_proyecto + "'; "
                    + " select referencia, nombre from plantilla_req_funcional ac where ac.proyecto='" + id_proyecto + "'; "
                    + " select referencia, nombre from plantilla_rnf ac where ac.proyecto='" + id_proyecto + "'; "
                    + " select referencia, nombre from plantilla_ri ac where ac.proyecto='" + id_proyecto + "';");

                    /////**** PLANTILLA OBJETIVOS*******

                  



                    //DataTable dt = admin.consultas("select referencia, nombre from plantilla_objetivo ob where ob.proyecto='" + id_proyecto + "'");

                   


                    //for (int i = 0; i < dt.Rows.Count; i++)
                    //{
                    //    nodo = new Node()
                    //    {
                    //        Text = "(" + dt.Rows[i]["referencia"].ToString() + ") - " + dt.Rows[i]["nombre"].ToString(),
                    //        Leaf = true,
                    //        NodeID = dt.Rows[i]["referencia"].ToString()
                    //    };
                    //    nodo.Icon = Icon.BulletGreen;

                    //    TreePanel1.GetNodeById("objetivos").AppendChild(nodo);
                    //}


                    ////***************************************


                    /////**** PLANTILLA ACTORES*******

                    //dt = admin.consultas("select referencia, nombre from plantilla_actor ac where ac.proyecto='" + id_proyecto + "'");

                    //for (int i = 0; i < dt.Rows.Count; i++)
                    //{
                    //    nodo = new Node()
                    //    {
                    //        Text = "(" + dt.Rows[i]["referencia"].ToString() + ") - " + dt.Rows[i]["nombre"].ToString(),
                    //        Leaf = true,
                    //        NodeID = dt.Rows[i]["referencia"].ToString()
                    //    };
                    //    nodo.Icon = Icon.BulletGreen;
                    //    TreePanel1.GetNodeById("actores").AppendChild(nodo);
                    //}


                    ////***************************************

                    /////**** PLANTILLA REQUISITOS FUNCIONALES*******

                    //dt = admin.consultas("select referencia, nombre from plantilla_req_funcional ac where ac.proyecto='" + id_proyecto + "'");

                    //for (int i = 0; i < dt.Rows.Count; i++)
                    //{
                    //    nodo = new Node()
                    //    {
                    //        Text = "(" + dt.Rows[i]["referencia"].ToString() + ") - " + dt.Rows[i]["nombre"].ToString(),
                    //        Leaf = true,
                    //        NodeID = dt.Rows[i]["referencia"].ToString()
                    //    };
                    //    nodo.Icon = Icon.BulletGreen;
                    //    TreePanel1.GetNodeById("funcionales").AppendChild(nodo);

                    //    caso = new Node()
                    //    {
                    //        Text = "(" + dt.Rows[i]["referencia"].ToString() + ") - " + dt.Rows[i]["nombre"].ToString(),
                    //        Leaf = true,
                    //        NodeID ="caso-" + dt.Rows[i]["referencia"].ToString()
                    //    };
                    //    caso.Icon = Icon.BulletBlue;
                    //    TreePanel1.GetNodeById("casos").AppendChild(caso);

                    //}


                    ////***************************************

                    /////**** PLANTILLA REQUISITOS NO FUNCIONALES*******

                    //dt = admin.consultas("select referencia, nombre from plantilla_rnf ac where ac.proyecto='" + id_proyecto + "'");

                    //for (int i = 0; i < dt.Rows.Count; i++)
                    //{
                    //    nodo = new Node()
                    //    {
                    //        Text = "(" + dt.Rows[i]["referencia"].ToString() + ") - " + dt.Rows[i]["nombre"].ToString(),
                    //        Leaf = true,
                    //        NodeID = dt.Rows[i]["referencia"].ToString()
                    //    };
                    //    nodo.Icon = Icon.BulletGreen;
                    //    TreePanel1.GetNodeById("no_funcionales").AppendChild(nodo);
                    //}


                    ////***************************************

                    /////**** PLANTILLA REQUISITOS INFORMACIÓN*******

                    //dt = admin.consultas("select referencia, nombre from plantilla_ri ac where ac.proyecto='" + id_proyecto + "'");

                    //for (int i = 0; i < dt.Rows.Count; i++)
                    //{
                    //    nodo = new Node()
                    //    {
                    //        Text = "(" + dt.Rows[i]["referencia"].ToString() + ") - " + dt.Rows[i]["nombre"].ToString(),
                    //        Leaf = true,
                    //        NodeID = dt.Rows[i]["referencia"].ToString()
                    //    };
                    //    nodo.Icon = Icon.BulletGreen;
                    //    TreePanel1.GetNodeById("informacion").AppendChild(nodo);



                    //}


                    ////***************************************

                    ///**** PLANTILLA OBJETIVOS*******





                    DataTable dt =ds.Tables[0];

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        nodo = new Node()
                        {
                            Text = "(" + dt.Rows[i]["referencia"].ToString() + ") - " + dt.Rows[i]["nombre"].ToString(),
                            Leaf = true,
                            NodeID = dt.Rows[i]["referencia"].ToString()
                        };
                        nodo.Icon = Icon.BulletGreen;

                        TreePanel1.GetNodeById("objetivos").AppendChild(nodo);
                    }


                    //***************************************


                    ///**** PLANTILLA ACTORES*******

                    dt = ds.Tables[1];

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        nodo = new Node()
                        {
                            Text = "(" + dt.Rows[i]["referencia"].ToString() + ") - " + dt.Rows[i]["nombre"].ToString(),
                            Leaf = true,
                            NodeID = dt.Rows[i]["referencia"].ToString()
                        };
                        nodo.Icon = Icon.BulletGreen;
                        TreePanel1.GetNodeById("actores").AppendChild(nodo);

                        caso = new Node()
                        {
                            Text = "(" + dt.Rows[i]["referencia"].ToString() + ") - " + dt.Rows[i]["nombre"].ToString(),
                            Leaf = true,
                            NodeID = "Actor-" + dt.Rows[i]["referencia"].ToString()
                        };
                        caso.Icon = Icon.BulletBlue;
                        TreePanel1.GetNodeById("casos").AppendChild(caso);
                    }


                    //***************************************

                    ///**** PLANTILLA REQUISITOS FUNCIONALES*******

                    dt = ds.Tables[2];

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        nodo = new Node()
                        {
                            Text = "(" + dt.Rows[i]["referencia"].ToString() + ") - " + dt.Rows[i]["nombre"].ToString(),
                            Leaf = true,
                            NodeID = dt.Rows[i]["referencia"].ToString()
                        };
                        nodo.Icon = Icon.BulletGreen;
                        TreePanel1.GetNodeById("funcionales").AppendChild(nodo);

                      

                    }


                    //***************************************

                    ///**** PLANTILLA REQUISITOS NO FUNCIONALES*******

                    dt = ds.Tables[3];

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        nodo = new Node()
                        {
                            Text = "(" + dt.Rows[i]["referencia"].ToString() + ") - " + dt.Rows[i]["nombre"].ToString(),
                            Leaf = true,
                            NodeID = dt.Rows[i]["referencia"].ToString()
                        };
                        nodo.Icon = Icon.BulletGreen;
                        TreePanel1.GetNodeById("no_funcionales").AppendChild(nodo);
                    }


                    //***************************************

                    ///**** PLANTILLA REQUISITOS INFORMACIÓN*******

                    dt = ds.Tables[4];

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        nodo = new Node()
                        {
                            Text = "(" + dt.Rows[i]["referencia"].ToString() + ") - " + dt.Rows[i]["nombre"].ToString(),
                            Leaf = true,
                            NodeID = dt.Rows[i]["referencia"].ToString()
                        };
                        nodo.Icon = Icon.BulletGreen;
                        TreePanel1.GetNodeById("informacion").AppendChild(nodo);



                    }


                    //***************************************



                    if (c == true) { tabpanel_detalle.RemoveAll(); detalleProyecto(cbx_proyecto.SelectedItem.Text); }

                    Session["accion"] = string.Empty;

                    clase = new Node()
                    {
                        Text = "Diagrama",
                        Leaf = true,
                        NodeID = "Clases"
                    };
                    clase.Icon = Icon.BulletBlue;
                    TreePanel1.GetNodeById("clases").AppendChild(clase);
                }
            
            }
            catch (Exception e)
            {
                X.Msg.Show(new MessageBoxConfig
                {
                    Title = "Error",
                    Message = e.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = (MessageBox.Icon)Enum.Parse(typeof(MessageBox.Icon), "ERROR"),

                });
            }

            //DataTable dt = admin.consultas("select codigo, nombre from proyecto where login='" + Session["usuario"].ToString() + "'");
            //store_proyectos.DataSource = dt;
            //store_proyectos.DataBind();

        }

        public void borrarNodos()
        {
            try
            {

                TreePanel1.GetNodeById("objetivos").RemoveAll();
                TreePanel1.GetNodeById("actores").RemoveAll();
                TreePanel1.GetNodeById("funcionales").RemoveAll();
                TreePanel1.GetNodeById("no_funcionales").RemoveAll();
                TreePanel1.GetNodeById("informacion").RemoveAll();
                TreePanel1.GetNodeById("casos").RemoveAll();
                TreePanel1.GetNodeById("clases").RemoveAll();
                TreePanel1.GetNodeById("secuencia").RemoveAll();

            }
            catch (Exception e)
            {
                X.Msg.Show(new MessageBoxConfig
                {
                    Title = "Error",
                    Message = e.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = (MessageBox.Icon)Enum.Parse(typeof(MessageBox.Icon), "ERROR"),

                });
            }


        }


         [DirectMethod]
        protected void detalle(object sender, DirectEventArgs e)
        {

            try
            {

                string value = e.ExtraParams["Item"].ToString();

                string[] plantilla = value.Split(new Char[] { '-' });
                string pag = string.Empty;

                switch (plantilla[0])
                {
                    case "OBJ":
                        pag = "Objetivo.aspx";
                        break;
                    case "RF":
                        pag = "RequisitoFuncional.aspx";
                        break;
                    case "RNF":
                        pag = "RequisitoNoFuncional.aspx";
                        break;
                    case "RI":
                        pag = "Informacion.aspx";
                        break;
                    case "ACT":
                        pag = "Actor.aspx";
                        break;
                    case "caso":
                        pag = "CasosDeUso.aspx";
                        break;
                }

              
                Session["accion"] = value;

                if (plantilla.Length >= 3)
                {
                    Session["plantilla"] = plantilla[1] + "-" + plantilla[2];


                    Ext.Net.Panel panel = new Ext.Net.Panel
                    {
                        ID = value,
                        Title = "Caso de uso: " + plantilla[1] + "-" + plantilla[2],
                        Closable = true,
                        Layout = "Fit",
                        Loader = new ComponentLoader
                        {
                            Url = "CasosDeUso.aspx",
                            Mode = LoadMode.Frame,
                            LoadMask =
                            {
                                
                                ShowMask = true,
                                Msg = "Cargando ..."
                            }
                        }
                    };
                    tabpanel_detalle.Add(panel);
                    panel.Render();

                    tabpanel_detalle.SetLastTabAsActive();
                }
                else
                {

                    if (value.Equals("Clases"))
                    {

                        Ext.Net.Panel panel = new Ext.Net.Panel
                        {
                            ID = value,
                            Title = value,
                            Closable = true,
                            Layout = "Fit",
                            Loader = new ComponentLoader
                            {
                                Url = "Clases.aspx",
                                Mode = LoadMode.Frame,
                                LoadMask =
                                {
                                    ShowMask = true,
                                    Msg = "Cargando ..."
                                }
                            }
                        };
                        tabpanel_detalle.Add(panel);
                        panel.Render();

                        tabpanel_detalle.SetLastTabAsActive();


                    }

                    else
                    {
                        Ext.Net.Panel panel = new Ext.Net.Panel
                        {
                            ID = value,
                            Title = value,
                            Closable = true,
                            Layout = "Fit",
                            Loader = new ComponentLoader
                            {
                                Url = pag,
                                Mode = LoadMode.Frame,
                                LoadMask =
                                {
                                    ShowMask = true,
                                    Msg = "Cargando ..."
                                }
                            }
                        };
                        tabpanel_detalle.Add(panel);
                        panel.Render();

                        tabpanel_detalle.SetLastTabAsActive();
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


         protected void detalleProyecto(string proyecto)
         {

            
             Ext.Net.Panel panel = new Ext.Net.Panel
             {
                 Title = proyecto,
                 Closable = true,
                 Layout = "Fit",
                 Loader = new ComponentLoader
                 {
                     Url = "ProyectoNuevo.aspx",
                     Mode = LoadMode.Frame,
                     LoadMask =
                     {
                         ShowMask = true,
                         Msg = "Cargando ..."
                     }
                 }
             };
             tabpanel_detalle.Add(panel);
             panel.Render();

             tabpanel_detalle.SetLastTabAsActive();

         }



         
    }
}