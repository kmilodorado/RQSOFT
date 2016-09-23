using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;

using RqSoft.LogicaDeNegocio;
using RqSoft.Entidades;



namespace Proyecto_Ing_Software.ReportesPDF
{

    public class reporte 
    {
        public void crearpdf()
        {
            string file = "report1" + DateTime.Now.Minute.ToString() + ".js";
           //string file = "report1.pdf";
           string FilePath = HttpRuntime.AppDomainAppPath + @"Formatos\" + file;
            Document document = new Document();
            MemoryStream m = new MemoryStream();

            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(FilePath, FileMode.OpenOrCreate));

            PdfPTable table = new PdfPTable(3);

            PdfPCell cell;
            // we add a cell with colspan 3
            cell = new PdfPCell(new Phrase("Cell with colspan 3"));
          //  cell.AddElement(new Phrase("celda1"));
            cell.Colspan = 3;
            table.AddCell(cell);
            // now we add a cell with rowspan 2
            cell = new PdfPCell(new Phrase("Cell with rowspan 2"));
            cell.Rowspan = 2;
            table.AddCell(cell);
            // we add the four remaining cells with addCell()
            table.AddCell("row 1; cell 1");
            table.AddCell("row 1; cell 2");
            table.AddCell("row 2; cell 1");
            table.AddCell("row 2; cell 2");

            document.Open();

            document.Add(new Paragraph("Este es el primer pdf"));
            document.NewPage();
            document.Add(new Paragraph("pagina 2"));
            document.NewPage();
            document.Add(new Paragraph("pagina 3"));
            document.Add(table);
            document.Close();

            string ds = Global.path + "/Formatos/" + file;

         Ext.Net.X.Js.Call("window.open", ds, "_Blank" );

            //codigo para abrir una nueva ventana con EXT.NET
            // X.Js.Call("window.open","Actor.aspx","_Blank");        
        }

        public string documento(string nombre, string proyecto)
        {
            string file = nombre+ DateTime.Now.Minute.ToString()+DateTime.Now.Second.ToString() + ".pdf";
            //string file = "report1.pdf";
            string FilePath = HttpRuntime.AppDomainAppPath + @"\Formatos\" + file;

            //   Document document = new Document(PageSize.LETTER, 50, 50, 50, 50);
            Document document = new Document(PageSize.LETTER, 80, 80, 80, 80);
            MemoryStream m = new MemoryStream();       

            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(FilePath, FileMode.OpenOrCreate));
            DataTable portada = admin.consultas("SELECT * from proyecto where codigo='" + proyecto + "'");

            writer.PageEvent = new HeaderFooter(portada.Rows[0]["nombre"].ToString().ToUpper());
            //writer.PageEvent = new HeaderFooter();

            Paragraph texto = new Paragraph();     
            texto.Alignment =  Element.ALIGN_CENTER;
             
           
            texto.Font = FontFactory.GetFont("Verdana",12);
            texto.Add(portada.Rows[0]["nombre"].ToString().ToUpper()+"");

            Paragraph info = new Paragraph();
            info.Alignment = 1;
            info.Font = FontFactory.GetFont("Verdana", 12,Font.BOLD);

            //******* abrir documento
            document.Open();

            //********* inicio portada**************

            for (int i = 0; i < 25; i++)
            {
                if (i == 0) {
                    document.Add(texto);
                    texto.RemoveAt(0);
                }
                else if (i == 1)
                {
                    texto.Add("Documento de requerimientos de sistema".ToUpper());
                    texto.Alignment = 1;
                    document.Add(texto);
                    texto.RemoveAt(0);
                }
           
                else if (i == 11)
                {
                    portada = admin.consultas("SELECT CONCAT(u.nombres, ' ', u.apellidos) AS autor FROM usuario u "
                                    + " INNER JOIN autor_proyecto ap ON ap.usuario= u.id_usuario WHERE ap.proyecto='" + proyecto + "'");

                    int diferencia = portada.Rows.Count;
                    if (diferencia > 7 )
                    {
                        info.Add("Presentado Por:".ToUpper());
                        info.Alignment = 1;
                        document.Add(info);
                        document.Add(new Paragraph(" "));
                        info.RemoveAt(0);
                            for (int j = 0; j < portada.Rows.Count; j++)
                              {
                                 texto.Add(portada.Rows[j]["autor"].ToString().ToUpper());
                                 document.Add(texto);
                                 texto.RemoveAt(0);
                               }

                    }

                    else
                    {
                        for (int k = 0; k < (diferencia/2)-1; k++)
                        {
                            document.Add(new Paragraph(" "));
                            i++;
                        }
                        for (int j = 0; j < portada.Rows.Count; j++)
                        {

                            texto.Add(portada.Rows[j]["autor"].ToString().ToUpper());
                            document.Add(texto);
                            texto.RemoveAt(0);
                        }


                    }
                    
                }
                else if (i == 18)
                {
                    info.Add("Presentado Para:".ToUpper());
                    document.Add(new Paragraph(info));
                    info.RemoveAt(0);
                }
                else if (i == 19)
                {
                    portada = admin.consultas("SELECT CONCAT(f.nombre, ' ', f.apellido) AS fuente FROM  "
                                     + " fuente f INNER JOIN proyecto_fuente pf ON pf.id_fuente = f.id and pf.id_organizacion= f.organizacion WHERE pf.proyecto='" + proyecto + "'");

                    for (int k = 0; k < portada.Rows.Count; k++)
                    {
                        texto.Add(portada.Rows[k]["fuente"].ToString().ToUpper());
                        document.Add(texto);
                        texto.RemoveAt(0);

                    }
                }
                else if (i==24)
                {
                    info.Add(DateTime.Now.Year.ToString());
                    document.Add(info);
                }
                else
                {
                    document.Add(new Paragraph(" "));
                }
            }

            document.NewPage();
/*

            document.Add(texto);
            texto.RemoveAt(0);
            texto.Add("Documento de requerimientos de sistema".ToUpper());
            texto.Alignment = 1;
            document.Add(texto);
            document.Add(new Paragraph(" "));
            document.Add(new Paragraph(" "));
            document.Add(new Paragraph(" "));
            document.Add(new Paragraph(" "));
            document.Add(new Paragraph(" "));
            document.Add(new Paragraph(" "));
            document.Add(new Paragraph(" "));
            info.Add("Presentado Por:".ToUpper());
            document.Add(new Paragraph(info));
            document.Add(new Paragraph(" "));

           portada = admin.consultas("SELECT CONCAT(u.nombres, ' ', u.apellidos) AS autor FROM usuario u "
                                    + " INNER JOIN autor_proyecto ap ON ap.usuario= u.id_usuario WHERE ap.proyecto='" + proyecto + "'");

            for (int i = 0; i < portada.Rows.Count; i++)
            {
                texto.RemoveAt(0);
                texto.Add(portada.Rows[i]["autor"].ToString().ToUpper());
                document.Add(texto);
            }
            document.Add(new Paragraph(" "));
            document.Add(new Paragraph(" "));
            document.Add(new Paragraph(" ")); 
            document.Add(new Paragraph(" "));           
            document.Add(new Paragraph(" "));
            info.RemoveAt(0);
            info.Add("Presentado Para:".ToUpper());
            document.Add(new Paragraph(info));
            document.Add(new Paragraph(" "));


            portada = admin.consultas("SELECT CONCAT(f.nombre, ' ', f.apellido) AS fuente FROM  "
                                     +" fuente f INNER JOIN proyecto_fuente pf ON pf.id_fuente = f.id and pf.id_organizacion= f.organizacion WHERE pf.proyecto='"+proyecto+"'");

            for (int i = 0; i < portada.Rows.Count; i++)
            {
                texto.RemoveAt(0);
                texto.Add(portada.Rows[i]["fuente"].ToString().ToUpper());
                document.Add(texto);
            }

            texto.RemoveAt(0);
            info.RemoveAt(0);
            info.Add(DateTime.Now.Year.ToString());
            document.Add(new Paragraph(" "));
            document.Add(new Paragraph(" "));
            document.Add(new Paragraph(" "));
            document.Add(new Paragraph(" "));
            document.Add(new Paragraph(" "));         
            document.Add(new Paragraph(" "));
            document.Add(new Paragraph(" "));        
            document.Add(info);
            //********* fin portada**************
            */

            //********página de objetivos*********
            document.NewPage();
            info.RemoveAt(0);
            info.Add("OBJETIVOS DEL SISTEMA");
            document.Add(info);
            document.Add(new Paragraph(" "));
            document.Add(new Paragraph(" "));

            DataTable obj = admin.consultas("select referencia from plantilla_objetivo po where proyecto ='"+proyecto+"'");
            for (int i = 0; i < obj.Rows.Count; i++)
            {
                document.Add(t_objetivos(obj.Rows[i]["referencia"].ToString(), proyecto));
                document.Add(new Paragraph(" "));
                document.Add(new Paragraph(" "));  
            }


            //********fin página de objetivos*********



            //********página de actores*********
         

            document.NewPage();
            info.RemoveAt(0);
            info.Add("ACTORES DEL SISTEMA");
            document.Add(info);
            document.Add(new Paragraph(" "));
            document.Add(new Paragraph(" "));

            DataTable act = admin.consultas("select referencia from plantilla_actor po where proyecto ='" + proyecto + "'");
            for (int i = 0; i < act.Rows.Count; i++)
            {
                document.Add(t_actores(act.Rows[i]["referencia"].ToString(), proyecto));
                document.Add(new Paragraph(" "));
                document.Add(new Paragraph(" "));
            }

            //********fin página actores*********



            //********página de req. funcionales*********
         


            document.NewPage();
            info.RemoveAt(0);
            info.Add("REQUISITOS FUNCIONALES");
            document.Add(info);
            document.Add(new Paragraph(" "));
            document.Add(new Paragraph(" "));

            DataTable rf = admin.consultas("select referencia from plantilla_req_funcional po where proyecto ='" + proyecto + "'");
            for (int i = 0; i < rf.Rows.Count; i++)
            {
                document.Add(t_rf(rf.Rows[i]["referencia"].ToString(), proyecto));
                document.Add(new Paragraph(" "));
                document.Add(new Paragraph(" "));
            }

            //********fin página de req funcionales*********


            //********página de req. no funcionales*********
        


            document.NewPage();
            info.RemoveAt(0);
            info.Add("REQUISITOS NO FUNCIONALES");
            document.Add(info);
            document.Add(new Paragraph(" "));
            document.Add(new Paragraph(" "));

            DataTable rnf = admin.consultas("select referencia from plantilla_rnf where proyecto ='" + proyecto + "'");
            for (int i = 0; i < rnf.Rows.Count; i++)
            {
                document.Add(t_rnf(rnf.Rows[i]["referencia"].ToString(), proyecto));
                document.Add(new Paragraph(" "));
                document.Add(new Paragraph(" "));
            }

            //********fin página de req no funcionales*********



            //********página de req. información*********
         

            document.NewPage();
            info.RemoveAt(0);
            info.Add("REQUISITOS DE ALMACENAMIENTO DE INFORMACIÓN");
            document.Add(info);
            document.Add(new Paragraph(" "));
            document.Add(new Paragraph(" "));

            DataTable ri = admin.consultas("select referencia from plantilla_ri where proyecto ='" + proyecto + "'");
            for (int i = 0; i < ri.Rows.Count; i++)
            {
                document.Add(t_ri(ri.Rows[i]["referencia"].ToString(), proyecto));
                document.Add(new Paragraph(" "));
                document.Add(new Paragraph(" "));
            }

            //********fin página de req. información*********



        



            document.Close();

            //**********fin documento**************

            string abrir = Global.path + @"Formatos/"+ file;


            return abrir;

                  
        }

        
        public PdfPTable t_objetivos(string obj, string pro)
        {
            DataTable dt = admin.consultas("select * from plantilla_objetivo po where proyecto ='" + pro + "' and referencia='"+obj+"'");
        
            DataTable a = admin.consultas("SELECT CONCAT(u.nombres,' ',u.apellidos) AS nombre, u.organizacion "
                            +" FROM usuario u INNER JOIN autor_objetivo ao ON u.id_usuario= ao.usuario "
                            + " WHERE (ao.proyecto='" + pro + "' AND ao.proyecto_objetivo='" + pro + "' and ao.objetivo='" + obj + "')");

            DataTable f = admin.consultas("SELECT CONCAT(f.nombre,' ',f.apellido) AS nombre, f.organizacion "
                        +" FROM fuente f  INNER JOIN fuente_objetivo fo ON (f.id, f.organizacion)= (fo.fuente, fo.organizacion) "
                        + " WHERE fo.proyecto='" + pro + "' and fo.proyecto_objetivo='" + pro + "' and fo.objetivo='" + obj + "'");

            DataTable ob = admin.consultas("SELECT po.referencia, po.nombre FROM plantilla_objetivo po "
                    +" INNER JOIN objetivo_objetivo oo ON (po.proyecto, po.referencia)= (oo.proyecto, oo.objetivo) "
                    + " WHERE (oo.proyecto2='" + pro + "' and oo.objetivo2='" + obj + "')");


            string autores = string.Empty;
            string fuentes = string.Empty;
            string objetivos = string.Empty;


            for (int i = 0; i < a.Rows.Count; i++)
            {
                autores += a.Rows[i]["nombre"].ToString() + " (" + a.Rows[i]["organizacion"].ToString()+")"+"\n";
            }

            for (int i = 0; i < f.Rows.Count; i++)
            {
                fuentes += f.Rows[i]["nombre"].ToString() + " (" + f.Rows[i]["organizacion"].ToString() + ")" + "\n";
            }
            for (int i = 0; i < ob.Rows.Count; i++)
            {
                objetivos += ob.Rows[i]["referencia"].ToString() + " <" + ob.Rows[i]["nombre"].ToString() + ">" + "\n";
            }
            
            
            
            
            PdfPTable objetivo = new PdfPTable(3);

            Phrase frase;
            PdfPCell celda = new PdfPCell();
            celda.Rowspan = 1;
            celda.Colspan = 1;
            celda.VerticalAlignment = Element.ALIGN_MIDDLE;

            PdfPCell celda2 = new PdfPCell();
            celda2.Rowspan = 1;
            celda2.Colspan = 2;
            celda2.VerticalAlignment = Element.ALIGN_MIDDLE;


            //*****llenar plantilla********

                frase = new Phrase();
                frase.Font = FontFactory.GetFont("Verdana", 10);
                frase.Add(dt.Rows[0]["referencia"].ToString());
                celda.Phrase = frase;           
                objetivo.AddCell(celda);

                frase = new Phrase();
                frase.Font = FontFactory.GetFont("Verdana", 10);
                frase.Add(dt.Rows[0]["nombre"].ToString());
                celda2.Phrase = frase;              
                objetivo.AddCell(celda2);

                frase = new Phrase();
                frase.Font = FontFactory.GetFont("Verdana", 10);
                frase.Add("Versión");
                celda.Phrase = frase;
                objetivo.AddCell(celda);

                frase = new Phrase();
                frase.Font = FontFactory.GetFont("Verdana", 10);
                frase.Add(dt.Rows[0]["version"].ToString() + " (" +Convert.ToDateTime( dt.Rows[0]["fecha"].ToString()).ToString("yyyy-MM-dd")+")");
                celda2.Phrase = frase;
                objetivo.AddCell(celda2);

                frase = new Phrase();
                frase.Font = FontFactory.GetFont("Verdana", 10);
                frase.Add("Autores");
                celda.Phrase = frase;
                objetivo.AddCell(celda);

                frase = new Phrase();
                frase.Font = FontFactory.GetFont("Verdana", 10);
                frase.Add(autores);
                celda2.Phrase = frase;
                objetivo.AddCell(celda2);

                frase = new Phrase();
                frase.Font = FontFactory.GetFont("Verdana", 10);
                frase.Add("Fuentes");
                celda.Phrase = frase;
                objetivo.AddCell(celda);

                frase = new Phrase();
                frase.Font = FontFactory.GetFont("Verdana", 10);
                frase.Add(fuentes);
                celda2.Phrase = frase;
                objetivo.AddCell(celda2);

                frase = new Phrase();
                frase.Font = FontFactory.GetFont("Verdana", 10);
                frase.Add("Descripción");
                celda.Phrase = frase;
                objetivo.AddCell(celda);

                frase = new Phrase();
                frase.Font = FontFactory.GetFont("Verdana", 10);
                frase.Add(dt.Rows[0]["descripcion"].ToString());
                celda2.Phrase = frase;
                objetivo.AddCell(celda2);

                frase = new Phrase();
                frase.Font = FontFactory.GetFont("Verdana", 10);
                frase.Add("SubObjetivos");
                celda.Phrase = frase;
                objetivo.AddCell(celda);

                frase = new Phrase();
                frase.Font = FontFactory.GetFont("Verdana", 10);
                frase.Add(objetivos);
                celda2.Phrase = frase;
                objetivo.AddCell(celda2);

                frase = new Phrase();
                frase.Font = FontFactory.GetFont("Verdana", 10);
                frase.Add("Importancia");
                celda.Phrase = frase;
                objetivo.AddCell(celda);

                frase = new Phrase();
                frase.Font = FontFactory.GetFont("Verdana", 10);
                frase.Add(dt.Rows[0]["importancia"].ToString());
                celda2.Phrase = frase;
                objetivo.AddCell(celda2);

                frase = new Phrase();
                frase.Font = FontFactory.GetFont("Verdana", 10);
                frase.Add("Urgencia");
                celda.Phrase = frase;
                objetivo.AddCell(celda);

                frase = new Phrase();
                frase.Font = FontFactory.GetFont("Verdana", 10);
                frase.Add(dt.Rows[0]["urgencia"].ToString());
                celda2.Phrase = frase;
                objetivo.AddCell(celda2);

                frase = new Phrase();
                frase.Font = FontFactory.GetFont("Verdana", 10);
                frase.Add("Estado");
                celda.Phrase = frase;
                objetivo.AddCell(celda);

                frase = new Phrase();
                frase.Font = FontFactory.GetFont("Verdana", 10);
                frase.Add(dt.Rows[0]["estado"].ToString());
                celda2.Phrase = frase;
                objetivo.AddCell(celda2);

                frase = new Phrase();
                frase.Font = FontFactory.GetFont("Verdana", 10);
                frase.Add("Estabilidad");
                celda.Phrase = frase;
                objetivo.AddCell(celda);

                frase = new Phrase();
                frase.Font = FontFactory.GetFont("Verdana", 10);
                frase.Add(dt.Rows[0]["estabilidad"].ToString());
                celda2.Phrase = frase;
                objetivo.AddCell(celda2);

                frase = new Phrase();
                frase.Font = FontFactory.GetFont("Verdana", 10);
                frase.Add("Comentarios");
                celda.Phrase = frase;
                objetivo.AddCell(celda);

                frase = new Phrase();
                frase.Font = FontFactory.GetFont("Verdana", 10);
                frase.Add(dt.Rows[0]["comentarios"].ToString());
                celda2.Phrase = frase;
                objetivo.AddCell(celda2);

            //*****fin plantilla********
            
            objetivo.WidthPercentage=100;
            return objetivo;

        }
        public PdfPTable t_actores(string act, string pro)
        {
            DataTable dt = admin.consultas("select * from plantilla_actor po where proyecto ='" + pro + "' and referencia='" +act + "'");

            DataTable a = admin.consultas("SELECT CONCAT(u.nombres,' ',u.apellidos) AS nombre, u.organizacion "
                            +" FROM usuario u "
                            +" INNER JOIN autor_actor ao ON u.id_usuario= ao.usuario "
                            + " WHERE (ao.proyecto='" + pro + "' AND ao.proyecto_actor='" + pro + "' AND ao.actor='" + act + "')");

            DataTable f = admin.consultas("SELECT CONCAT(f.nombre,' ',f.apellido) AS nombre, f.organizacion "
                        + " FROM fuente f  INNER JOIN fuente_actor fo ON (f.id, f.organizacion)= (fo.fuente, fo.organizacion) "
                        + " WHERE fo.proyecto='" + pro + "' and fo.proyecto_actor='" + pro + "' and fo.actor='" + act + "'");


            string autores = string.Empty;
            string fuentes = string.Empty;
        


            for (int i = 0; i < a.Rows.Count; i++)
            {
                autores += a.Rows[i]["nombre"].ToString() + " (" + a.Rows[i]["organizacion"].ToString() + ")" + "\n";
            }

            for (int i = 0; i < f.Rows.Count; i++)
            {
                fuentes += f.Rows[i]["nombre"].ToString() + " (" + f.Rows[i]["organizacion"].ToString() + ")" + "\n";
            }
         




            PdfPTable objetivo = new PdfPTable(3);

            Phrase frase;
            PdfPCell celda = new PdfPCell();
            celda.Rowspan = 1;
            celda.Colspan = 1;
            celda.VerticalAlignment = Element.ALIGN_MIDDLE;

            PdfPCell celda2 = new PdfPCell();
            celda2.Rowspan = 1;
            celda2.Colspan = 2;
            celda2.VerticalAlignment = Element.ALIGN_MIDDLE;


            //*****llenar plantilla********

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add(dt.Rows[0]["referencia"].ToString());
            celda.Phrase = frase;
            objetivo.AddCell(celda);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add(dt.Rows[0]["nombre"].ToString());
            celda2.Phrase = frase;
            objetivo.AddCell(celda2);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add("Versión");
            celda.Phrase = frase;
            objetivo.AddCell(celda);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add(dt.Rows[0]["version"].ToString() + " (" + Convert.ToDateTime(dt.Rows[0]["fecha"].ToString()).ToString("yyyy-MM-dd") + ")");
            celda2.Phrase = frase;
            objetivo.AddCell(celda2);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add("Autores");
            celda.Phrase = frase;
            objetivo.AddCell(celda);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add(autores);
            celda2.Phrase = frase;
            objetivo.AddCell(celda2);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add("Fuentes");
            celda.Phrase = frase;
            objetivo.AddCell(celda);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add(fuentes);
            celda2.Phrase = frase;
            objetivo.AddCell(celda2);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add("Descripción");
            celda.Phrase = frase;
            objetivo.AddCell(celda);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add(dt.Rows[0]["descripcion"].ToString());
            celda2.Phrase = frase;
            objetivo.AddCell(celda2);


            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add("Comentarios");
            celda.Phrase = frase;
            objetivo.AddCell(celda);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add(dt.Rows[0]["comentarios"].ToString());
            celda2.Phrase = frase;
            objetivo.AddCell(celda2);

            //*****fin plantilla********
            objetivo.WidthPercentage = 100;
            return objetivo;

        }
        public PdfPTable t_rf(string rf, string pro)
        {
            DataTable dt = admin.consultas("select * from plantilla_req_funcional po where proyecto ='" + pro + "' and referencia='" + rf + "'");

            DataTable a = admin.consultas("SELECT CONCAT(u.nombres,' ',u.apellidos) AS nombre, u.organizacion "
                            + " FROM usuario u INNER JOIN autor_rf ao ON u.id_usuario= ao.usuario "
                            + " WHERE (ao.proyecto='" + pro + "' AND ao.proyecto_rf='" + pro + "' and ao.referencia='" +rf + "')");

            DataTable f = admin.consultas("SELECT CONCAT(f.nombre,' ',f.apellido) AS nombre, f.organizacion "
                        + " FROM fuente f  INNER JOIN fuente_rf fo ON (f.id, f.organizacion)= (fo.fuente, fo.organizacion) "
                        + " WHERE fo.proyecto='" + pro + "' and fo.proyecto_rf='" + pro + "' and fo.referencia='" + rf + "'");

            DataTable ob = admin.consultas("SELECT po.referencia, po.nombre FROM plantilla_objetivo po "
                    + " INNER JOIN objetivo_rf oo ON (po.proyecto, po.referencia)= (oo.proyecto, oo.objetivo) "
                    + " WHERE (oo.proyecto_rf='" + pro + "' and oo.referencia='" + rf + "')");

            DataTable req1 = admin.consultas("SELECT rf.referencia, rf.nombre "
                        +" FROM plantilla_req_funcional rf INNER JOIN rf_rf on (rf.proyecto, rf.referencia) = (rf_rf.proyecto_rf, rf_rf.rf) "
                        + " where rf_rf.proyecto_rf2='" + pro + "' and rf_rf.rf2='" + rf + "'");

            DataTable req2 = admin.consultas("SELECT rf.referencia, rf.nombre "
                    +" FROM plantilla_ri rf "
                    +" INNER JOIN ri_rf on (rf.proyecto, rf.referencia) = (ri_rf.proyecto_ri, ri_rf.ri) "
                    + " where ri_rf.proyecto_rf='" + pro + "' and ri_rf.rf='" + rf + "'");

            DataTable req3 = admin.consultas("SELECT rf.referencia, rf.nombre "
                        +" FROM plantilla_rnf rf "
                        +" INNER JOIN rnf_rf on (rf.proyecto, rf.referencia) = (rnf_rf.proyecto_rnf, rnf_rf.rnf) "
                        + " where rnf_rf.proyecto_rf='" + pro + "' and rnf_rf.rf='" + rf + "'");

            req1.Merge(req2);
            req1.Merge(req3);


            string autores = string.Empty;
            string fuentes = string.Empty;
            string objetivos = string.Empty;
            string requisitos = string.Empty;


            for (int i = 0; i < a.Rows.Count; i++)
            {
                autores += a.Rows[i]["nombre"].ToString() + " (" + a.Rows[i]["organizacion"].ToString() + ")" + "\n";
            }

            for (int i = 0; i < f.Rows.Count; i++)
            {
                fuentes += f.Rows[i]["nombre"].ToString() + " (" + f.Rows[i]["organizacion"].ToString() + ")" + "\n";
            }
            for (int i = 0; i < ob.Rows.Count; i++)
            {
                objetivos += ob.Rows[i]["referencia"].ToString() + " <" + ob.Rows[i]["nombre"].ToString() + ">" + "\n";
            }


            for (int i = 0; i < req1.Rows.Count; i++)
            {
                requisitos += req1.Rows[i]["referencia"].ToString() + " <" + req1.Rows[i]["nombre"].ToString() + ">" + "\n";
            }



           


            PdfPTable tabla = new PdfPTable(3);

            Phrase frase;
            PdfPCell celda = new PdfPCell();
            celda.Rowspan = 1;
            celda.Colspan = 1;
            celda.VerticalAlignment = Element.ALIGN_MIDDLE;

            PdfPCell celda2 = new PdfPCell();
            celda2.Rowspan = 1;
            celda2.Colspan = 2;
            celda2.VerticalAlignment = Element.ALIGN_MIDDLE;

            PdfPCell celda3 = new PdfPCell();
            celda3.Rowspan = 1;
            celda3.Colspan = 3;
            celda3.VerticalAlignment = Element.ALIGN_MIDDLE;

    

            //******************* tabla de secuencia normal
            DataTable sec = admin.consultas("select s.secuencia, s.descripcion from secuencia s where s.rf='"+rf+"' and s.proyecto='"+pro+"'");

            PdfPTable t_sec = new PdfPTable(4);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add("Paso");
            celda.Phrase = frase;
            celda.HorizontalAlignment = Element.ALIGN_MIDDLE;
            t_sec.AddCell(celda);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add("Acción");
            celda3.Phrase = frase;
            celda3.HorizontalAlignment = Element.ALIGN_MIDDLE;
            t_sec.AddCell(celda3);


            for (int i = 0; i < sec.Rows.Count; i++)
            {

                frase = new Phrase();
                frase.Font = FontFactory.GetFont("Verdana", 10);
                frase.Add(sec.Rows[i]["secuencia"].ToString());
                celda.Phrase = frase;
                t_sec.AddCell(celda);

                frase = new Phrase();
                frase.Font = FontFactory.GetFont("Verdana", 10);
                frase.Add(sec.Rows[i]["descripcion"].ToString());
                celda3.Phrase = frase;
                t_sec.AddCell(celda3);

            }
            //**************************



            //******************* tabla excepción
            DataTable exc = admin.consultas("SELECT s.secuencia, s.excepcion FROM secuencia s WHERE s.excepcion !='null' AND s.proyecto='"+pro+"' AND s.rf='" + rf + "'");

            PdfPTable t_exc = new PdfPTable(4);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add("Paso");
            celda.Phrase = frase;
            celda.HorizontalAlignment = Element.ALIGN_MIDDLE;
            t_exc.AddCell(celda);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add("Acción");
            celda3.Phrase = frase;
            celda3.HorizontalAlignment = Element.ALIGN_MIDDLE;
            t_exc.AddCell(celda3);


            for (int i = 0; i < exc.Rows.Count; i++)
            {

                frase = new Phrase();
                frase.Font = FontFactory.GetFont("Verdana", 10);
                frase.Add(exc.Rows[i]["secuencia"].ToString());
                celda.Phrase = frase;
                t_exc.AddCell(celda);

                frase = new Phrase();
                frase.Font = FontFactory.GetFont("Verdana", 10);
                frase.Add(exc.Rows[i]["excepcion"].ToString());
                celda3.Phrase = frase;
                t_exc.AddCell(celda3);

            }
            //**************************


            //******************* tabla rendimiento
            DataTable rend = admin.consultas("SELECT s.secuencia, s.rendimiento FROM secuencia s WHERE s.rendimiento !='null' AND s.proyecto='" + pro + "' AND s.rf='" + rf + "'");

            PdfPTable t_ren = new PdfPTable(4);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add("Paso");
            celda.Phrase = frase;
            celda.HorizontalAlignment = Element.ALIGN_MIDDLE;
            t_ren.AddCell(celda);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add("Cota de tiempo");
            celda3.Phrase = frase;
            celda3.HorizontalAlignment = Element.ALIGN_MIDDLE;
            t_ren.AddCell(celda3);


            for (int i = 0; i < rend.Rows.Count; i++)
            {

                frase = new Phrase();
                frase.Font = FontFactory.GetFont("Verdana", 10);
                frase.Add(rend.Rows[i]["secuencia"].ToString());
                celda.Phrase = frase;
                t_ren.AddCell(celda);

                frase = new Phrase();
                frase.Font = FontFactory.GetFont("Verdana", 10);
                frase.Add(rend.Rows[i]["rendimiento"].ToString());
                celda3.Phrase = frase;
                t_ren.AddCell(celda3);

            }
            //**************************



            celda.HorizontalAlignment = Element.ALIGN_LEFT;


            //*****llenar plantilla********

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add(dt.Rows[0]["referencia"].ToString());
            celda.Phrase = frase;
            tabla.AddCell(celda);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add(dt.Rows[0]["nombre"].ToString());
            celda2.Phrase = frase;
            tabla.AddCell(celda2);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add("Versión");
            celda.Phrase = frase;
            tabla.AddCell(celda);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add(dt.Rows[0]["version"].ToString() + " (" + Convert.ToDateTime(dt.Rows[0]["fecha"].ToString()).ToString("yyyy-MM-dd") + ")");
            celda2.Phrase = frase;
            tabla.AddCell(celda2);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add("Autores");
            celda.Phrase = frase;
            tabla.AddCell(celda);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add(autores);
            celda2.Phrase = frase;
            tabla.AddCell(celda2);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add("Fuentes");
            celda.Phrase = frase;
            tabla.AddCell(celda);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add(fuentes);
            celda2.Phrase = frase;
            tabla.AddCell(celda2);        

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add("Objetivos asociados");
            celda.Phrase = frase;
            tabla.AddCell(celda);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add(objetivos);
            celda2.Phrase = frase;
            tabla.AddCell(celda2);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add("Requisitos asociados");
            celda.Phrase = frase;
            tabla.AddCell(celda);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add(requisitos);
            celda2.Phrase = frase;
            tabla.AddCell(celda2);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add("Descripción");
            celda.Phrase = frase;
            tabla.AddCell(celda);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add(dt.Rows[0]["descripcion"].ToString());
            celda2.Phrase = frase;
            tabla.AddCell(celda2);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add("Precondición");
            celda.Phrase = frase;
            tabla.AddCell(celda);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add(dt.Rows[0]["precondicion"].ToString());
            celda2.Phrase = frase;
            tabla.AddCell(celda2);

            //*************** secuencia
            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add("Secuencia Normal");
            celda.Phrase = frase;
            tabla.AddCell(celda);
          
          
            celda2.AddElement(t_sec);
            tabla.AddCell(celda2);


            //***********************

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add("Postcondición");
            celda.Phrase = frase;
            tabla.AddCell(celda);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add(dt.Rows[0]["postcondicion"].ToString());
            celda2.Phrase = frase;
            tabla.AddCell(celda2);

            //*************** excepción
            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add("Excepción");
            celda.Phrase = frase;
            tabla.AddCell(celda);


            celda2.AddElement(t_exc);
            tabla.AddCell(celda2);


            //***********************

            celda2.Phrase = frase;


            //*************** rendimiento
            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add("Rendimiento");
            celda.Phrase = frase;
            tabla.AddCell(celda);


            celda2.AddElement(t_ren);
            tabla.AddCell(celda2);


            //***********************


            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add("Frecuencia Esperada");
            celda.Phrase = frase;
            tabla.AddCell(celda);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add(dt.Rows[0]["frecuencia"].ToString());
            celda2.Phrase = frase;
            tabla.AddCell(celda2);



            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add("Importancia");
            celda.Phrase = frase;
            tabla.AddCell(celda);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add(dt.Rows[0]["importancia"].ToString());
            celda2.Phrase = frase;
            tabla.AddCell(celda2);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add("Urgencia");
            celda.Phrase = frase;
            tabla.AddCell(celda);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add(dt.Rows[0]["urgencia"].ToString());
            celda2.Phrase = frase;
            tabla.AddCell(celda2);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add("Estado");
            celda.Phrase = frase;
            tabla.AddCell(celda);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add(dt.Rows[0]["estado"].ToString());
            celda2.Phrase = frase;
            tabla.AddCell(celda2);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add("Estabilidad");
            celda.Phrase = frase;
            tabla.AddCell(celda);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add(dt.Rows[0]["estabilidad"].ToString());
            celda2.Phrase = frase;
            tabla.AddCell(celda2);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add("Comentarios");
            celda.Phrase = frase;
            tabla.AddCell(celda);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add(dt.Rows[0]["comentarios"].ToString());
            celda2.Phrase = frase;
            tabla.AddCell(celda2);

            //*****fin plantilla********

            tabla.WidthPercentage = 100;
            return tabla;

        }
        public PdfPTable t_rnf(string rnf, string pro)
        {
            DataTable dt = admin.consultas("select * from plantilla_rnf po where proyecto ='" + pro + "' and referencia='" + rnf + "'");

            DataTable a = admin.consultas("SELECT CONCAT(u.nombres,' ',u.apellidos) AS nombre, u.organizacion "
                            + " FROM usuario u INNER JOIN autor_rnf ao ON u.id_usuario= ao.usuario "
                            + " WHERE (ao.proyecto='" + pro + "' AND ao.proyecto_rnf='" + pro + "' and ao.referencia='" + rnf + "')");

            DataTable f = admin.consultas("SELECT CONCAT(f.nombre,' ',f.apellido) AS nombre, f.organizacion "
                        + " FROM fuente f  INNER JOIN fuente_rnf fo ON (f.id, f.organizacion)= (fo.fuente, fo.organizacion) "
                        + " WHERE fo.proyecto='" + pro + "' and fo.proyecto_rnf='" + pro + "' and fo.referencia='" + rnf + "'");

            DataTable ob = admin.consultas("SELECT po.referencia, po.nombre FROM plantilla_objetivo po "
                    + " INNER JOIN objetivo_rf oo ON (po.proyecto, po.referencia)= (oo.proyecto, oo.objetivo) "
                    + " WHERE (oo.proyecto_rf='" + pro + "' and oo.referencia='" + rnf + "')");

            DataTable req1 = admin.consultas("SELECT rf.referencia, rf.nombre "
                        + " FROM plantilla_req_funcional rf INNER JOIN rf_rnf on (rf.proyecto, rf.referencia) = (rf_rnf.proyecto_rf, rf_rnf.rf) "
                        + " where rf_rnf.proyecto_rfn='" + pro + "' and rf_rnf.rnf='" + rnf + "'");

            DataTable req2 = admin.consultas("SELECT rf.referencia, rf.nombre "
                    + " FROM plantilla_ri rf "
                    + " INNER JOIN ri_rnf on (rf.proyecto, rf.referencia) = (ri_rnf.proyecto_ri, ri_rnf.ri) "
                    + " where ri_rnf.proyecto_rnf='" + pro + "' and ri_rnf.rnf='" + rnf + "'");

            DataTable req3 = admin.consultas("SELECT rf.referencia, rf.nombre "
                        + " FROM plantilla_rnf rf "
                        + " INNER JOIN rnf_rnf on (rf.proyecto, rf.referencia) = (rnf_rnf.proyecto, rnf_rnf.rnf) "
                        + " where rnf_rnf.proyecto2='" + pro + "' and rnf_rnf.rnf2='" + rnf + "'");

            req1.Merge(req2);
            req1.Merge(req3);


            string autores = string.Empty;
            string fuentes = string.Empty;
            string objetivos = string.Empty;
            string requisitos = string.Empty;


            for (int i = 0; i < a.Rows.Count; i++)
            {
                autores += a.Rows[i]["nombre"].ToString() + " (" + a.Rows[i]["organizacion"].ToString() + ")" + "\n";
            }

            for (int i = 0; i < f.Rows.Count; i++)
            {
                fuentes += f.Rows[i]["nombre"].ToString() + " (" + f.Rows[i]["organizacion"].ToString() + ")" + "\n";
            }
            for (int i = 0; i < ob.Rows.Count; i++)
            {
                objetivos += ob.Rows[i]["referencia"].ToString() + " <" + ob.Rows[i]["nombre"].ToString() + ">" + "\n";
            }


            for (int i = 0; i < req1.Rows.Count; i++)
            {
                requisitos += req1.Rows[i]["referencia"].ToString() + " <" + req1.Rows[i]["nombre"].ToString() + ">" + "\n";
            }






            PdfPTable tabla = new PdfPTable(3);

            Phrase frase;
            PdfPCell celda = new PdfPCell();
            celda.Rowspan = 1;
            celda.Colspan = 1;
            celda.VerticalAlignment = Element.ALIGN_MIDDLE;

            PdfPCell celda2 = new PdfPCell();
            celda2.Rowspan = 1;
            celda2.Colspan = 2;
            celda2.VerticalAlignment = Element.ALIGN_MIDDLE;

            PdfPCell celda3 = new PdfPCell();
            celda3.Rowspan = 1;
            celda3.Colspan = 3;
            celda3.VerticalAlignment = Element.ALIGN_MIDDLE;



            //*****llenar plantilla********

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add(dt.Rows[0]["referencia"].ToString());
            celda.Phrase = frase;
            tabla.AddCell(celda);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add(dt.Rows[0]["nombre"].ToString());
            celda2.Phrase = frase;
            tabla.AddCell(celda2);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add("Versión");
            celda.Phrase = frase;
            tabla.AddCell(celda);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add(dt.Rows[0]["version"].ToString() + " (" + Convert.ToDateTime(dt.Rows[0]["fecha"].ToString()).ToString("yyyy-MM-dd") + ")");
            celda2.Phrase = frase;
            tabla.AddCell(celda2);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add("Autores");
            celda.Phrase = frase;
            tabla.AddCell(celda);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add(autores);
            celda2.Phrase = frase;
            tabla.AddCell(celda2);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add("Fuentes");
            celda.Phrase = frase;
            tabla.AddCell(celda);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add(fuentes);
            celda2.Phrase = frase;
            tabla.AddCell(celda2);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add("Objetivos asociados");
            celda.Phrase = frase;
            tabla.AddCell(celda);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add(objetivos);
            celda2.Phrase = frase;
            tabla.AddCell(celda2);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add("Requisitos asociados");
            celda.Phrase = frase;
            tabla.AddCell(celda);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add(requisitos);
            celda2.Phrase = frase;
            tabla.AddCell(celda2);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add("Descripción");
            celda.Phrase = frase;
            tabla.AddCell(celda);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add(dt.Rows[0]["descripcion"].ToString());
            celda2.Phrase = frase;
            tabla.AddCell(celda2);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add("Importancia");
            celda.Phrase = frase;
            tabla.AddCell(celda);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add(dt.Rows[0]["importancia"].ToString());
            celda2.Phrase = frase;
            tabla.AddCell(celda2);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add("Urgencia");
            celda.Phrase = frase;
            tabla.AddCell(celda);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add(dt.Rows[0]["urgencia"].ToString());
            celda2.Phrase = frase;
            tabla.AddCell(celda2);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add("Estado");
            celda.Phrase = frase;
            tabla.AddCell(celda);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add(dt.Rows[0]["estado"].ToString());
            celda2.Phrase = frase;
            tabla.AddCell(celda2);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add("Estabilidad");
            celda.Phrase = frase;
            tabla.AddCell(celda);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add(dt.Rows[0]["estabilidad"].ToString());
            celda2.Phrase = frase;
            tabla.AddCell(celda2);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add("Comentarios");
            celda.Phrase = frase;
            tabla.AddCell(celda);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add(dt.Rows[0]["comentarios"].ToString());
            celda2.Phrase = frase;
            tabla.AddCell(celda2);

            //*****fin plantilla********

            tabla.WidthPercentage = 100;
            return tabla;

        }
        public PdfPTable t_ri(string ri, string pro)
        {
            DataTable dt = admin.consultas("select * from plantilla_ri po where proyecto ='" + pro + "' and referencia='" + ri + "'");

            DataTable a = admin.consultas("SELECT CONCAT(u.nombres,' ',u.apellidos) AS nombre, u.organizacion "
                            + " FROM usuario u INNER JOIN autor_ri ao ON u.id_usuario= ao.usuario "
                            + " WHERE (ao.proyecto='" + pro + "' AND ao.proyecto_ri='" + pro + "' and ao.referencia='" + ri + "')");

            DataTable f = admin.consultas("SELECT CONCAT(f.nombre,' ',f.apellido) AS nombre, f.organizacion "
                        + " FROM fuente f  INNER JOIN fuente_ri fo ON (f.id, f.organizacion)= (fo.fuente, fo.organizacion) "
                        + " WHERE fo.proyecto='" + pro + "' and fo.proyecto_ri ='" + pro + "' and fo.referencia='" + ri + "'");

            DataTable ob = admin.consultas("SELECT po.referencia, po.nombre FROM plantilla_objetivo po "
                    + " INNER JOIN objetivo_ri oo ON (po.proyecto, po.referencia)= (oo.proyecto, oo.objetivo) "
                    + " WHERE (oo.proyecto_ri='" + pro + "' and oo.referencia='" + ri + "')");

            DataTable req1 = admin.consultas("SELECT rf.referencia, rf.nombre "
                        + " FROM plantilla_req_funcional rf INNER JOIN rf_ri on (rf.proyecto, rf.referencia) = (rf_ri.proyecto_rf, rf_ri.rf) "
                        + " where rf_ri.proyecto_ri='" + pro + "' and rf_ri.ri='" + ri + "'");

            DataTable req2 = admin.consultas("SELECT rf.referencia, rf.nombre "
                    + " FROM plantilla_ri rf "
                    + " INNER JOIN ri_ri on (rf.proyecto, rf.referencia) = (ri_ri.proyecto, ri_ri.ri) "
                    + " where ri_ri.proyecto2='" + pro + "' and ri_ri.ri2='" + ri + "'");

            DataTable req3 = admin.consultas("SELECT rf.referencia, rf.nombre "
                        + " FROM plantilla_rnf rf "
                        + " INNER JOIN rnf_ri on (rf.proyecto, rf.referencia) = (rnf_ri.proyecto_rnf, rnf_ri.rnf) "
                        + " where rnf_ri.proyecto_ri='" + pro + "' and rnf_ri.ri='" + ri + "'");

            req1.Merge(req2);
            req1.Merge(req3);


            string autores = string.Empty;
            string fuentes = string.Empty;
            string objetivos = string.Empty;
            string requisitos = string.Empty;
            string datos = string.Empty;


            for (int i = 0; i < a.Rows.Count; i++)
            {
                autores += a.Rows[i]["nombre"].ToString() + " (" + a.Rows[i]["organizacion"].ToString() + ")" + "\n";
            }

            for (int i = 0; i < f.Rows.Count; i++)
            {
                fuentes += f.Rows[i]["nombre"].ToString() + " (" + f.Rows[i]["organizacion"].ToString() + ")" + "\n";
            }
            for (int i = 0; i < ob.Rows.Count; i++)
            {
                objetivos += ob.Rows[i]["referencia"].ToString() + " <" + ob.Rows[i]["nombre"].ToString() + ">" + "\n";
            }


            for (int i = 0; i < req1.Rows.Count; i++)
            {
                requisitos += req1.Rows[i]["referencia"].ToString() + " <" + req1.Rows[i]["nombre"].ToString() + ">" + "\n";
            }






            PdfPTable tabla = new PdfPTable(3);

            Phrase frase;
            PdfPCell celda = new PdfPCell();
            celda.Rowspan = 1;
            celda.Colspan = 1;
            celda.VerticalAlignment = Element.ALIGN_MIDDLE;

            PdfPCell celda2 = new PdfPCell();
            celda2.Rowspan = 1;
            celda2.Colspan = 2;
            celda2.VerticalAlignment = Element.ALIGN_MIDDLE;

           



            //******************* datos
            DataTable dat = admin.consultas("select d.descripcion from datos_especificos d where d.proyecto='"+pro+"' and d.ri='"+ri+"'");

            for (int i = 0; i < dat.Rows.Count; i++)
            {
                datos += "- "+dat.Rows[i]["descripcion"].ToString() + "\n";
            }
           

           


            //*****llenar plantilla********

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add(dt.Rows[0]["referencia"].ToString());
            celda.Phrase = frase;
            tabla.AddCell(celda);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add(dt.Rows[0]["nombre"].ToString());
            celda2.Phrase = frase;
            tabla.AddCell(celda2);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add("Versión");
            celda.Phrase = frase;
            tabla.AddCell(celda);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add(dt.Rows[0]["version"].ToString() + " (" + Convert.ToDateTime(dt.Rows[0]["fecha"].ToString()).ToString("yyyy-MM-dd") + ")");
            celda2.Phrase = frase;
            tabla.AddCell(celda2);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add("Autores");
            celda.Phrase = frase;
            tabla.AddCell(celda);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add(autores);
            celda2.Phrase = frase;
            tabla.AddCell(celda2);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add("Fuentes");
            celda.Phrase = frase;
            tabla.AddCell(celda);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add(fuentes);
            celda2.Phrase = frase;
            tabla.AddCell(celda2);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add("Objetivos asociados");
            celda.Phrase = frase;
            tabla.AddCell(celda);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add(objetivos);
            celda2.Phrase = frase;
            tabla.AddCell(celda2);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add("Requisitos asociados");
            celda.Phrase = frase;
            tabla.AddCell(celda);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add(requisitos);
            celda2.Phrase = frase;
            tabla.AddCell(celda2);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add("Descripción");
            celda.Phrase = frase;
            tabla.AddCell(celda);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add(dt.Rows[0]["descripcion"].ToString());
            celda2.Phrase = frase;
            tabla.AddCell(celda2);


            //*************** datos
            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add("Datos Específicos");
            celda.Phrase = frase;
            tabla.AddCell(celda);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add(datos);
            celda2.Phrase = frase;
            tabla.AddCell(celda2);


            //***********************

           

         

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add("Intervalo Temporal");
            celda.Phrase = frase;
            tabla.AddCell(celda);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add(dt.Rows[0]["intervalo_temporal"].ToString());
            celda2.Phrase = frase;
            tabla.AddCell(celda2);



            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add("Importancia");
            celda.Phrase = frase;
            tabla.AddCell(celda);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add(dt.Rows[0]["importancia"].ToString());
            celda2.Phrase = frase;
            tabla.AddCell(celda2);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add("Urgencia");
            celda.Phrase = frase;
            tabla.AddCell(celda);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add(dt.Rows[0]["urgencia"].ToString());
            celda2.Phrase = frase;
            tabla.AddCell(celda2);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add("Estado");
            celda.Phrase = frase;
            tabla.AddCell(celda);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add(dt.Rows[0]["estado"].ToString());
            celda2.Phrase = frase;
            tabla.AddCell(celda2);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add("Estabilidad");
            celda.Phrase = frase;
            tabla.AddCell(celda);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add(dt.Rows[0]["estabilidad"].ToString());
            celda2.Phrase = frase;
            tabla.AddCell(celda2);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add("Comentarios");
            celda.Phrase = frase;
            tabla.AddCell(celda);

            frase = new Phrase();
            frase.Font = FontFactory.GetFont("Verdana", 10);
            frase.Add(dt.Rows[0]["comentarios"].ToString());
            celda2.Phrase = frase;
            tabla.AddCell(celda2);

            //*****fin plantilla********

            tabla.WidthPercentage = 100;
            return tabla;

        }
    }


    class HeaderFooter : IPdfPageEvent


    {
        PdfContentByte pdfContent;
        private string v;

        public HeaderFooter(string v)
        {
            this.v = v;
        }

        public void OnChapter(PdfWriter writer, Document document, float paragraphPosition, Paragraph title)
        {
        }

        public void OnChapterEnd(PdfWriter writer, Document document, float paragraphPosition)
        {
        }

        public void OnCloseDocument(PdfWriter writer, Document document)
        {
        }

        public void OnEndPage(PdfWriter writer, Document document)
        {
            //We are going to add two strings in header. Create separate Phrase object with font setting and string to be included
            Phrase p1Header = new Phrase("", FontFactory.GetFont("verdana", 8));
            Phrase p2Header = new Phrase(v, FontFactory.GetFont("verdana", 8));
            //create iTextSharp.text Image object using local image path
            iTextSharp.text.Image imgPDF = iTextSharp.text.Image.GetInstance(HttpRuntime.AppDomainAppPath + @"\Styles\img\LogoRQSOFT.png");
            //imgPDF.Width = 10;
            imgPDF.ScaleAbsolute(68,25);
            //Create PdfTable object
            PdfPTable pdfTab = new PdfPTable(3);
            //We will have to create separate cells to include image logo and 2 separate strings
            PdfPCell pdfCell1 = new PdfPCell(imgPDF);
            PdfPCell pdfCell2 = new PdfPCell(p1Header);
            PdfPCell pdfCell3 = new PdfPCell(p2Header);
            //set the alignment of all three cells and set border to 0
            pdfCell1.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCell2.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCell3.HorizontalAlignment = Element.ALIGN_RIGHT;
            pdfCell1.VerticalAlignment = Element.ALIGN_CENTER;
            pdfCell2.VerticalAlignment = Element.ALIGN_BOTTOM ;

            pdfCell3.VerticalAlignment = Element.ALIGN_CENTER;

            pdfCell1.Border = 0;
            pdfCell2.Border = 0;
            pdfCell3.Border = 0;
            //add all three cells into PdfTable
            pdfTab.AddCell(pdfCell1);
            pdfTab.AddCell(pdfCell2);
            pdfTab.AddCell(pdfCell3);
            pdfTab.TotalWidth = document.PageSize.Width - 20;
            //call WriteSelectedRows of PdfTable. This writes rows from PdfWriter in PdfTable
            //first param is start row. -1 indicates there is no end row and all the rows to be included to write
            //Third and fourth param is x and y position to start writing
            pdfTab.WriteSelectedRows(0, -1, 10, document.PageSize.Height - 15, writer.DirectContent);
            //set pdfContent value
            pdfContent = writer.DirectContent;
            //Move the pointer and draw line to separate header section from rest of page
            pdfContent.MoveTo(30, document.PageSize.Height - 40);
            pdfContent.LineTo(document.PageSize.Width - 20, document.PageSize.Height - 40);



            //We are going to add two strings in header. Create separate Phrase object with font setting and string to be included
            Phrase pie = new Phrase("Documento generado por RQSOFT", FontFactory.GetFont("verdana", 8));
            Phrase pie2 = new Phrase("Pagina  " +writer.PageNumber, FontFactory.GetFont("verdana", 8));
            //create iTextSharp.text Image object using local image path
            //imgPDF.Width = 10;
            //Create PdfTable object
            PdfPTable pdffo = new PdfPTable(2);
            //We will have to create separate cells to include image logo and 2 separate strings
            PdfPCell pdfCel2 = new PdfPCell(pie);
            PdfPCell pdfCel3 = new PdfPCell(pie2);
            //set the alignment of all three cells and set border to 0
            pdfCel2.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCel3.HorizontalAlignment = Element.ALIGN_RIGHT;
            pdfCel2.VerticalAlignment = Element.ALIGN_BOTTOM;

            pdfCel3.VerticalAlignment = Element.ALIGN_CENTER;

            pdfCel2.Border = 0;
            pdfCel3.Border = 0;
            //add all three cells into PdfTable
            pdffo.AddCell(pdfCel2);
            pdffo.AddCell(pdfCel3);
            pdffo.TotalWidth = document.PageSize.Width - 20;
            //call WriteSelectedRows of PdfTable. This writes rows from PdfWriter in PdfTable
            //first param is start row. -1 indicates there is no end row and all the rows to be included to write
            //Third and fourth param is x and y position to start writing
            pdffo.WriteSelectedRows(0, -1, 10, (document.PageSize.Height-document.PageSize.Height) +30, writer.DirectContent);
            //set pdfContent value
            pdfContent = writer.DirectContent;
            //Move the pointer and draw line to separate header section from rest of page
         //   pdfContent.MoveTo(30, document.PageSize.Height - 35);
           // pdfContent.LineTo(document.PageSize.Width - 20, document.PageSize.Height - 35);






            pdfContent.Stroke();

        }

        public void OnGenericTag(PdfWriter writer, Document document, Rectangle rect, string text)
        {
        }

        public void OnOpenDocument(PdfWriter writer, Document document)
        {
        }

        public void OnParagraph(PdfWriter writer, Document document, float paragraphPosition)
        {
        }

        public void OnParagraphEnd(PdfWriter writer, Document document, float paragraphPosition)
        {
        }

        public void OnSection(PdfWriter writer, Document document, float paragraphPosition, int depth, Paragraph title)
        {
        }

        public void OnSectionEnd(PdfWriter writer, Document document, float paragraphPosition)
        {
        }

        public void OnStartPage(PdfWriter writer, Document document)
        {
        }
    }

}
 
