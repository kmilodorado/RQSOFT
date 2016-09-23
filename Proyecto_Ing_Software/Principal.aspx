<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Principal.aspx.cs" Inherits="Proyecto_Ing_Software.Principal" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>



<script runat="server">
    
    protected void ProyectoNew(object sender, DirectEventArgs e)
    {
        Session["accion"] = "crear";
        
        Window win = new Window
        {
            ID = "Window1",
            Title = "NUEVO PROYECTO",
            Height = 520,
            Width = 808,
            Resizable = false,
            AutoScroll=true,
            BodyPadding = 1,
            Modal = true,            
            CloseAction = CloseAction.Destroy,
            Loader = new ComponentLoader
            {
                Url = "ProyectoNuevo.aspx",
                Mode = LoadMode.Frame,
                LoadMask =
                {
                    ShowMask = true
                }
            }
        };

        win.Render(this.Form);
        
    }
    protected void AddElemento(object sender, DirectEventArgs e)
    {

        if (Session["proyecto"].ToString() != string.Empty)
        {

            string value = e.ExtraParams["Item"].ToString();

            switch (value)
            {
                case "Objetivos Del Sistema":
                    this.FormObjetivo();
                    break;

                case "Almacenamiento De Información":
                    this.FormAI();
                    break;
                case "Actores":
                    this.FormActores();
                    break;

                case "Requisitos Funcionales":
                    this.FormRF();
                    break;
                case "Requisitos No Funcionales":
                    this.FormRNF();
                    break;
            }

            Session["accion"] = "crear";
            
        }
        else
        {
            Notification.Show(new NotificationConfig
            {
                Title = "Error",
                Icon = Icon.Information,
                Html = "Debes seleccionar un proyecto antes de agregar una plantilla nueva",
                HideDelay = 4000,
            });
                
        }
    }
    protected void FormObjetivo()
    {
        Window win = new Window
        {
            ID = "Window1",
            Title = "PLANTILLA DE OBJETIVOS DEL SISTEMA",
            Height = 620,
            Width = 530,
            Resizable = false,
            BodyPadding = 5,
            Modal = true,
            CloseAction = CloseAction.Destroy,          
            Loader = new ComponentLoader
            {
                Url = "Objetivo.aspx",
                Mode = LoadMode.Frame,
                LoadMask =
                {
                    ShowMask = true
                }
            }
        };

        win.Render(this.Form);
        
    }
    protected void FormAI()
    {
        Window win = new Window
        {
            ID = "Window1",
            Title = "PLANTILLA REQUISITO DE ALMACENAMIENTO DE INFORMACIÓN",
            Height = 620,
            Width = 500,
            Resizable = false,
            BodyPadding = 5,
            Modal = true,
            CloseAction = CloseAction.Destroy,           
            Loader = new ComponentLoader
            {
                Url = "Informacion.aspx",
                Mode = LoadMode.Frame,
                LoadMask =
                {
                    ShowMask = true
                }
            }
        };

        win.Render(this.Form);

    }
    protected void FormActores()
    {
        Window win = new Window
        {
            ID = "Window1",
            Title = "PLANTILLA DE ACTORES",
            Height = 600,
            Width = 500,
            BodyPadding = 5,
            Resizable= false,
            Modal = true,
            CloseAction = CloseAction.Destroy,
            Loader = new ComponentLoader
            {
                Url = "Actor.aspx",
                Mode = LoadMode.Frame,
                LoadMask =
                {
                    ShowMask = true
                }
            }
        };

        win.Render(this.Form);

    }
    protected void FormRF()
    {
        Window win = new Window
        {
            ID = "Window1",
            Title = "PLANTILLA REQUISITO FUNCIONALES",
            Height = 580,
            Width = 530,
            BodyPadding = 5,
            Resizable=false,
            Modal = true,
            CloseAction = CloseAction.Destroy,
            Loader = new ComponentLoader
            {
                Url = "RequisitoFuncional.aspx",
                Mode = LoadMode.Frame,
                LoadMask =
                {
                    ShowMask = true
                }
            }
        };

        win.Render(this.Form);

    }
    protected void FormRNF()
    {
        Window win = new Window
        {
            ID = "Window1",
            Title = "PLANTILLA REQUISITO NO FUNCIONALES",
            Height = 580,
            Width = 510,
            BodyPadding = 5,
            Resizable=false,
            Modal = true,
            CloseAction = CloseAction.Destroy,
            Loader = new ComponentLoader
            {
                Url = "RequisitoNoFuncional.aspx",
                Mode = LoadMode.Frame,
                LoadMask =
                {
                    ShowMask = true
                }
            }
        };

        win.Render(this.Form);

    }
</script>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="Scripts/AbrirPdf.js"></script>
 
   <style type="text/css">      
	    #unlicensed{
		    display: none !important;
	    }
    </style>
          <link href="Styles/css/bootstrap.css" rel="stylesheet">
          <link href="Styles/css/style.css" rel="stylesheet">

      <script src="Scripts/js/jquery.min.js"></script>
    
     
    <script>
        var showMenu = function (view, node, item, index, e) {

            var sd = node.get("text");
            var sd2 = node.get("id");
        //    Ext.Msg.notify("Button Click", sd2);

            //if (node.get("text") != "Diagramas" && node.get("text") != "Plantillas" && node.get("text") != "Clases" 
            //&& node.get("text") != "Casos de uso" && node.get("text") != "Secuencia") 

         //   if (node.get("text") == "Objetivos Del Sistema" || node.get("text") == "Actores" || node.get("text") == "Almacenamiento De Información"
            //|| node.get("text") == "Requisitos Funcionales" || node.get("text") != "Requisitos No Funcionales")

            if (sd == "Objetivos Del Sistema" || sd == "Actores" || sd == "Almacenamiento De Información"
      || sd == "Requisitos Funcionales" || sd == "Requisitos No Funcionales") {

                var menu = App.TreeContextMenu;

                this.menuNode = node;
                menu.nodeName = node.get("text");
                view.getSelectionModel().select(node);

                menu.showAt([e.getXY()[0], e.getXY()[1] + 10]);
                e.stopEvent();
            }
            else {

                if (sd != "Diagramas" && sd != "Plantillas" && sd != "Clases"
                    && sd != "Casos de uso" && sd != "Secuencia") {
                 //   Ext.Msg.notify("Button Click", sd);
                    var menu2 = App.Menu2;

                    this.menuNode = node;
                    menu2.nodeName = node.get("id");
                    view.getSelectionModel().select(node);

                    menu2.showAt([e.getXY()[0], e.getXY()[1] + 10]);
                    e.stopEvent();
                }
            }
      };    
    </script>

</head>
<body>

    <header class="header black-bg">
     
            <a href="index.html" class="logo" ><b><img src="Styles/img/LogoRQSoft.png" height="40" width="100" /></b></a>
            <div class="top-menu">
            	<ul class="nav pull-right top-menu">
                    <li>
                          <ext:Label runat="server" ID="lb_usuario"  Cls="nombre">
              </ext:Label>
                    </li>
                     <li><a class="logout" href="">
                         <ext:LinkButton ID="Button4" runat="server"  Text="Salir" Html="<font color=#FFfFFF></font>" >
                         <DirectEvents>
                             <Click OnEvent="salir">
                                 <EventMask ShowMask="true" Msg="Cerrando Sesión..." MinDelay="2000" />
                             </Click>
                         </DirectEvents>
                      </ext:LinkButton>
                        </a></li>
            	</ul>
            </div>
        </header>

      <div >
      
    <form id="form1" runat="server">
        <div>
        <ext:ResourceManager ID="ResourceManager1" runat="server" />
         <ext:Menu ID="TreeContextMenu" runat="server">
            <Items>
                <ext:MenuItem ID="MenuItem1" runat="server" Text="Nueva Plantilla" Icon="Add">
                    <DirectEvents>
                        <Click OnEvent="AddElemento">
                            <EventMask ShowMask="true" />
                            <ExtraParams>
                                <ext:Parameter Name="Item" Value="#{TreeContextMenu}.nodeName" Mode="Raw"/>
                            </ExtraParams>
                        </Click>
                    </DirectEvents>
                </ext:MenuItem>
            </Items>          
        </ext:Menu>


         <ext:Menu ID="Menu2" runat="server">
            <Items>
                <ext:MenuItem ID="MenuItem3" runat="server" Text="Ver Plantilla" Icon="Zoom">
                    <DirectEvents>
                        <Click OnEvent="detalle">
                            <EventMask ShowMask="true" />
                            <ExtraParams>
                                <ext:Parameter Name="Item" Value="#{Menu2}.nodeName" Mode="Raw"/>
                            </ExtraParams>
                        </Click>
                    </DirectEvents>
                </ext:MenuItem>
            </Items>          
        </ext:Menu>
        

        <ext:Viewport ID="Viewport1" runat="server" StyleSpec="background-color: transparent;"
            Layout="BorderLayout" Cls="panel1">          
            <Items>
                <ext:Panel ID="pnlNorth" runat="server" Region="North" Height="50" Border="false"
                    Header="false" BodyStyle="background-color: transparent;">
                    <Content>
                        <div id="settingsWrapper">
                  <div id="settings">
                      
                  </div>
              </div>
          </Content>         

      </ext:Panel>
          <ext:Panel runat="server" Region="West" Title="PROYECTO" Width="100"  ID="Panel01"
              BodyStyle="background-color: #284051;" Collapsible="true" Split="true" MinWidth="300"
              MaxWidth="400" MarginsSummary="31 0 5 5" CMarginsSummary="31 5 5 5">

           <Items>
               <ext:Panel ID="pn_proyecto" runat="server" Width="300" BodyStyle="background-color: #284051;"
                   Border="true" Height="36" >
                   <TopBar>
                       <ext:Toolbar ID="Toolbar1" runat="server" Layout="HBoxLayout">
                           <Items>
                               <ext:ComboBox ID="cbx_proyecto" runat="server" Margins="5 5 5 5" Flex="2" Editable="false"
                                    DisplayField="nombre" ValueField="codigo" ForceSelection="true">
                                   <Store>
                                       <ext:Store runat="server" ID="store_proyectos" >
                                           <Model>
                                               <ext:Model runat="server" ID="model6" IDProperty="codigo">
                                                      <Fields>
                                                    <ext:ModelField Name="nombre" />
                                                    <ext:ModelField Name="codigo" />                                              
                                                </Fields>
                                               </ext:Model>
                                           </Model>
                                       </ext:Store>
                                   </Store>
                                  <Listeners>
                                       <Expand Handler="App.direct.cargarProyecto()" />
                                      <Select Handler="App.direct.selecProyecto(true)" />
                                   </Listeners>
                                 
                               </ext:ComboBox>
                               <ext:Button ID="Button3" runat="server" Margins="5 5 5 5" Icon="Add"
                                   ToolTip="Crear un proyecto nuevo">
                                   <DirectEvents>
                                       <Click OnEvent="ProyectoNew">
                                           <EventMask ShowMask="true" />                                          
                                       </Click>
                                   </DirectEvents>
                               </ext:Button>
                           </Items>
                       </ext:Toolbar>
                   </TopBar>
               </ext:Panel>

               <ext:Panel runat="server" Region="West" Title="Elementos" Width="300" ID="pnlSettings"
                   BodyStyle="background-color: #E1E1E1;" Collapsible="false" Split="false" MinWidth="175"
                   MaxWidth="400" MarginsSummary="31 0 5 5" CMarginsSummary="31 5 5 5"  >
                   <Items>
                   
                       <ext:TreePanel ID="TreePanel1" runat="server" Width="300" Height="700"  AutoScroll="true" RootVisible="false"
                       Animate="true" UseArrows="true">
                           <TopBar>
                               <ext:Toolbar ID="Toolbar2" runat="server">
                                   <Items>
                                       <ext:Button ID="Button1" runat="server" Text="Expandir Todo">
                                           <Listeners>
                                               <Click Handler="#{TreePanel1}.expandAll();" />
                                           </Listeners>
                                       </ext:Button>
                                       <ext:Button ID="Button2" runat="server" Text="Contraer Todo">
                                           <Listeners>
                                               <Click Handler="#{TreePanel1}.collapseAll();" />
                                           </Listeners>
                                       </ext:Button>
                                        <ext:Button ID="btn_actualizar" runat="server"  Icon="Reload" ToolTip="Actualizar">
                                          <Listeners>
                                               <Click Handler="App.direct.selecProyecto(false);" />
                                           </Listeners>
                                       </ext:Button>
                                   </Items>
                               </ext:Toolbar>
                           </TopBar>                         
                           <Root>
                        <ext:Node>
                            <Children>
                                <ext:Node  NodeID="root" Text="Plantillas" Icon="ApplicationCascade"  >
                                    <Children>
                                        <ext:Node NodeID="objetivos" Text="Objetivos Del Sistema" EmptyChildren="true" Leaf="false" Icon="ApplicationForm"/>
                                        <ext:Node  NodeID="actores" Text="Actores" Leaf="false" EmptyChildren="true" Icon="ApplicationForm" />
                                        <ext:Node NodeID="informacion" Text="Almacenamiento De Información" Leaf="false" EmptyChildren="true" Icon="ApplicationForm" />
                                        <ext:Node NodeID="funcionales" Text="Requisitos Funcionales" Leaf="false" EmptyChildren="true" Icon="ApplicationForm" />
                                        <ext:Node NodeID="no_funcionales" Text="Requisitos No Funcionales" Leaf="false" EmptyChildren="true" Icon="ApplicationForm"  />
                                    </Children>
                                </ext:Node>
                                <ext:Node Text="Diagramas" Icon="ApplicationCascade">
                                    <Children>
                                        <ext:Node NodeID="clases" Text="Clases"   Icon="Images" EmptyChildren="true" Leaf="false"/>
                                        <ext:Node  NodeID="casos" Text="Casos de uso"   Icon="Images" EmptyChildren="true" Leaf="false" />
                                        <ext:Node NodeID="secuencia" Text="Secuencia"   Icon="Images" EmptyChildren="true" Leaf="false"/>
                                    </Children>
                                </ext:Node>
                            </Children>
                        </ext:Node>
                           </Root>
                           <Listeners>
                               <ItemContextMenu Fn="showMenu" StopEvent="true" />
                               <RemoteActionRefusal Handler="Ext.Msg.alert('Action refusal', e.message);" />    
                                                   
              
                           </Listeners>
                       </ext:TreePanel>
                   </Items>
               </ext:Panel>              
           </Items>
       </ext:Panel>

          <ext:TabPanel ID="tabpanel_detalle" Region="Center" runat="server" Title="Detalles" Border="false"
              BodyStyle=" background-image : url(Styles/img/FondoPrincipaln7.jpg);"
              MarginsSummary="5 5 5 0" Layout="FitLayout">
              <TabBar>
                  <ext:ToolbarFill ID="ToolbarFill1" runat="server" />
              </TabBar>
          </ext:TabPanel>
      </Items>     
      </ext:Viewport>
              <script src="Scripts/js/bootstrap.min.js"></script>

    </div>
    </form>

     </div>
</body>
</html>
