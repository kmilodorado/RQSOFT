<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="Proyecto_Ing_Software.Registro" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">      
	    #unlicensed{
		    display: none !important;
	    }
    </style>
     <script>
         var showResult = function (btn) {
             // Ext.Msg.notify("Button Click", "You clicked the " + btn + " button");

             parentAutoLoadControl.hide();
         };

       
    </script> 
</head>
<body>
     <ext:ResourceManager ID="ResourceManager1" runat="server" />
  <ext:Viewport ID="Viewport1" runat="server" AutoScroll="true">
        <LayoutConfig>
            <ext:VBoxLayoutConfig Align="Center" Pack="Center" />
        </LayoutConfig>
        <Items>
            <ext:FormPanel ID="FormPanel1"
                runat="server"
                Width="420"
                Height="290"
                Frame="true"              
                AutoScroll="true">
                <FieldDefaults LabelAlign="Right" LabelWidth="115" MsgTarget="Side" />
                <Items>
                    <ext:FieldSet ID="FieldSet1" runat="server" Title="Información General" DefaultWidth="330">
                        <Items>
                            <ext:TextField ID="txt_Nombre" runat="server" FieldLabel="Nombre" AllowBlank="false" MaskRe="/[ -.a-zA-Z, ñÑ]/" />
                            <ext:TextField ID="txt_Apellidos" runat="server" FieldLabel="Apellido" AllowBlank="false" MaskRe="/[ -.a-zA-Z, ñÑ]/" />
                            <ext:TextField ID="txt_Login" runat="server" FieldLabel="Usuario" AllowBlank="false" />
                            <ext:TextField ID="txt_Password" runat="server" FieldLabel="Contraseña" InputType="Password"
                                AllowBlank="false">
                                <Listeners>
                                    <ValidityChange Handler="this.next().validate();" />
                                    <Blur Handler="this.next().validate();" />
                                </Listeners>
                            </ext:TextField>
                            <ext:TextField ID="txt_Password2" runat="server" FieldLabel="Repetir Contraseña"
                                InputType="Password" AllowBlank="false" MsgTarget="Side" Vtype="password">
                                <CustomConfig>
                                    <ext:ConfigItem Name="initialPassField" Value="txt_Password" Mode="Value" />
                                </CustomConfig>
                            </ext:TextField>
                            <ext:TextField ID="txt_email" runat="server" FieldLabel="Email" Vtype="email" AllowBlank="false" />
                            <ext:TextField ID="txt_org" runat="server"  FieldLabel="Organización" AllowBlank="false" MaxLength="99" EnforceMaxLength="true" />
                        </Items>
                    </ext:FieldSet>                        
                </Items>
                <Buttons>
                    <ext:Button ID="Button1" runat="server" Text="Aceptar" Disabled="true" FormBind="true" Icon="Accept">
                         <DirectEvents>
                            <Click OnEvent="registrar">
                            <EventMask ShowMask="true" Msg="Registrando..." />
                            </Click>
                        </DirectEvents>
                    
                    </ext:Button>
                </Buttons>
            </ext:FormPanel>
        </Items>
    </ext:Viewport>   
</body>
</html>
