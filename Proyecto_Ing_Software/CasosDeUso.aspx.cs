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
    public partial class CasosDeUso : System.Web.UI.Page
    {
        static string[] actores_existe = new string[8];
      static  int cuY = 70;
      static int cant_act = 0;
      static int actY = 70;     
      static int txtY = 80;
      static int desY = 80;

        protected void Page_Load(object sender, EventArgs e)
        {
        //  generar();
        }


        public void generar()
        {

            DataTable dt = admin.consultas("select * from secuencia as s inner join plantilla_req_funcional as pr on pr.referencia=s.rf where s.actor_ref='"+Session["plantilla"]+"'");



            string html = "<div id=\"pape\" class=\"pape\"></div>" + "<script type=\"text/javascript\"> " +
                        "var graph = new joint.dia.Graph();" +
                         "var paper = new joint.dia.Paper({" +
                         "  el: $('#pape')," +
                         "width: 1200," +
                         "  height: 800," +
                         "  gridSize: 1," +
                         "  model: graph" +
                         "});" +
                        " joint.shapes.basic.DecoratedRect = joint.shapes.basic.Generic.extend({" +
                       "markup: '<g class=\"rotatable\"><g class=\"scalable\"><rect/></g><image/><text/></g>'," +
                       "defaults: joint.util.deepSupplement({" +
                        "type: 'basic.DecoratedRect'," +
                       "size: { width: 100, height: 60 }," +
                       "attrs:{" + " 'text': { 'font-size': 14, text: '', 'ref-x': .5, 'ref-y': .5, ref: 'rect', 'y-alignment': '', 'x-alignment': 'middle', fill: 'black' }," +
                       "'image': { 'ref-x': 2, 'ref-y': 2, 'y-alignment':'middle','x-alignment': 'middle', ref: 'rect', width: 80, height: 120 } } }," +
                    "joint.shapes.basic.Generic.prototype.defaults) });" +
            "var decoratedRect = new joint.shapes.basic.DecoratedRect({" +
           " position: { x: 150, y: 80 }," +
            "size: { width: 100, height: 90 }," +
            "attrs:{" +
               " text: { text: 'My Element' }," +
               " image: { 'xlink:href': '" +HttpRuntime.AppDomainAppPath + @"Diagramas\actor.png" +"' }" +
           " }});" + "graph.addCell(decoratedRect);";
            string clas = string.Empty;
            int  y;
          
            y = 10;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                clas += "var " + dt.Rows[i]["nombre"] + "  = new joint.shapes.basic.Circle({"+
                   "position: { x:400  , y:"+i*20+" }," +
                    
                    " size: { width: 90, height: 50 }," +
                   " attrs: { text: { text: '"+dt.Rows[i]["nombre"]+"' }, circle: { fill: '#2ECC71' } }," +
                   "  name: '"+dt.Rows[i]["nombre"] + "'" +
                     "});" +
                   "graph.addCell("+ dt.Rows[i]["nombre"] + ");";
            }
         

            clas += "</script>";

            html += clas;
            sci.InnerHtml = html;
        }




        /*
        protected void dibujar()
        {
          DataTable dt = admin.consultas("select descripcion from secuencia s where s.rf='" + Session["plantilla"] + "' and s.proyecto='" + Session["proyecto"] + "'");


          for (int i = 0; i < dt.Rows.Count; i++)
          {
              string[] texto = dt.Rows[i]["descripcion"].ToString().Split(' ');

              casosdeuso(texto);

              for (int j = 0; j < 8; j++)
              {
                  if (actores_existe[j] == string.Empty) { actores_existe[j] = texto[1]; j = 8; }
                  else {
                      if(actores_existe[j].Equals(texto[1])){
                          j=8;
                  }
                     
                  }
                  

              }
          }

          actores();


            /*
               Sprite actor;
            string[] des = new string[2];
            des[0] = string.Empty;
           des[1] = string.Empty;
            int mi = Convert.ToInt16((texto.Length-2) / 2);

            Sprite caso = new Sprite
            {
                SpriteID = "caso",
                Type = SpriteType.Image,
                Src = "/Diagramas/Caso.png",
                Width = 180,
                Height = 70,
                X = 335,
                Y = 120

            };
            Draw1.Add(caso);
            caso.Show(true);

            Sprite linea = new Sprite
            {
                SpriteID = "linea",
                Type = SpriteType.Image,
                Src = "/Diagramas/line.png",
               Width=150, Height=1, X=184, Y=130

            };
           
            Draw1.Add(linea);
            linea.Show(true);
            Draw1.GetSprite("linea").SetAttributes(new SpriteAttributes { Rotate = new RotateAttribute { Degrees = 20 } }, true);
         


            for (int i = 2; i < texto.Length; i++)
            {

                if (i - 2 >= mi && mi>0)   des[1] += texto[i] +" ";                
                else     des[0] += texto[i] + " ";
            }

            Sprite[] sprite2 = new Sprite[2];

            for (int i = 0; i < des.Length; i++)
            {
                 sprite2[i] = new Sprite
                {
                    SpriteID = "sprite2"+i,
                    Type = SpriteType.Text,
                    Text = des[i],
                    X = 350,
                    Y = 150+(i*13),

                };
                Draw1.Add(sprite2[i]);
                sprite2[i].Show(true);
            }
            actor = new Sprite
            {
                SpriteID = "act",
                Type = SpriteType.Image,
                Src="/Diagramas/Actor.png",
                Width=40, 
                Height=70,                
                X = 150,
                Y = 70

            };

            Sprite sprite = new Sprite
            {
                SpriteID = texto[1],
                Type = SpriteType.Text,
                Text= texto[1],               
                X = 150,
                Y = 150
               
            };

          
            Draw1.Add(sprite);
            sprite.Show(true);
            
            Draw1.Add(actor);
            actor.Show(true);

           */


        //  }
        /*
        protected void casosdeuso(string [] texto)
        {
            string[] des = new string[2];
            int mi = Convert.ToInt16((texto.Length - 2) / 2);

            Sprite caso = new Sprite
            {
                SpriteID = "caso",
                Type = SpriteType.Image,
                Src = "~/Diagramas/Caso.png",
                Width = 220,
                Height = 70,
                X = 320,
                Y = cuY
            };
            Draw1.Add(caso);
            caso.Show(true);

            cuY+=80;


            for (int i = 2; i < texto.Length; i++)
            {

                if (i - 2 >= mi && mi > 0) des[1] += texto[i] + " ";
                else des[0] += texto[i] + " ";
            }


            Sprite[] sprite2 = new Sprite[2];

            for (int i = 0; i < des.Length; i++)
            {
                sprite2[i] = new Sprite
                {
                    SpriteID = "sprite2" + i,
                    Type = SpriteType.Text,
                    Text = des[i],
                    X = 350,
                    Y = desY +(i*13)+13,

                };
                Draw1.Add(sprite2[i]);
                sprite2[i].Show(true);
               
            }
            desY += 80;

        }
        
        protected void actores()
        {
           

            for(int i= 0;i <actores_existe.Length;i++)
            {

                if (actores_existe[i] != string.Empty)
                {

                    Sprite texto = new Sprite
                       {
                           SpriteID = "texto",
                           Type = SpriteType.Text,
                           Text = actores_existe[i],
                           X = 150,
                           Y = txtY+140,

                       };
                    Draw1.Add(texto);
                    texto.Show(true);

                    txtY += 120;

                    if (cant_act <= 4)
                    {
                        Sprite actor = new Sprite
                        {
                            SpriteID = "act",
                            Type = SpriteType.Image,
                            Src = "~/Diagramas/Actor.png",
                            Width = 40,
                            Height = 70,
                            X = 150,
                            Y = actY+70

                        };
                        Draw1.Add(actor);
                        actor.Show(true);


                    }
                    else
                    {
                        Sprite actor = new Sprite
                        {
                            SpriteID = "act",
                            Type = SpriteType.Image,
                            Src = "~/Diagramas/Actor.png",
                            Width = 40,
                            Height = 70,
                            X = 380,
                            Y = actY+70

                        };
                        Draw1.Add(actor);
                        actor.Show(true);
                    }

                    actY += 120;
                    cant_act++;
                }
                else i = actores_existe.Length;
           }
            
        }
        */

    }
}