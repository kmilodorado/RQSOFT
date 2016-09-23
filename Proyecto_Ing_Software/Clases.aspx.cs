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
    public partial class Clases : System.Web.UI.Page
    {
        public static int x = 50;
        public static int y = 60;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
              generar();
            }
           
                

        }

        public void generar()
        {
            string html = "<div id=\"paper\" class=\"paper\"></div>" + "<script type=\"text/javascript\"> " + "var graph = new joint.dia.Graph();" +
                         "var paper = new joint.dia.Paper({" +
                         "  el: $('#paper')," +
                         "width: 1200," +
                         "  height: 800," +
                         "  gridSize: 1," +
                         "  model: graph" +
                         "});" +
                         "var uml = joint.shapes.uml;";

            string clas = string.Empty;
            clas = "var classes = {";
            DataTable dt = admin.consultas("select ri.referencia, ri.nombre from plantilla_ri ri where ri.proyecto='" + Session["proyecto"] + "'");

            Random rd = new Random();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataTable dt2 = admin.consultas("select de.descripcion as des from datos_especificos de where de.proyecto='" + Session["proyecto"] + "' and de.ri='" + dt.Rows[i]["referencia"].ToString() + "'");
                clas += dt.Rows[i]["nombre"].ToString() + ":new uml.Class({" +
                             "position: { x:"+rd.Next(100,900)+ "  , y: " + rd.Next(100, 700) + " }," +
                             "size: { width: 220, height: 140 }," +
                             "name:" + "'" + dt.Rows[i]["nombre"].ToString() + "'" + "," +
                             "attributes: [";
                if (dt.Rows.Count==0)
                {
                    clas += "],";
                }
                else
                {
                for (int j = 0; j < dt2.Rows.Count; j++)
                {
                    if (dt2.Rows.Count == 1)
                    {
                        clas += "'" + dt2.Rows[j]["des"] + "'],";
                    }
                    else if (j == dt2.Rows.Count - 1)
                    {
                        clas += "'" + dt2.Rows[j]["des"] + "'],";
                    }
                    else
                    {
                        clas += "'" + dt2.Rows[j]["des"] + "' , ";
                    }
                }
                }
                DataTable met = admin.consultas("select ri.ri, ri.rf,rf.nombre,ri.proyecto_ri from ri_rf as ri inner join plantilla_req_funcional as rf on ri.rf=rf.referencia where ri.proyecto_rf=rf.proyecto  and ri.ri='" + dt.Rows[i]["referencia"].ToString() + "' and ri.proyecto_ri='" + Session["proyecto"] + "'");
                clas += "methods: [";
                if (met.Rows.Count == 0)
                {
                    clas += "],";
                }
                else
                {


                    for (int j = 0; j < met.Rows.Count; j++)
                    {
                        if (met.Rows.Count == 1)
                        {
                            clas += "'" + met.Rows[j]["nombre"] + "(): Void' ],";
                        }
                        else if (j == met.Rows.Count - 1)
                        {
                            clas += "'" + met.Rows[j]["nombre"] + "(): Void' ],";
                        }
                        else
                        {
                            clas += "'" + met.Rows[j]["nombre"] + "'(): Void', ";
                        }
                    }
                }

                clas += "attrs: { " +
                     "'.uml-class-name-rect': {" +
                     "fill: '#A69D8D'," +
                     "stroke: '#000'," +
                     "'stroke-width': 0.5," +
                     "}," +
                     "'.uml-class-attrs-rect, .uml-class-methods-rect': {" +
                     " fill: '#CCD3D9'," +
                     "stroke: '#000'," +
                     "'stroke-width': 0.5" +
                     "}," +
                     "'.uml-class-attrs-text': {" +
                    " ref: '.uml-class-attrs-rect'," +
                    "'ref-y': 0.5," +
                    "'y-alignment': 'middle'" +
                    "}," +
                     "'.uml-class-methods-text': {" +
                  "ref: '.uml-class-methods-rect'," +
                  "  'ref-y': 0.5," +
                  "'y-alignment': 'middle'" +
                    " }" +
                  " }" +
                 " }),";
                }
            

            clas +=" }; _.each(classes, function(c) { graph.addCell(c); });</script>";

            html += clas;
            scri.InnerHtml = html;
        }

        protected void dibujar()
        {
           DataTable dt = admin.consultas("select ri.referencia, ri.nombre from plantilla_ri ri where ri.proyecto='"+Session["proyecto"]+"'");

           // DataTable dt = admin.consultas("select ri.referencia, ri.nombre from plantilla_ri ri where ri.proyecto='19'");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                clases(dt.Rows[i]["referencia"].ToString(), dt.Rows[i]["nombre"].ToString(), i);
            }
        
        }
        protected void clases(string ri, string nom, int i)
        {
           

     

          DataTable dt = admin.consultas("select de.descripcion from datos_especificos de where de.proyecto='"+Session["proyecto"]+"' and de.ri='" + ri + "'");

          
               
        
        }
    }
}