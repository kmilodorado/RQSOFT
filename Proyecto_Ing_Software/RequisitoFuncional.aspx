<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RequisitoFuncional.aspx.cs" Inherits="Proyecto_Ing_Software.RequisitoFuncional" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
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

               parentAutoLoadControl.hide();
           };
              </script>
      </ext:XScript>
</head>
<body>
   <ext:ResourceManager ID="ResourceManager1" runat="server" />
  
    
    <ext:Toolbar ID="toolbar1" runat="server">
        <Items>
            <ext:Button ID="btn_top" runat="server" Text="Historial" Icon="Zoom" ToolTip="Ver historial de la plantilla">
                <DirectEvents>
                    <Click OnEvent="Historial">
                        <EventMask Msg="Cargando historial..." ShowMask="true" MinDelay="1000" />
                    </Click>
                </DirectEvents>

            </ext:Button>
        </Items>
    </ext:Toolbar>
    <ext:Viewport ID="Viewport1" runat="server" AutoScroll="true">

        

        <LayoutConfig>
            <ext:VBoxLayoutConfig Align="Center" Pack="Center" />
        </LayoutConfig>
        <Items>
            <ext:FormPanel ID="FormPanel1"
                runat="server"
                Width="500"
                Height="520"
                Frame="true"              
                BodyPadding="13"
                AutoScroll="true">
                <FieldDefaults LabelAlign="Right" LabelWidth="115" MsgTarget="Side" />
                <Items>
                    <ext:FieldSet ID="FieldSet1" runat="server" Title="Información General" DefaultWidth="330">
                        <Items>
                            <ext:TextField ID="txt_Identificador" runat="server" FieldLabel="Identificador"
                                MaskRe="/[0-9]/"  ReadOnly="true" AnchorHorizontal="95%" />
                            <ext:TextField ID="txt_nombre" runat="server" FieldLabel="Nombre" AllowBlank="false"
                                EmptyText="Nombre descriptivo"  AnchorHorizontal="95%"/>
                            <ext:TextField ID="txt_Version" runat="server" FieldLabel="Versión" Text="001"  ReadOnly="true" AnchorHorizontal="95%" />
                            <ext:TextArea ID="txt_descripcion" runat="server" FieldLabel="Descripción" AllowBlank="false"
                                EmptyText="<<Descripción>>" AnchorHorizontal="95%" />
                             <ext:TextField ID="txt_Precondicion" runat="server" FieldLabel="Precondición"  AnchorHorizontal="95%"/>
                            <ext:TextField ID="txt_Postcondicion" runat="server" FieldLabel="Postcondición" AnchorHorizontal="95%" />
                        </Items>
                    </ext:FieldSet>
                    
            <%--        <ext:FieldSet ID="FieldSet2" runat="server" Title="Autores" DefaultWidth="330">
                        <Items>
                            <ext:ComboBox ID="cbx_autores" runat="server"  EmptyText="Seleccione..."  Editable="false">
                            </ext:ComboBox>         
                            <ext:Container ID="Container1" runat="server" Layout="HBoxLayout">
                                <Items>
                                    <ext:Button ID="btn_autor" runat="server" Text="Ingresar" ToolTip="Registrar Autor"
                                         Width="80" Margins="0 0 0 250" Icon="Add">
                                    </ext:Button>                                   
                                </Items>
                            </ext:Container>
                             <ext:Container ID="Container2" runat="server" Layout="HBoxLayout">
                                <Items>
                                    <ext:GridPanel ID="GridPanel1" runat="server" Title="Autores del Objetivo" Height="100"
                                        AutoScroll="true" Collapsible="true" Collapsed="true" Layout="HBoxLayout" Flex="3">
                                        <Store>
                                    <ext:Store ID="store1" runat="server">
                                      <Model>
                                            <ext:Model ID="Model2" runat="server">
                                                <Fields>
                                                    <ext:ModelField Name="Nombre" />
                                                    <ext:ModelField Name="Organizacón" />                                                   
                                                </Fields>
                                            </ext:Model>
                                      </Model>
                                    </ext:Store>
                                    </Store>
                                        <ColumnModel ID="ColumnModel2" runat="server">
                                            <Columns>
                                                <ext:Column ID="Column3" runat="server" Text="Nombre" Flex="3" DataIndex="Nombre" />
                                                <ext:Column ID="Column4" runat="server" Text="Organización" Flex="1" DataIndex="Organización" />
                                                 <ext:CommandColumn ID="CommandColumn5" runat="server" Width="30">
                                                    <Commands>
                                                        <ext:GridCommand Icon="Delete" CommandName="Delete">
                                                            <ToolTip Text="Eliminar" />
                                                        </ext:GridCommand>                                                                                                             
                                                    </Commands>                                                
                                                </ext:CommandColumn>
                                            </Columns>
                                        </ColumnModel>
                                    </ext:GridPanel>

                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:FieldSet>
                
                     <ext:FieldSet ID="FieldSet3" runat="server" Title="Fuentes" DefaultWidth="330">
                         <Items>
                             <ext:ComboBox ID="cbx_fuente" runat="server"  EmptyText="Seleccione..." Editable="false">
                            </ext:ComboBox>  
                             <ext:Container ID="Container3" runat="server" Layout="HBoxLayout">
                                 <Items>
                                     <ext:Button ID="btn_ingresar_fuente" runat="server" Text="Ingresar" ToolTip="Registrar Fuente"
                                         Width="80" Margins="0 0 0 250" Icon="Add">
                                     </ext:Button>
                                 </Items>
                             </ext:Container>
                             <ext:Container ID="Container4" runat="server" Layout="HBoxLayout">
                                 <Items>
                                     <ext:GridPanel ID="GridPanel2" runat="server" Title="Fuentes del Objetivo" Height="100"
                                         AutoScroll="true" Collapsible="true" Collapsed="true" Layout="HBoxLayout" Flex="3">
                                         <Store>
                                             <ext:Store ID="store2" runat="server">
                                                 <Model>
                                                     <ext:Model ID="Model1" runat="server">
                                                         <Fields>
                                                             <ext:ModelField Name="Nombre" />
                                                             <ext:ModelField Name="Organizacón" />
                                                         </Fields>
                                                     </ext:Model>
                                                 </Model>
                                             </ext:Store>
                                    </Store>
                                        <ColumnModel ID="ColumnModel1" runat="server">
                                            <Columns>
                                                <ext:Column ID="Column1" runat="server" Text="Nombre" Flex="3" DataIndex="Nombre" />
                                                <ext:Column ID="Column2" runat="server" Text="Organización" Flex="1" DataIndex="Organización" />
                                                 <ext:CommandColumn ID="CommandColumn1" runat="server" Width="30">
                                                    <Commands>
                                                        <ext:GridCommand Icon="Delete" CommandName="Delete">
                                                            <ToolTip Text="Eliminar" />
                                                        </ext:GridCommand>                                                                                                             
                                                    </Commands>                                                
                                                </ext:CommandColumn>
                                            </Columns>
                                        </ColumnModel>
                                    </ext:GridPanel>

                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:FieldSet>
                   
            
                    <ext:FieldSet ID="FieldSet5" runat="server" Title="Objetivos Asociados" DefaultWidth="330">
                        <Items>
                            <ext:ComboBox ID="cbx_obj_asociado" runat="server" EmptyText="Seleccione..." Editable="false">
                            </ext:ComboBox>
                            <ext:Container ID="Container7" runat="server" Layout="HBoxLayout">
                                <Items>
                                    <ext:Button ID="Button3" runat="server" Text="Ingresar" ToolTip="Registrar Objetivo"
                                        Width="80" Margins="0 0 0 250" Icon="Add">
                                    </ext:Button>
                                </Items>
                            </ext:Container>
                            <ext:Container ID="Container8" runat="server" Layout="HBoxLayout">
                                <Items>
                                    <ext:GridPanel ID="GridPanel4" runat="server" Title="Objetivos asociados" Height="100"
                                        AutoScroll="true" Collapsible="true" Collapsed="true" Layout="HBoxLayout" Flex="3">
                                        <Store>
                                            <ext:Store ID="store4" runat="server">
                                                <Model>
                                                    <ext:Model ID="Model4" runat="server">
                                                        <Fields>
                                                            <ext:ModelField Name="Codigo" />
                                                            <ext:ModelField Name="Nombre" />
                                                        </Fields>
                                                    </ext:Model>
                                                </Model>
                                            </ext:Store>
                                        </Store>
                                        <ColumnModel ID="ColumnModel4" runat="server">
                                            <Columns>
                                                <ext:Column ID="Column7" runat="server" Text="Código" Flex="1" DataIndex="Codigo" />
                                                <ext:Column ID="Column8" runat="server" Text="Nombre" Flex="3" DataIndex="Nombre" />
                                                 <ext:CommandColumn ID="CommandColumn2" runat="server" Width="30">
                                                    <Commands>
                                                        <ext:GridCommand Icon="Delete" CommandName="Delete">
                                                            <ToolTip Text="Eliminar" />
                                                        </ext:GridCommand>                                                                                                             
                                                    </Commands>                                                
                                                </ext:CommandColumn>
                                            </Columns>
                                        </ColumnModel>
                                    </ext:GridPanel>
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:FieldSet>

                    
            
                    <ext:FieldSet ID="FieldSet4" runat="server" Title="Requisitos Asociados" DefaultWidth="330">
                        <Items>
                            <ext:ComboBox ID="cbx_req_asociado" runat="server" EmptyText="Seleccione..." Editable="false" >
                            </ext:ComboBox>
                            <ext:Container ID="Container5" runat="server" Layout="HBoxLayout">
                                <Items>
                                    <ext:Button ID="Button2" runat="server" Text="Ingresar" ToolTip="Registrar Requisito"
                                        Width="80" Margins="0 0 0 250" Icon="Add">
                                    </ext:Button>
                                </Items>
                            </ext:Container>
                            <ext:Container ID="Container6" runat="server" Layout="HBoxLayout">
                                <Items>
                                    <ext:GridPanel ID="GridPanel3" runat="server" Title="Requisitos asociados" Height="100"
                                        AutoScroll="true" Collapsible="true" Collapsed="true" Layout="HBoxLayout" Flex="3">
                                        <Store>
                                            <ext:Store ID="store3" runat="server">
                                                <Model>
                                                    <ext:Model ID="Model3" runat="server">
                                                        <Fields>
                                                            <ext:ModelField Name="Codigo" />
                                                            <ext:ModelField Name="Nombre" />
                                                        </Fields>
                                                    </ext:Model>
                                                </Model>
                                            </ext:Store>
                                        </Store>
                                        <ColumnModel ID="ColumnModel3" runat="server">
                                            <Columns>
                                                <ext:Column ID="Column5" runat="server" Text="Código" Flex="1" DataIndex="Codigo" />
                                                <ext:Column ID="Column6" runat="server" Text="Nombre" Flex="3" DataIndex="Nombre" />
                                                 <ext:CommandColumn ID="CommandColumn3" runat="server" Width="30">
                                                    <Commands>
                                                        <ext:GridCommand Icon="Delete" CommandName="Delete">
                                                            <ToolTip Text="Eliminar" />
                                                        </ext:GridCommand>                                                                                                             
                                                    </Commands>                                                
                                                </ext:CommandColumn>
                                            </Columns>
                                        </ColumnModel>
                                    </ext:GridPanel>
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:FieldSet>--%>










                       <ext:FieldSet ID="FieldSet2" runat="server" AnchorHorizontal="100%">
                        <Items>
                            <ext:TabPanel ID="tabpanel" runat="server" Border="false" Layout="FitLayout" AnchorHorizontal="95%">
                                <TabBar>
                                </TabBar>
                                <Items>
                                    <ext:Panel runat="server" ID="panel_autor" Title="Autores" AnchorHorizontal="95%">
                                        <Items>
                                            <ext:Container ID="Container1" runat="server" Layout="HBoxLayout">
                                                <Items>
                                                    <ext:ComboBox ID="cbx_autores" runat="server" EmptyText="Seleccione..." AllowBlank="true"
                                                        QueryMode="Local" DisplayField="Nombre" Flex="2" Margins="5 5 5 10" ValueField="Login" TypeAhead="true" ForceSelection="true">
                                                        <Store>
                                                            <ext:Store ID="store_autores" runat="server">
                                                                <Model>
                                                                    <ext:Model ID="Model1" runat="server">
                                                                        <Fields>
                                                                            <ext:ModelField Name="Nombre" />
                                                                            <ext:ModelField Name="Login" />
                                                                            <ext:ModelField Name="Organizacion" />
                                                                        </Fields>
                                                                    </ext:Model>
                                                                </Model>
                                                            </ext:Store>
                                                        </Store>
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
                                                </Items>
                                            </ext:Container>
                                            <ext:Container ID="Container2" runat="server" Layout="HBoxLayout">
                                                <Items>
                                                    <ext:Button ID="btn_autor" runat="server" Text="Ingresar" ToolTip="Registrar Autor"
                                                        Width="80" Margins="0 5 5 310" Icon="Add">
                                                        <DirectEvents>
                                                            <Click OnEvent="ingresarAutor">
                                                                <EventMask ShowMask="true" Msg="Registrando Autor..." />
                                                            </Click>
                                                        </DirectEvents>
                                                    </ext:Button>
                                                </Items>
                                            </ext:Container>
                                            <ext:Container ID="Container3" runat="server" Layout="HBoxLayout">
                                                <Items>
                                                    <ext:GridPanel ID="grilla_autores" runat="server" Title="Autores del Objetivo"
                                                        AutoScroll="true" Collapsible="true" Collapsed="true" Layout="HBoxLayout" Flex="3">
                                                        <Store>
                                                            <ext:Store ID="store1" runat="server" PageSize="5">
                                                                <Model>
                                                                    <ext:Model ID="Model2" runat="server">
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
                                                            <ext:PagingToolbar ID="PagingToolbar1" DisplayInfo="false" runat="server" PageSize="5" HideRefresh="true">
                                                                <Items>

                                                                    <ext:Button runat="server" ID="btn_eliminar1" Text="Eliminar" ToolTip="Eliminar autor" Icon="Delete">
                                                                        <Listeners>
                                                                            <Click Handler="var selection = #{grilla_autores}.getView().getSelectionModel().getSelection()[0];
                                                if (selection) {
                                                    #{grilla_autores}.store.remove(selection);                                                
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
                                    </ext:Panel>
                                    <ext:Panel runat="server" ID="panel_fuente" Title="Fuentes">
                                        <Items>
                                            <ext:Container ID="Container4" runat="server" Layout="HBoxLayout">
                                                <Items>
                                                    <ext:ComboBox ID="cbx_fuentes" runat="server" EmptyText="Seleccione..." AllowBlank="true"
                                                        QueryMode="Local" DisplayField="Nombre" ValueField="id" TypeAhead="true"
                                                        Flex="2" Margins="5 5 5 10" ForceSelection="true">
                                                        <Store>
                                                            <ext:Store ID="store_fuente" runat="server" >
                                                                <Model>
                                                                    <ext:Model ID="Model4" runat="server">
                                                                        <Fields>
                                                                            <ext:ModelField Name="id" />
                                                                            <ext:ModelField Name="Nombre" />
                                                                            <ext:ModelField Name="Organizacion" />
                                                                        </Fields>
                                                                    </ext:Model>
                                                                </Model>
                                                            </ext:Store>
                                                        </Store>
                                                        <ListConfig Width="320" Height="300" ItemSelector=".x-boundlist-item">
                                                            <Tpl ID="Tpl2" runat="server">
                                                                <Html>
                                                                    <tpl for=".">
						                               <tpl if="[xindex] == 1">
							                          <table class="cbStates-list">
								                            <tr>
                                                                 <th>Ident.</th>
									                            <th>Nombre</th>
									                            <th>Organizacion</th>
                                                                  
                                                            
								                            </tr>
						                            </tpl>
						                            <tr class="x-boundlist-item">
                                                          <td>{id}</td>
							                            <td>{Nombre}</td>
							                            <td>{Organizacion}</td>
                                                      
                                                         
						                            </tr>
						                            <tpl if="[xcount-xindex]==0">
							                            </table>
						                            </tpl>
					                            </tpl>
                                                                </Html>
                                                            </Tpl>

                                                        </ListConfig>
                                                        <Listeners>
                                                            <Select Handler="App.direct.valor2(this.getValue());" />
                                                        </Listeners>
                                                    </ext:ComboBox>
                                                </Items>
                                            </ext:Container>
                                            <ext:Container ID="Container5" runat="server" Layout="HBoxLayout">
                                                <Items>
                                                    <ext:Button ID="Button3" runat="server" Text="Ingresar" ToolTip="Registrar Fuente"
                                                        Width="80" Margins="0 5 5 310" Icon="Add">
                                                        <DirectEvents>
                                                            <Click OnEvent="ingresarFuente">
                                                                <EventMask ShowMask="true" Msg="Registrando Fuente..." />
                                                            </Click>
                                                        </DirectEvents>
                                                    </ext:Button>
                                                </Items>
                                            </ext:Container>
                                            <ext:Container ID="Container6" runat="server" Layout="HBoxLayout">
                                                <Items>
                                                    <ext:GridPanel ID="grilla_fuentes" runat="server" Title="Fuentes del Objetivo"
                                                        AutoScroll="true" Collapsible="true" Collapsed="true" Layout="HBoxLayout" Flex="3">
                                                        <Store>
                                                            <ext:Store ID="store2" runat="server" PageSize="5">
                                                                <Model>
                                                                    <ext:Model ID="Model3" runat="server">
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
                                                                <ext:Column ID="Column1" runat="server" Text="Identificación" Flex="1" DataIndex="Identificacion" Align="Center" />
                                                                <ext:Column ID="Column2" runat="server" Text="Nombre" Flex="3" DataIndex="Nombre" />
                                                                <ext:Column ID="Column7" runat="server" Text="Organización" Flex="1" DataIndex="Organizacion" />
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
                                    </ext:Panel>
                                    <ext:Panel runat="server" ID="panel_obj_asociados" Title="Objetivos Asociados">
                                        <Items>
                                            <ext:Container ID="Container7" runat="server" Layout="HBoxLayout">
                                                <Items>
                                                      <ext:ComboBox ID="cbx_objetivo" runat="server" EmptyText="Seleccione..." AllowBlank="true"
                                                        QueryMode="Local" DisplayField="Nombre" ValueField="id" TypeAhead="true"
                                                        Flex="2" Margins="5 5 5 10" ForceSelection="true">
                                                        <Store>
                                                            <ext:Store ID="store_objetivo" runat="server" >
                                                                <Model>
                                                                    <ext:Model ID="Model8" runat="server">
                                                                        <Fields>
                                                                            <ext:ModelField Name="id" />
                                                                            <ext:ModelField Name="Nombre" />                                                                            
                                                                        </Fields>
                                                                    </ext:Model>
                                                                </Model>
                                                            </ext:Store>
                                                        </Store>
                                                        <ListConfig Width="320" Height="300" ItemSelector=".x-boundlist-item">
                                                            <Tpl ID="Tpl3" runat="server">
                                                                <Html>
                                                                    <tpl for=".">
						                               <tpl if="[xindex] == 1">
							                          <table class="cbStates-list">
								                            <tr>
                                                                 <th>Ref.</th>
									                            <th>Nombre</th>	       
								                            </tr>
						                            </tpl>
						                            <tr class="x-boundlist-item">
                                                          <td>{id}</td>
							                            <td>{Nombre}</td>
						                            </tr>
						                            <tpl if="[xcount-xindex]==0">
							                            </table>
						                            </tpl>
					                            </tpl>
                                                                </Html>
                                                            </Tpl>

                                                        </ListConfig>
                                                        <Listeners>
                                                            <Select Handler="App.direct.valor3(this.getValue());" />
                                                        </Listeners>
                                                    </ext:ComboBox>
                                                </Items>
                                            </ext:Container>

                                            <ext:Container ID="Container8" runat="server" Layout="HBoxLayout">
                                                <Items>
                                                    <ext:Button ID="btn_objetivo" runat="server" Text="Ingresar" ToolTip="Registrar Objetivo"
                                                        Width="80" Margins="0 5 5 310" Icon="Add">
                                                         <DirectEvents>
                                                            <Click OnEvent="ingresarObjetivo">
                                                                <EventMask ShowMask="true" Msg="Registrando Objetivo..." />
                                                            </Click>
                                                        </DirectEvents>
                                                    </ext:Button>
                                                </Items>
                                            </ext:Container>
                                            <ext:Container ID="Container15" runat="server" Layout="HBoxLayout">
                                                <Items>
                                                    <ext:GridPanel ID="grilla_objetivos" runat="server" Title="Objetivos asociados" 
                                                        AutoScroll="true" Collapsible="true" Collapsed="true" Layout="HBoxLayout" Flex="3">
                                                        <Store>
                                                            <ext:Store ID="store3" runat="server" PageSize="5">
                                                                <Model>
                                                                    <ext:Model ID="Model9" runat="server">
                                                                        <Fields>
                                                                            <ext:ModelField Name="id" />
                                                                            <ext:ModelField Name="Nombre" />
                                                                        </Fields>
                                                                    </ext:Model>
                                                                </Model>
                                                            </ext:Store>
                                                        </Store>
                                                        <ColumnModel ID="ColumnModel3" runat="server">
                                                            <Columns>
                                                                <ext:Column ID="Column5" runat="server" Text="Referencia" Flex="1" DataIndex="id" />
                                                                <ext:Column ID="Column6" runat="server" Text="Nombre" Flex="3" DataIndex="Nombre" />

                                                            </Columns>
                                                        </ColumnModel>
                                                        <SelectionModel>
                                                            <ext:RowSelectionModel ID="RowSelectionModel3" runat="server" Mode="Single">
                                                            </ext:RowSelectionModel>
                                                        </SelectionModel>
                                                        <View>
                                                            <ext:GridView runat="server" ID="GridView2" StripeRows="true" TrackOver="true" />
                                                        </View>
                                                        <BottomBar>
                                                            <ext:PagingToolbar ID="PagingToolbar3" DisplayInfo="false" runat="server" PageSize="5" HideRefresh="true">
                                                                <Items>

                                                                    <ext:Button runat="server" ID="Button7" Text="Eliminar" ToolTip="Eliminar autor" Icon="Delete">
                                                                        <Listeners>
                                                                            <Click Handler="var selection = #{grilla_objetivos}.getView().getSelectionModel().getSelection()[0];
                                                if (selection) {
                                                    #{grilla_objetivos}.store.remove(selection);                                                
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
                                    </ext:Panel>

                                     <ext:Panel runat="server" ID="panel_req_asociados" Title="Requisitos Asociados">
                                        <Items>
                                            <ext:Container ID="Container16" runat="server" Layout="HBoxLayout">
                                                <Items>
                                                      <ext:ComboBox ID="cbx_requisitos" runat="server" EmptyText="Seleccione..." AllowBlank="true"
                                                        QueryMode="Local" DisplayField="Nombre" ValueField="id" TypeAhead="true"
                                                        Flex="2" Margins="5 5 5 10" ForceSelection="true">
                                                        <Store>
                                                            <ext:Store ID="store_requisitos" runat="server" >
                                                                <Model>
                                                                    <ext:Model ID="Model10" runat="server">
                                                                        <Fields>
                                                                            <ext:ModelField Name="id" />
                                                                            <ext:ModelField Name="Nombre" />                                                                            
                                                                        </Fields>
                                                                    </ext:Model>
                                                                </Model>
                                                            </ext:Store>
                                                        </Store>
                                                        <ListConfig Width="320" Height="300" ItemSelector=".x-boundlist-item">
                                                            <Tpl ID="Tpl4" runat="server">
                                                                <Html>
                                                                    <tpl for=".">
						                               <tpl if="[xindex] == 1">
							                          <table class="cbStates-list">
								                            <tr>
                                                                 <th>Ref.</th>
									                            <th>Nombre</th>	       
								                            </tr>
						                            </tpl>
						                            <tr class="x-boundlist-item">
                                                          <td>{id}</td>
							                            <td>{Nombre}</td>
						                            </tr>
						                            <tpl if="[xcount-xindex]==0">
							                            </table>
						                            </tpl>
					                            </tpl>
                                                                </Html>
                                                            </Tpl>

                                                        </ListConfig>
                                                        <Listeners>
                                                            <Select Handler="App.direct.valor4(this.getValue());" />
                                                        </Listeners>
                                                    </ext:ComboBox>
                                                </Items>
                                            </ext:Container>

                                            <ext:Container ID="Container17" runat="server" Layout="HBoxLayout">
                                                <Items>
                                                    <ext:Button ID="btn_requisito" runat="server" Text="Ingresar" ToolTip="Registrar Requisito"
                                                        Width="80" Margins="0 5 5 310" Icon="Add">
                                                         <DirectEvents>
                                                            <Click OnEvent="ingresarRequisito">
                                                                <EventMask ShowMask="true" Msg="Registrando Requisito..." />
                                                            </Click>
                                                        </DirectEvents>
                                                    </ext:Button>
                                                </Items>
                                            </ext:Container>
                                            <ext:Container ID="Container18" runat="server" Layout="HBoxLayout">
                                                <Items>
                                                    <ext:GridPanel ID="grilla_requisitos" runat="server" Title="Requisitos asociados" 
                                                        AutoScroll="true" Collapsible="true" Collapsed="true" Layout="HBoxLayout" Flex="3">
                                                        <Store>
                                                            <ext:Store ID="store4" runat="server" PageSize="5">
                                                                <Model>
                                                                    <ext:Model ID="Model11" runat="server">
                                                                        <Fields>
                                                                            <ext:ModelField Name="id" />
                                                                            <ext:ModelField Name="Nombre" />
                                                                        </Fields>
                                                                    </ext:Model>
                                                                </Model>
                                                            </ext:Store>
                                                        </Store>
                                                        <ColumnModel ID="ColumnModel4" runat="server">
                                                            <Columns>
                                                                <ext:Column ID="Column8" runat="server" Text="Referencia" Flex="1" DataIndex="id" />
                                                                <ext:Column ID="Column15" runat="server" Text="Nombre" Flex="3" DataIndex="Nombre" />

                                                            </Columns>
                                                        </ColumnModel>
                                                        <SelectionModel>
                                                            <ext:RowSelectionModel ID="RowSelectionModel4" runat="server" Mode="Single">
                                                            </ext:RowSelectionModel>
                                                        </SelectionModel>
                                                        <View>
                                                            <ext:GridView runat="server" ID="GridView3" StripeRows="true" TrackOver="true" />
                                                        </View>
                                                        <BottomBar>
                                                            <ext:PagingToolbar ID="PagingToolbar4" DisplayInfo="false" runat="server" PageSize="5" HideRefresh="true">
                                                                <Items>

                                                                    <ext:Button runat="server" ID="Button8" Text="Eliminar" ToolTip="Eliminar" Icon="Delete">
                                                                        <Listeners>
                                                                            <Click Handler="var selection = #{grilla_requisitos}.getView().getSelectionModel().getSelection()[0];
                                                if (selection) {
                                                    #{grilla_requisitos}.store.remove(selection);                                                
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
                                    </ext:Panel>
                                </Items>
                            </ext:TabPanel>
                        </Items>
                    </ext:FieldSet>





                        <%-- -------------------------------------------------------------------------------------- --%>

                    <ext:FieldSet ID="FieldSet3" runat="server" AnchorHorizontal="100%">
                        <Items>
                            <ext:TabPanel ID="tabpanel1" runat="server" Border="false" Layout="FitLayout" AnchorHorizontal="100%">
                                <Items>
                                      <ext:Panel runat="server" ID="panel1" Title="Secuencia">
                                        <Items>
                                             <ext:Container ID="Container24" runat="server" Layout="HBoxLayout">
                                                <Items>
                                                     <ext:ComboBox ID="cbx_actor" runat="server" Margins="5 5 5 10"
                                                             Flex="3" EmptyText="<<seleccione actor>>"  ForceSelection="true"/>
                                                </Items>
                                            </ext:Container>

                                            <ext:Container ID="Container9" runat="server" Layout="HBoxLayout">
                                                <Items>
                                                      <ext:TextField ID="txt_secuencia" runat="server"  Margins="5 5 5 10"
                                                             Flex="3" EmptyText="<<Ingrese el paso a registrar>>"/>
                                                </Items>
                                            </ext:Container>
                                            <ext:Container ID="Container10" runat="server" Layout="HBoxLayout">
                                                <Items>
                                                    <ext:Button ID="Button4" runat="server"  Text="Ingresar" ToolTip="Registrar Paso"
                                                        Width="80" Margins="0 5 5 310" Icon="Add">
                                                        <Listeners>
                                                            <Click Handler="App.direct.ingresarPaso(App.grilla_secuencia.getRowsValues(), App.txt_secuencia.getValue(), App.cbx_actor.getValue())" />
                                                        </Listeners>
                                            </ext:Button>
                                                </Items>
                                            </ext:Container>
                                            <ext:Container ID="Container11" runat="server" Layout="HBoxLayout">
                                                <Items>
                                                    <ext:GridPanel ID="grilla_secuencia" runat="server" Title="Secuencia" 
                                                        AutoScroll="true" Collapsible="true" Collapsed="true" Layout="HBoxLayout" Flex="3">
                                                        <Store>
                                                            <ext:Store ID="store6" runat="server" PageSize="5">
                                                                <Model>
                                                                    <ext:Model ID="Model6" runat="server">
                                                                        <Fields>
                                                                            <ext:ModelField Name="id" />
                                                                            <ext:ModelField Name="Nombre" />
                                                                        </Fields>
                                                                    </ext:Model>
                                                                </Model>
                                                            </ext:Store>
                                                        </Store>
                                                        <ColumnModel ID="ColumnModel5" runat="server">
                                                            <Columns>
                                                                <ext:Column ID="Column9" runat="server" Text="Paso" Flex="1" DataIndex="id" />
                                                                <ext:Column ID="Column10" runat="server" Text="Descripción" Flex="3" DataIndex="Nombre" />

                                                            </Columns>
                                                                    </ColumnModel>
                                                                    <SelectionModel>
                                                                        <ext:RowSelectionModel ID="RowSelectionModel5" runat="server" Mode="Single">
                                                                        <DirectEvents>
                                                                            <Select OnEvent="selectRow" Success="">
                                                                                <ExtraParams>
                                                                                    <ext:Parameter Name="id" Value="record.data['id']" Mode="Raw" />
                                                                                </ExtraParams>                                                                               
                                                                            </Select>
                                                                        </DirectEvents>
                                                                        </ext:RowSelectionModel>
                                                                    </SelectionModel>
                                                                    <View>
                                                            <ext:GridView runat="server" ID="GridView4" StripeRows="true" TrackOver="true" />
                                                        </View>
                                                        <BottomBar>
                                                            <ext:PagingToolbar ID="PagingToolbar5" DisplayInfo="false" runat="server" PageSize="5" HideRefresh="true">
                                                                <Items>

                                                                    <ext:Button runat="server" ID="Button5" Text="Eliminar" ToolTip="Eliminar" Icon="Delete">
                                                                        <Listeners>
                                                                            <Click Handler="var selection = #{grilla_secuencia}.getView().getSelectionModel().getSelection()[0];
                                                if (selection) { #{grilla_secuencia}.store.remove(selection);}; App.direct.eliminarPaso(#{grilla_secuencia}.getRowsValues())" />
                                                                        </Listeners>
                                                                    </ext:Button>
                                                                </Items>
                                                            </ext:PagingToolbar>
                                                        </BottomBar>
                                                    </ext:GridPanel>
                                                </Items>
                                            </ext:Container>
                                        </Items>
                                    </ext:Panel>



                                      <ext:Panel runat="server" ID="panel2" Title="Excepciones">
                                        <Items>
                                            <ext:Container ID="Container12" runat="server" Layout="HBoxLayout">
                                                <Items>
                                                      <ext:ComboBox ID="cbx_pasos_excep" runat="server" EmptyText="Seleccione el paso..." AllowBlank="true"
                                                        QueryMode="Local" DisplayField="Nombre" ValueField="id" TypeAhead="true"
                                                        Flex="2" Margins="5 5 5 10" ForceSelection="true">
                                                        <Store>
                                                            <ext:Store ID="store5" runat="server" >
                                                                <Model>
                                                                    <ext:Model ID="Model5" runat="server">
                                                                        <Fields>
                                                                            <ext:ModelField Name="id" />
                                                                            <ext:ModelField Name="Nombre" />                                                                            
                                                                        </Fields>
                                                                    </ext:Model>
                                                                </Model>
                                                            </ext:Store>
                                                        </Store>
                                                        <ListConfig Width="320" Height="300" ItemSelector=".x-boundlist-item">
                                                            <Tpl ID="Tpl5" runat="server">
                                                                <Html>
                                                                    <tpl for=".">
						                               <tpl if="[xindex] == 1">
							                          <table class="cbStates-list">
								                            <tr>
                                                                 <th>Paso</th>
									                            <th>Descripción</th>	       
								                            </tr>
						                            </tpl>
						                            <tr class="x-boundlist-item">
                                                          <td>{id}</td>
							                            <td>{Nombre}</td>
						                            </tr>
						                            <tpl if="[xcount-xindex]==0">
							                            </table>
						                            </tpl>
					                            </tpl>
                                                                </Html>
                                                            </Tpl>

                                                        </ListConfig>
                                                        <Listeners>
                                                            <Select Handler="App.direct.valor5(this.getValue());" />
                                                        </Listeners>
                                                    </ext:ComboBox>
                                                </Items>
                                            </ext:Container>
                                            <ext:Container ID="Container19" runat="server" Layout="HBoxLayout">
                                                <Items>
                                                      <ext:TextField ID="txt_excepcion" runat="server"  Margins="5 5 5 10"
                                                             Flex="3" EmptyText="<<Ingrese la excepción>>" Disabled="true"/>
                                                </Items>
                                            </ext:Container>
                                            <ext:Container ID="Container13" runat="server" Layout="HBoxLayout">
                                                <Items>
                                                    <ext:Button ID="btn_axcep" runat="server" Text="Ingresar" ToolTip="Registrar excepción "
                                                        Width="80" Margins="0 5 5 310" Icon="Add" Disabled="true">
                                                         <Listeners>
                                                    <Click Handler="App.direct.ingresarExc(App.grilla_excepciones.getRowsValues(), App.txt_excepcion.getValue())" />
                                                </Listeners>
                                                    </ext:Button>
                                                </Items>
                                            </ext:Container>

                                            
                                            <ext:Container ID="Container14" runat="server" Layout="HBoxLayout">
                                                <Items>
                                                    <ext:GridPanel ID="grilla_excepciones" runat="server" Title="Excepciones" 
                                                        AutoScroll="true" Collapsible="true" Collapsed="true" Layout="HBoxLayout" Flex="3">
                                                        <Store>
                                                            <ext:Store ID="store7" runat="server" PageSize="5">
                                                                <Model>
                                                                    <ext:Model ID="Model7" runat="server">
                                                                        <Fields>
                                                                            <ext:ModelField Name="id" />
                                                                            <ext:ModelField Name="Nombre" />
                                                                        </Fields>
                                                                    </ext:Model>
                                                                </Model>
                                                            </ext:Store>
                                                        </Store>
                                                        <ColumnModel ID="ColumnModel6" runat="server">
                                                            <Columns>
                                                                <ext:Column ID="Column11" runat="server" Text="Paso" Flex="1" DataIndex="id" />
                                                                <ext:Column ID="Column12" runat="server" Text="Descripción" Flex="3" DataIndex="Nombre" />

                                                            </Columns>
                                                        </ColumnModel>
                                                        <SelectionModel>
                                                            <ext:RowSelectionModel ID="RowSelectionModel6" runat="server" Mode="Single">
                                                            </ext:RowSelectionModel>
                                                        </SelectionModel>
                                                        <View>
                                                            <ext:GridView runat="server" ID="GridView5" StripeRows="true" TrackOver="true" />
                                                        </View>
                                                        <BottomBar>
                                                            <ext:PagingToolbar ID="PagingToolbar6" DisplayInfo="false" runat="server" PageSize="5" HideRefresh="true">
                                                                <Items>

                                                                    <ext:Button runat="server" ID="Button9" Text="Eliminar" ToolTip="Eliminar" Icon="Delete">
                                                                        <Listeners>
                                                                            <Click Handler="var selection = #{grilla_excepciones}.getView().getSelectionModel().getSelection()[0];
                                                if (selection) {
                                                    #{grilla_excepciones}.store.remove(selection);                                                
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
                                    </ext:Panel>




                                      <ext:Panel runat="server" ID="panel3" Title="Rendimiento">
                                        <Items>
                                            <ext:Container ID="Container20" runat="server" Layout="HBoxLayout">
                                                <Items>
                                                      <ext:ComboBox ID="cbx_pasos_rendimiento" runat="server" EmptyText="Seleccione el paso..." AllowBlank="true"
                                                        QueryMode="Local" DisplayField="Nombre" ValueField="id" TypeAhead="true"
                                                        Flex="2" Margins="5 5 5 10" ForceSelection="true">
                                                        <Store>
                                                            <ext:Store ID="store8" runat="server" >
                                                                <Model>
                                                                    <ext:Model ID="Model12" runat="server">
                                                                        <Fields>
                                                                            <ext:ModelField Name="id" />
                                                                            <ext:ModelField Name="Nombre" />                                                                            
                                                                        </Fields>
                                                                    </ext:Model>
                                                                </Model>
                                                            </ext:Store>
                                                        </Store>
                                                        <ListConfig Width="320" Height="300" ItemSelector=".x-boundlist-item">
                                                            <Tpl ID="Tpl6" runat="server">
                                                                <Html>
                                                                    <tpl for=".">
						                               <tpl if="[xindex] == 1">
							                          <table class="cbStates-list">
								                            <tr>
                                                                 <th>Paso</th>
									                            <th>Descripción</th>	       
								                            </tr>
						                            </tpl>
						                            <tr class="x-boundlist-item">
                                                          <td>{id}</td>
							                            <td>{Nombre}</td>
						                            </tr>
						                            <tpl if="[xcount-xindex]==0">
							                            </table>
						                            </tpl>
					                            </tpl>
                                                                </Html>
                                                            </Tpl>

                                                        </ListConfig>
                                                        <Listeners>
                                                            <Select Handler="App.direct.valor6(this.getValue());" />
                                                        </Listeners>
                                                    </ext:ComboBox>
                                                </Items>
                                            </ext:Container>

                                            <ext:Container ID="Container21" runat="server" Layout="HBoxLayout">
                                                <Items>
                                                      <ext:TextField ID="txt_rendimiento" runat="server"  Margins="5 80 5 10"
                                                           Flex="3" EmptyText="<<Ingrese el rendimiento>>" Disabled="true"/>
                                                </Items>
                                            </ext:Container>

                                            <ext:Container ID="Container22" runat="server" Layout="HBoxLayout">
                                                <Items>
                                                    <ext:Button ID="btn_rendimiento" runat="server" Text="Ingresar" ToolTip="Registrar Rendmiento "
                                                        Width="80" Margins="0 5 5 310" Icon="Add" Disabled="true">
                                                               <Listeners>
                                                                <Click Handler="App.direct.ingresarRend(App.grilla_rendimiento.getRowsValues(), App.txt_rendimiento.getValue())" />
                                                            </Listeners>
                                                    </ext:Button>
                                                </Items>
                                            </ext:Container>

                                            
                                            <ext:Container ID="Container23" runat="server" Layout="HBoxLayout">
                                                <Items>
                                                    <ext:GridPanel ID="grilla_rendimiento" runat="server" Title="Rendimiento" 
                                                        AutoScroll="true" Collapsible="true" Collapsed="true" Layout="HBoxLayout" Flex="3">
                                                        <Store>
                                                            <ext:Store ID="store9" runat="server" PageSize="5">
                                                                <Model>
                                                                    <ext:Model ID="Model13" runat="server">
                                                                        <Fields>
                                                                            <ext:ModelField Name="id" />
                                                                            <ext:ModelField Name="Nombre" />
                                                                        </Fields>
                                                                    </ext:Model>
                                                                </Model>
                                                            </ext:Store>
                                                        </Store>
                                                        <ColumnModel ID="ColumnModel7" runat="server">
                                                            <Columns>
                                                                <ext:Column ID="Column13" runat="server" Text="Paso" Flex="1" DataIndex="id" />
                                                                <ext:Column ID="Column14" runat="server" Text="Descripción" Flex="3" DataIndex="Nombre" />

                                                            </Columns>
                                                        </ColumnModel>
                                                        <SelectionModel>
                                                            <ext:RowSelectionModel ID="RowSelectionModel7" runat="server" Mode="Single">
                                                            </ext:RowSelectionModel>
                                                        </SelectionModel>
                                                        <View>
                                                            <ext:GridView runat="server" ID="GridView6" StripeRows="true" TrackOver="true" />
                                                        </View>
                                                        <BottomBar>
                                                            <ext:PagingToolbar ID="PagingToolbar7" DisplayInfo="false" runat="server" PageSize="5" HideRefresh="true">
                                                                <Items>

                                                                    <ext:Button runat="server" ID="Button11" Text="Eliminar" ToolTip="Eliminar" Icon="Delete">
                                                                        <Listeners>
                                                                            <Click Handler="var selection = #{grilla_rendimiento}.getView().getSelectionModel().getSelection()[0];
                                                if (selection) {
                                                    #{grilla_rendimiento}.store.remove(selection);                                                
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
                                    </ext:Panel>
                                  </Items>
                            </ext:TabPanel>                        
                          </Items>
                    </ext:FieldSet>


                    <%--    <ext:FieldSet ID="FieldSet7" runat="server" Title="Secuencia Normal" DefaultWidth="330">
                        <Items>
                          
                            <ext:TextField ID="txt_Secuencia" runat="server" FieldLabel="Secuencia" />
                            <ext:Container ID="Container9" runat="server" Layout="HBoxLayout">
                                <Items>
                                    <ext:Button ID="Button4" runat="server" Text="Ingresar" ToolTip="Registrar Secuencia"
                                        Width="80" Margins="0 0 0 250" Icon="Add">
                                    </ext:Button>
                                </Items>
                            </ext:Container>
                            <ext:Container ID="Container10" runat="server" Layout="HBoxLayout">
                                <Items>
                                    <ext:GridPanel ID="GridPanel5" runat="server" Title="Secuencia" Height="100" AutoScroll="true"
                                        Collapsible="true" Collapsed="true" Layout="HBoxLayout" Flex="3">
                                        <Store>
                                            <ext:Store ID="store5" runat="server">
                                                <Model>
                                                    <ext:Model ID="Model5" runat="server">
                                                        <Fields>
                                                            <ext:ModelField Name="Paso" />
                                                            <ext:ModelField Name="Nombre" />
                                                        </Fields>
                                                    </ext:Model>
                                                </Model>
                                            </ext:Store>
                                        </Store>
                                        <ColumnModel ID="ColumnModel5" runat="server">
                                            <Columns>
                                                <ext:Column ID="Column9" runat="server" Text="Paso" Flex="1" DataIndex="Paso" />
                                                <ext:Column ID="Column10" runat="server" Text="Nombre" Flex="3" DataIndex="Nombre" />
                                                 <ext:CommandColumn ID="CommandColumn4" runat="server" Width="30">
                                                    <Commands>
                                                        <ext:GridCommand Icon="Delete" CommandName="Delete">
                                                            <ToolTip Text="Eliminar" />
                                                        </ext:GridCommand>                                                                                                             
                                                    </Commands>                                                
                                                </ext:CommandColumn>
                                            </Columns>
                                        </ColumnModel>
                                    </ext:GridPanel>
                                </Items>
                            </ext:Container>                            
                      
                        </Items>
                    </ext:FieldSet>
                     
            
                    <ext:FieldSet ID="FieldSet8" runat="server" Title="Excepciones" DefaultWidth="330">
                        <Items>
                            <ext:ComboBox ID="cbx_paso" runat="server" EmptyText="Seleccione..." Editable="false" FieldLabel="Paso"  Flex="1">
                            </ext:ComboBox>
                            <ext:TextField ID="txt_excep" runat="server" FieldLabel="Excepción" />
                            <ext:Container ID="Container11" runat="server" Layout="HBoxLayout">
                                <Items>
                                    <ext:Button ID="Button5" runat="server" Text="Ingresar" ToolTip="Registrar Excepción"
                                        Width="80" Margins="0 0 0 250" Icon="Add">
                                    </ext:Button>
                                </Items>
                            </ext:Container>
                            <ext:Container ID="Container12" runat="server" Layout="HBoxLayout">
                                <Items>
                                    <ext:GridPanel ID="GridPanel6" runat="server" Title="Excepciones" Height="100" AutoScroll="true"
                                        Collapsible="true" Collapsed="true" Layout="HBoxLayout" Flex="3">
                                        <Store>
                                            <ext:Store ID="store6" runat="server">
                                                <Model>
                                                    <ext:Model ID="Model6" runat="server">
                                                        <Fields>
                                                            <ext:ModelField Name="Paso" />
                                                            <ext:ModelField Name="Excepcion" />
                                                        </Fields>
                                                    </ext:Model>
                                                </Model>
                                            </ext:Store>
                                        </Store>
                                        <ColumnModel ID="ColumnModel6" runat="server">
                                            <Columns>
                                                <ext:Column ID="Column11" runat="server" Text="Paso" Flex="1" DataIndex="Paso" />
                                                <ext:Column ID="Column12" runat="server" Text="Excepción" Flex="3" DataIndex="Excepcion" />
                                                 <ext:CommandColumn ID="CommandColumn6" runat="server" Width="30">
                                                    <Commands>
                                                        <ext:GridCommand Icon="Delete" CommandName="Delete">
                                                            <ToolTip Text="Eliminar" />
                                                        </ext:GridCommand>                                                                                                             
                                                    </Commands>                                                
                                                </ext:CommandColumn>
                                            </Columns>
                                        </ColumnModel>
                                    </ext:GridPanel>
                                </Items>
                            </ext:Container>                            
                      
                        </Items>
                    </ext:FieldSet>

                    
            
                    <ext:FieldSet ID="FieldSet9" runat="server" Title="Rendimiento" DefaultWidth="330">
                        <Items>
                            <ext:ComboBox ID="cbx_pasoR" runat="server" EmptyText="Seleccione..." Editable="false" FieldLabel="Paso"  Flex="1">
                            </ext:ComboBox>
                            <ext:TextField ID="txt_rendimiento" runat="server" FieldLabel="Rendimiento" />
                            <ext:Container ID="Container13" runat="server" Layout="HBoxLayout">
                                <Items>
                                    <ext:Button ID="Button6" runat="server" Text="Ingresar" ToolTip="Registrar Rendimiento"
                                        Width="80" Margins="0 0 0 250" Icon="Add">
                                    </ext:Button>
                                </Items>
                            </ext:Container>
                            <ext:Container ID="Container14" runat="server" Layout="HBoxLayout">
                                <Items>
                                    <ext:GridPanel ID="GridPanel7" runat="server" Title="Rendimiento" Height="100" AutoScroll="true"
                                        Collapsible="true" Collapsed="true" Layout="HBoxLayout" Flex="3">
                                        <Store>
                                            <ext:Store ID="store7" runat="server">
                                                <Model>
                                                    <ext:Model ID="Model7" runat="server">
                                                        <Fields>
                                                            <ext:ModelField Name="Paso" />
                                                            <ext:ModelField Name="Rendimiento" />
                                                        </Fields>
                                                    </ext:Model>
                                                </Model>
                                            </ext:Store>
                                        </Store>
                                        <ColumnModel ID="ColumnModel7" runat="server">
                                            <Columns>
                                                <ext:Column ID="Column13" runat="server" Text="Paso" Flex="1" DataIndex="Paso" />
                                                <ext:Column ID="Column14" runat="server" Text="Rendimiento" Flex="3" DataIndex="Rendimiento" />
                                                 <ext:CommandColumn ID="CommandColumn7" runat="server" Width="30">
                                                    <Commands>
                                                        <ext:GridCommand Icon="Delete" CommandName="Delete">
                                                            <ToolTip Text="Eliminar" />
                                                        </ext:GridCommand>                                                                                                             
                                                    </Commands>                                                
                                                </ext:CommandColumn>
                                            </Columns>
                                        </ColumnModel>
                                    </ext:GridPanel>
                                </Items>
                            </ext:Container>                            
                      
                        </Items>
                    </ext:FieldSet>--%>


                           <%-- -------------------------------------------------------------------------------------- --%>



                    <ext:FieldSet ID="FieldSet6" runat="server" Title="Otra Información" DefaultWidth="330">
                        <Items>
                         <ext:TextField ID="txt_Frecuencia" runat="server" FieldLabel="Frecuencia Esperada" AnchorHorizontal="95%" />
                           <ext:ComboBox ID="cbx_Importancia" runat="server" FieldLabel="Importancia" Editable="false" AllowBlank="false" AnchorHorizontal="95%">
                    <Items>
                            <ext:ListItem Text="Vital"  />
                        <ext:ListItem Text="Importante" />
                        <ext:ListItem Text="Quedaría Bien" />
                        <ext:ListItem Text="Por Determinar" />
                    </Items>
                </ext:ComboBox>
                 <ext:ComboBox ID="cbx_Urgencia" runat="server" FieldLabel="Urgencia" Editable="false" AllowBlank="false" AnchorHorizontal="95%">
                    <Items>
                    <ext:ListItem Text="Inmediatamente" />
                        <ext:ListItem Text="Hay Presión" />
                        <ext:ListItem Text="Puede Esperar" />
                        <ext:ListItem Text="Por Determinar" />
                    </Items>
                </ext:ComboBox>
                 <ext:ComboBox ID="cbx_Estado" runat="server" FieldLabel="Estado" Editable="false" AllowBlank="false" AnchorHorizontal="95%">
                    <Items>
                         <ext:ListItem Text="En Construcción" />
                        <ext:ListItem Text="Pendiente De Negociación" />
                        <ext:ListItem Text="Pendiente De Validación" />
                        <ext:ListItem Text="Validado" />
                    </Items>
                </ext:ComboBox>
                 <ext:ComboBox ID="cbx_Estabilidad" runat="server" FieldLabel="Estabilidad" Editable="false" AllowBlank="false" AnchorHorizontal="95%">
                    <Items>
                         <ext:ListItem Text="Alta" />
                        <ext:ListItem Text="Media" />
                        <ext:ListItem Text="Baja" />
                        <ext:ListItem Text="Por Determinar"/>
                    </Items>
                </ext:ComboBox>
             
                  <ext:TextArea ID="txt_Comentarios" runat="server" FieldLabel="Comentarios" EmptyText="<<Comentarios>>" AnchorHorizontal="95%"/>
                        </Items>
                    </ext:FieldSet>

                </Items>
                <Buttons>
                    <ext:Button ID="Button1"
                        runat="server"
                        Text="Aceptar"
                        Disabled="true"
                        FormBind="true" Icon="Add">
                        <Listeners>
                            <Click Handler="App.direct.CrearPlantilla([App.txt_Identificador.getValue(), App.txt_nombre.getValue(), 
                            App.txt_Version.getValue(), App.txt_descripcion.getValue(),
                                App.txt_Precondicion.getValue(), App.txt_Postcondicion.getValue(), App.txt_Frecuencia.getValue(),
                        App.cbx_Importancia.getValue(), App.cbx_Urgencia.getValue(), 
                            App.cbx_Estado.getValue(), App.cbx_Estabilidad.getValue(), App.txt_Comentarios.getValue()],
                                 [App.grilla_autores.getRowsValues(),App.grilla_fuentes.getRowsValues(),
                                App.grilla_objetivos.getRowsValues(), App.grilla_requisitos.getRowsValues(),
                                 App.grilla_secuencia.getRowsValues(), App.grilla_excepciones.getRowsValues(), App.grilla_rendimiento.getRowsValues()])" />
                        </Listeners>
                    </ext:Button>

                    <ext:Button ID="btn_act" 
                        runat="server" 
                        Text="Actualizar" 
                        Disabled="true" 
                        FormBind="true"  Icon="Accept" Hidden="true" >            
                          <Listeners>
                        
                              <Click Handler="App.direct.Actualizar([App.txt_nombre.getValue(), 
                             App.txt_descripcion.getValue(), App.txt_Comentarios.getValue(), App.txt_Identificador.getValue(),
                             App.txt_Version.getValue(), App.cbx_Importancia.getValue(),App.cbx_Urgencia.getValue(),
                             App.cbx_Estado.getValue(),App.cbx_Estabilidad.getValue(),App.txt_Precondicion.getValue(),App.txt_Postcondicion.getValue(),
                            App.txt_Frecuencia.getValue()],
                            [App.grilla_autores.getRowsValues(),App.grilla_fuentes.getRowsValues(),App.grilla_objetivos.getRowsValues(),
                            App.grilla_requisitos.getRowsValues(),App.grilla_secuencia.getRowsValues(),App.grilla_excepciones.getRowsValues(),
                            App.grilla_rendimiento.getRowsValues()])" />
                   
                              </Listeners>
                    </ext:Button>



                </Buttons>
            </ext:FormPanel>
        </Items>
    </ext:Viewport>   
</body>
</html>
