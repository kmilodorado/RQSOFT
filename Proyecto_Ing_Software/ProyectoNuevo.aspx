<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProyectoNuevo.aspx.cs" Inherits="Proyecto_Ing_Software.ProyectoNuevo" %>


<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="Scripts/AbrirPdf.js"></script>

    
    <style type="text/css">      
	    #unlicensed{
		    display: none !important;
	    }
    </style>
     <style >
          .cbStates-list {
            width : 298px;
            font  : 11px tahoma,arial,helvetica,sans-serif;
        }
        
        .cbStates-list th {
            font-weight : bold;
        }
        
        .cbStates-list td, .cbStates-list th {
            padding : 3px;
        }

        .list-item {
            cursor : pointer;
        }
            .search-item {
            font          : normal 11px tahoma, arial, helvetica, sans-serif;
            padding       : 3px 5px 3px 5px;
            border        : 1px solid #fff;
            border-bottom : 1px solid #eeeeee;
            white-space   : normal;
            color         : #555;
        }
    </style>
      <ext:XScript ID="XScript1" runat="server">
       <script>

           var showResult = function (btn) {
               // Ext.Msg.notify("Button Click", "You clicked the " + btn + " button");

               parentAutoLoadControl.hide();
           };
           var cambio = function () {

          #{txt_nombre_fuente}.reset();
          #{txt_apellido_fuente}.reset();
          #{cbx_fuente_org}.reset();
          #{cbx_fuente_org}.getStore().removeAll();

          #{txt_nombre_fuente}.setDisabled(true);
          #{txt_apellido_fuente}.setDisabled(true);
          #{cbx_fuente_org}.setDisabled(true);
          #{btn_ingresar_fuente}.setDisabled(true);
                    
           }
      </script>
      </ext:XScript>
 

</head>
<body>
    <ext:ResourceManager ID="ResourceManager1" runat="server" />
    <ext:Toolbar ID="toolbar1" runat="server">
        <Items>
            <ext:Button ID="btn_top" runat="server" Text="Exportar" Icon="ReportGo" ToolTip="Crear documento de requisitos">
                <DirectEvents>
                    <Click OnEvent="Crear">
                        <EventMask Msg="Exportando..." ShowMask="true" MinDelay="1000" />
                    </Click>
                </DirectEvents>

            </ext:Button>

            <ext:Button ID="Button3" runat="server" Text="Historial" Icon="Zoom" ToolTip="Ver historial del proyecto">
                <DirectEvents>
                    <Click OnEvent="Historial">
                        <EventMask Msg="Cargando historial..." ShowMask="true" MinDelay="1000" />
                    </Click>
                </DirectEvents>

            </ext:Button>
        </Items>
    </ext:Toolbar>

    <ext:Viewport ID="Viewport1" runat="server" AutoScroll="true">
        <LayoutConfig >
            <ext:VBoxLayoutConfig Align="Center" Pack="Center" />
        </LayoutConfig>
        <Items>
            <ext:FormPanel ID="FormPanel1"
                runat="server"
                Frame="true"  
                Width="808"
                Height="480"         
                BodyPadding="5"
                AutoScroll="true"
                >
                <FieldDefaults LabelAlign="Top" />
                <TopBar>
                  
                </TopBar>
                <Items>

                   
                    <ext:FieldSet ID="FieldSet1"  Layout="ColumnLayout" runat="server" Title="Información de proyecto" DefaultWidth="330">
                      
                        
                         <Items >
                            <ext:TextField ID="txt_nombre" runat="server" AllowBlank="false" FieldLabel="Nombre" ColumnWidth="0.55"
                                 EmptyText="Nombre del proyecto" AutoDataBind="true" AnchorHorizontal="30%"  Padding="4" /> 
                             <ext:DateField ID="txt_fecha" runat="server" FieldLabel="Fecha" AllowBlank="false" ColumnWidth="0.15" 
                                 Padding="4" Editable="false" AnchorHorizontal="20%" Format="yyyy-MM-dd" AltFormats="yyyy-MM-dd" MaxDate="<%# DateTime.Today %>"  AutoDataBind="true" />
                            <ext:TextField ID="txt_version" runat="server" AllowBlank="false" FieldLabel="Version"
                                 Text="001" ReadOnly="true"  AnchorHorizontal="20%" ColumnWidth="0.2"  Padding="4"/>
                        </Items>
                          
                    </ext:FieldSet>
                   
                   
                     
                    <ext:FieldSet ID="FieldSet2" runat="server"  DefaultWidth="330" Layout="ColumnLayout">
                        <Items>
                         <ext:FieldSet ID="FieldSet5" runat="server" Title="Autores"  ColumnWidth="0.49" >
                              <Items>
                            <ext:ComboBox ID="cbx_autores" runat="server" FieldLabel="Autor" EmptyText="Seleccione..." AllowBlank="true"
                                QueryMode="Local" DisplayField="Nombre"  ValueField="Login" TypeAhead="true"  AnchorHorizontal="98%" ForceSelection="true">
                                <Store>
                                    <ext:Store ID="store_autores" runat="server">
                                        <Model>
                                            <ext:Model ID="Model3" runat="server">
                                                <Fields>
                                                    <ext:ModelField Name="Nombre" />
                                                    <ext:ModelField Name="Login" />
                                                    <ext:ModelField Name="Organizacion" />
                                                </Fields>
                                            </ext:Model>
                                        </Model>
                                    </ext:Store>
                                </Store>
                                <%--     <DirectEvents>                                   
                                        <Select OnEvent="cargarAutores">
                                            <EventMask ShowMask="true" Msg="Cargando ..." Target="Page" />
                                        </Select>
                                    </DirectEvents>--%>
                                     <ListConfig Width="320" Height="300" ItemSelector=".x-boundlist-item">
                                        <Tpl ID="Tpl1" runat="server">
                                            <Html>
                                                <tpl for=".">
						                               <tpl if="[xindex] == 1">
							                          <table class="cbStates-list">
								                            <tr>
									                            <th>Nombre</th>
									                            <th>Organizacion</th>
                                                                   <th>Login</th>
                                                            
								                            </tr>
						                            </tpl>
						                            <tr class="x-boundlist-item">
							                            <td>{Nombre}</td>
							                            <td>{Organizacion}</td>
                                                        <td>{Login}</td>
                                                         
						                            </tr>
						                            <tpl if="[xcount-xindex]==0">
							                            </table>
						                            </tpl>
					                            </tpl>
                                            </Html>
                                        </Tpl>       
                                                        
                                    </ListConfig>        
                                  <Listeners>
                                        <Select Handler="App.direct.valor(this.getValue());" />
                                    </Listeners>

                                   </ext:ComboBox>
                          <%--    <ext:TextField ID="nn" runat="server"  Visible="true"/>
                               <%--  <ext:ComboBox ID="cbx_org" runat="server" FieldLabel="Organización" EmptyText="Seleccione..." AllowBlank="true">
                            </ext:ComboBox>--%>
                            <ext:Container ID="Container1" runat="server" Layout="HBoxLayout" AnchorHorizontal="98%">
                                <Items>
                                    <ext:Button ID="btn_autor" runat="server" Text="Ingresar" ToolTip="Registrar Autor"
                                        Width="80" Margins="0 0 0 245" Icon="Add">
                                        <DirectEvents>
                                            <Click OnEvent="ingresarAutor">
                                                <EventMask ShowMask="true" Msg="Registrando Autor..." />
                                            </Click>
                                        </DirectEvents>
                                    </ext:Button>
                                </Items>
                            </ext:Container>
                             <ext:Container ID="Container2" runat="server" Layout="HBoxLayout" AnchorHorizontal="98%">
                                <Items>
                                    <ext:GridPanel ID="grilla_autores" runat="server" Title="Autores del proyecto" Height="180"
                                         Collapsible="true"  Collapsed="true"  Layout="HBoxLayout" Flex="3" AnchorHorizontal="98%">
                                        <Store>
                                    <ext:Store ID="store1" runat="server" PageSize="5" AnchorHorizontal="98%">
                                      <Model>
                                            <ext:Model ID="Model2" runat="server" IDProperty="Login">
                                                <Fields>
                                                    <ext:ModelField Name="Nombre" />
                                                    <ext:ModelField Name="Organizacion" />     
                                                    <ext:ModelField Name="Login" />
                                                </Fields>
                                            </ext:Model>
                                      </Model>
                                    </ext:Store>
                                        </Store>
                                        <ColumnModel ID="ColumnModel2" runat="server">
                                            <Columns>
                                                <ext:Column ID="Column3" runat="server" Text="Nombre" Flex="3" DataIndex="Nombre" />
                                                <ext:Column ID="Column4" runat="server" Text="Organización" Flex="1" DataIndex="Organizacion" />
                                                
                                            </Columns>
                                        </ColumnModel>
                                        <SelectionModel>
                                            <ext:RowSelectionModel ID="RowSelectionModel1" runat="server" Mode="Single">
                                            </ext:RowSelectionModel>
                                        </SelectionModel>
                                        <View>
                                            <ext:GridView runat="server" ID="view1" StripeRows="true" TrackOver="true" />
                                        </View>
                                        <BottomBar>
                                            <ext:PagingToolbar ID="PagingToolbar1" DisplayInfo="false" runat="server" PageSize="5" HideRefresh="true" >
                                                <Items>
                                                             
                                                      <ext:Button runat="server" ID="btn_eliminar1" Text="Eliminar" ToolTip="Eliminar autor" Icon="Delete">
                                                        <Listeners>
                                                            <Click Handler="var selection = #{grilla_autores}.getView().getSelectionModel().getSelection()[0];
                                                if (selection) {
                                                    #{grilla_autores}.store.remove(selection);  
                                                     App.direct.actualizarProyecto(App.txt_fecha.getValue(), App.txt_version.getValue(), App.grilla_autores.getRowsValues(), App.grilla_fuentes.getRowsValues());                                            
                                                }" />
                                                        </Listeners>

                                                    </ext:Button>
                                                   
                                                </Items>
                                        </ext:PagingToolbar>
                                           

                                    </BottomBar>
                                    </ext:GridPanel>

                                </Items>
                            </ext:Container>
                         </Items>
                        </ext:FieldSet>
                     
                           
                            <ext:FieldSet ID="FieldSet3" runat="server" Title="Fuentes"  ColumnWidth="0.49">
                         <Items>
                             <ext:Container ID="Container5" runat="server" Layout="ColumnLayout" AnchorHorizontal="98%">
                               
                                   <Items>
                                      
                              <ext:TextField ID="txt_idfuente" runat="server" AllowBlank="true" FieldLabel="Identificación"
                                   MaskRe="/[0-9.a-zA-Z]/" ColumnWidth="0.85" LabelAlign="Right" >
                                  <Listeners>
                                      <Change Handler="cambio();" />
                                  </Listeners>

                              </ext:TextField>
                             
                                 
                                     <ext:Button ID="btn_buscar" runat="server" Icon="Zoom"  ColumnWidth="0.1" Margins=""
                                         Width="25" ToolTip="buscar" ArrowAlign="Bottom">
                                         <DirectEvents>
                                             <Click OnEvent="buscarFuente">
                                                 <EventMask ShowMask="true" Msg="Buscando..." />
                                             </Click>
                                         </DirectEvents>
                                     </ext:Button>
                                 </Items>

                             </ext:Container>
                             <ext:Container ID="con" runat="server" Layout="ColumnLayout">
                                 <Items>
                             <ext:TextField ID="txt_nombre_fuente" runat="server" AllowBlank="true" FieldLabel="Nombre(s)"
                                 EmptyText="Nombre Completo" MaskRe="/[ -.a-zA-Z]/" Disabled="true" AnchorHorizontal="95%" ColumnWidth="0.5"/>
                             <ext:TextField ID="txt_apellido_fuente" runat="server" AllowBlank="true" FieldLabel="Apellido(s)"
                                 EmptyText="Apellido(s)" MaskRe="/[ -.a-zA-Z]/" Disabled="true" AnchorHorizontal="95%" ColumnWidth="0.5" />
                                  </Items>    
                                 </ext:Container>    
                             
                             
                                          
                             <ext:ComboBox ID="cbx_fuente_org" runat="server" FieldLabel="Organización" Disabled="true"
                                 AllowBlank="true" QueryMode="Local" AnchorHorizontal="98%" >
                                 <Triggers>
                                     <ext:FieldTrigger Icon="Clear" Qtip="Borrar" />
                                 </Triggers>
                                 <Listeners>
                                     <TriggerClick Handler="this.removeByValue(this.getValue());
                                       this.clearValue();" />
                                 </Listeners>
                             </ext:ComboBox>
                                  <ext:Container ID="Container3" runat="server" >
                                 <Items> 
                            
                             
                                     <ext:Button ID="btn_ingresar_fuente" runat="server" Text="Ingresar" ToolTip="Registrar Fuente"
                                         Width="80" Margins="0 0 0 260" Icon="Add" Disabled="true" >
                                          <DirectEvents>
                                            <Click OnEvent="ingresarFuente">
                                                <EventMask ShowMask="true" Msg="Registrando Fuente..." />
                                            </Click>
                                        </DirectEvents>
                                     </ext:Button>
                                 </Items>
                             </ext:Container>
                             <ext:Container ID="Container4" runat="server" Layout="HBoxLayout">
                                 <Items>
                                     <ext:GridPanel ID="grilla_fuentes" runat="server" Title="Fuentes del proyecto" Height="125"
                                         AutoScroll="true"  Collapsible="true" Collapsed="true" Layout="HBoxLayout" Flex="3">
                                         <Store>
                                             <ext:Store ID="store2" runat="server" PageSize="2">
                                                 <Model>
                                                     <ext:Model ID="Model1" runat="server">
                                                         <Fields>
                                                              <ext:ModelField Name="Identificacion" />
                                                             <ext:ModelField Name="Nombre" />
                                                             <ext:ModelField Name="Organizacion" />
                                                         </Fields>
                                                     </ext:Model>
                                                 </Model>
                                             </ext:Store>
                                    </Store>
                                        <ColumnModel ID="ColumnModel1" runat="server">
                                            <Columns>
                                                 <ext:Column ID="Column5" runat="server"  Text="Identificación" Flex="1" DataIndex="Identificacion" Align="Center"/>
                                                <ext:Column ID="Column1" runat="server" Text="Nombre" Weight="100" Flex="3" DataIndex="Nombre" />
                                                <ext:Column ID="Column2" runat="server" Text="Organización" Flex="1" DataIndex="Organizacion" />
                                            </Columns>
                                        </ColumnModel>
                                         <SelectionModel>
                                             <ext:RowSelectionModel ID="RowSelectionModel2" runat="server" Mode="Single">
                                             </ext:RowSelectionModel>
                                         </SelectionModel>
                                         <View>
                                             <ext:GridView runat="server" ID="GridView1" StripeRows="true" TrackOver="true" />
                                         </View>
                                         <BottomBar>
                                             <ext:PagingToolbar ID="PagingToolbar2" DisplayInfo="false" runat="server" PageSize="5" HideRefresh="true">
                                                 <Items>

                                                     <ext:Button runat="server" ID="Button2" Text="Eliminar" ToolTip="Eliminar autor" Icon="Delete">
                                                         <Listeners>
                                                             <Click Handler="var selection = #{grilla_fuentes}.getView().getSelectionModel().getSelection()[0];
                                                if (selection) {
                                                    #{grilla_fuentes}.store.remove(selection);                                                
                                                }" />
                                                         </Listeners>
                                                     </ext:Button>
                                                 </Items>
                                             </ext:PagingToolbar>
                                    </BottomBar>
                                    </ext:GridPanel>                                    
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:FieldSet>
                            
                        </Items>
                    </ext:FieldSet>
                     
                
               
                </Items>
                <Buttons>
                    <ext:Button ID="Button1" 
                        runat="server" 
                        Text="Aceptar" 
                        Disabled="true" 
                        FormBind="true"  Icon="Accept"  Hidden="false">
                     <%--    <DirectEvents>
                            <Click OnEvent="registrar">
                            <EventMask ShowMask="true" Msg="Registrando..." />
                            </Click>
                        </DirectEvents>--%>
                          <Listeners>
                        <Click Handler="App.direct.CrearProyecto(App.txt_nombre.getValue(), App.txt_fecha.getValue(),
                             App.grilla_autores.getRowsValues(), App.grilla_fuentes.getRowsValues())" />
                    </Listeners>
                    </ext:Button>

                     <ext:Button ID="btn_act" 
                        runat="server" 
                        Text="Actualizar" 
                        Disabled="true" 
                        FormBind="true"  Icon="Accept" Hidden="true" >            
                          <Listeners>
                        <Click Handler="App.direct.actualizarProyecto(App.txt_fecha.getValue(), App.txt_version.getValue(), App.grilla_autores.getRowsValues(), App.grilla_fuentes.getRowsValues())" />
                    </Listeners>
                    </ext:Button>
                </Buttons>
            </ext:FormPanel>
        </Items>
    </ext:Viewport>
</body>
</html>
