<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dibujo.aspx.cs" Inherits="Proyecto_Ing_Software.Dibujo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <style>
        .cursor-dragme {
            cursor: move;
        }
    </style>
    <style type="text/css">      
	    #unlicensed{
		    display: none !important;
	    }
    </style>

</head>
<body>
    <ext:ResourceManager ID="ResourceManager1" runat="server" />
  <ext:Viewport ID="Viewport1" runat="server" AutoScroll="true">
        <LayoutConfig>
            <ext:VBoxLayoutConfig Align="Center" Pack="Center" />
        </LayoutConfig>
      <Items>
          <ext:DrawComponent ID="Draw1" runat="server" Region="Center" ViewBox="false" StyleSpec="background:white;" Width="800"
            Height="600"  Border="true" >
            <FloatingConfig Shadow="None" />
           
          <Items>
        <%--  <ext:Sprite Type="Rect"   Width="30" Height="30" Fill="green" X="100" Y="100"  Draggable="true" />--%>
     <%--  <ext:Sprite Type="Arrow"  X="20" Y="20"  Fill="green"   Width="30" Height="30" />--%>
              <ext:Sprite Type="Image" Src='/Diagramas/actor.png' Width="50" Height="100" X="250" Y="250" Draggable="true" />
              <ext:Sprite Type="Image" Src='/Diagramas/actor.png' Width="50" Height="100" X="100" Y="100" Draggable="true" />
              <ext:Sprite Type="Image" Src='/Diagramas/caso_de_uso.png' Width="180" Height="70" X="320" Y="200" Draggable="true" />
              <ext:Sprite Type="Text" Text="Jugador" X="200" Y="340" Draggable="false" />
              <ext:Sprite Type="Text" Text="Sistema" X="200" Y="200" Draggable="false" />
              <ext:Sprite Type="Text" Text="Registrarse en la página" X="350" Y="230" Draggable="false" />
              <ext:Sprite Type="Line" Size="5" X="100" Y="200"  Draw="green" />
             
          </Items>
          </ext:DrawComponent>
      </Items>
  </ext:Viewport>
</body>
</html>
