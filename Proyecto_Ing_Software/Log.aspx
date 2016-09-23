<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Log.aspx.cs" Inherits="Proyecto_Ing_Software.Log"  %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext"  %>


<!DOCTYPE html>
<html>

<head>
	<meta charset="UTF-8">
	<title>Iniciar sesión - RQSoft</title>
    <link rel='stylesheet prefetch' href='http://fonts.googleapis.com/css?family=Open+Sans:600'>

    <style type="text/css">      
	    #unlicensed{
		    display: none !important;
	    }
    </style>

      <link href="Styles/Loggin/css/bootstrap.css" rel="stylesheet">
    <!--external css-->
    <link href="Styles/Loggin/font-awesome/css/font-awesome.css" rel="stylesheet" />
        
    <!-- Custom styles for this template -->
    <link href="Styles/Loggin/css/style.css" rel="stylesheet">
    <link href="Styles/Loggin/css/style-responsive.css" rel="stylesheet">

</head>

<body>
             

       <ext:ResourceManager ID="ResourceManager1" runat="server" />


    
	  <div id="login-page">
	  	<div class="container">
	  	
		      <form class="form-login"  runat="server">
		        <h2 class="form-login-heading">Iniciar sesión</h2>
		        <div class="login-wrap">
                       <label for="txt_Usuario" class="">Usuario:</label>
                       <ext:TextField ID="txt_Usuario" runat="server"   AllowBlank="false" FieldCls="form-control" Size="100" Height="40" InvalidCls="invalid" AutoFocus="true" />
					
		            <br>
                    <label for="password" class="" runat="server">Contraseña:</label>
                        <ext:TextField ID="txt_Password" runat="server"  InputType="Password" EnableKeyEvents="true" AllowBlank="false" FieldCls="form-control" Size="100" Height="40" InvalidCls="invalid">
                            <Listeners>
                                <KeyPress Handler="if(e.getKey()==13){
                                    App.direct.IniciarSession()
                                    }"/>
                            </Listeners>

                        </ext:TextField>


		            <label class="checkbox">
		                <span class="pull-right">
		                    <a data-toggle="modal" href="#myModal"> ¿Olvidaste tu contraseña?</a>
		
		                </span>
		            </label>

                     <Buttons>          
                            <ext:Button ID="btn_aceptar" runat="server" Text="<span style='color:WHITE;'>INGRESAR </span>" Cls="btn btn-theme btn-block" AutoFocus="true"  >                              
                                    <Listeners>
                                        <Click Handler="if(#{txt_Password}.isValid() && #{txt_usuario}.isValid()){App.direct.IniciarSession();}
                                            else{ Ext.Msg.alert('Mensaje', 'Complete el formularo') }" />
                                    </Listeners>
                                </ext:Button>
                            </Buttons>
		            <hr>
		            
		            <div class="login-social-link centered">
		               
		            </div>
		            <div class="registration">
		                <br/>
		                <a data-toggle="modal" href="#myModal1">
		                    Registrarme 
		                </a>
		            </div>
		
		        </div>
		
		          <!-- Modal -->
		          <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="myModal" class="modal fade" runat="server">
		              <div class="modal-dialog">
		                  <div class="modal-content">
		                      <div class="modal-header">
		                          <button type="button"  class="close" id="bo" data-dismiss="modal" aria-hidden="true" runat="server">&times;</button>
		                          <h4 class="modal-title"> ¿Olvidaste tu contraseña?</h4>
		                      </div>
		                      <div class="modal-body">
		                          <p>Ingrese el correo electronico con el que se ha registrado.</p>
		                          <label for="email" class="">Correo electrónico:</label>
                              <ext:TextField ID="TextField1" runat="server"   AllowBlank="false" FieldCls="form-control placeholder-no-fix" Size="100" Height="35" InvalidCls="invalid" Vtype="email" />
 
		                      </div>
		                      <div class="modal-footer">
                                   <Buttons>          
                                   <ext:Button ID="Button1" runat="server" Text="<span style='color:WHITE;'>ENVIAR</span>" Cls="btn btn-theme" Width="200"  >                              
                                    <Listeners>
                                        <Click Handler="if(#{TextField1}.isValid()){App.direct.correo();
                                            
                                            }
                                            else{ Ext.Msg.alert('Mensaje', 'ingrese su correo electronico') }" />
                                    </Listeners>
                                </ext:Button>
                            </Buttons>
		                      </div>
		                  </div>
		              </div>
		          </div>
		          <!-- modal -->
                  <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="myModal1" class="modal fade">
		              <div class="modal-dialog">
		                  <div class="modal-content">
		                      <div class="modal-header">
		                          <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
		                          <h4 class="modal-title">Formulario de registro</h4>
		                      </div>
		                      <div class="modal-body">
		                         <label for="nombre" class="">Nombres:</label>
                                 <ext:TextField ID="nombre" runat="server"   AllowBlank="false" FieldCls="form-control placeholder-no-fix" Size="100" Height="35" InvalidCls="invalid" MaskRe="/[ -.a-zA-Z, ñÑ]/" />
                                  <label for="apel" class="">Apellidos:</label>
                                <ext:TextField ID="apel" runat="server"   AllowBlank="false" FieldCls="form-control placeholder-no-fix" Size="100" Height="35" InvalidCls="invalid" MaskRe="/[ -.a-zA-Z, ñÑ]/" />
                                  <label for="email" class="">Correo electrónico:</label>
                              <ext:TextField ID="email" runat="server"   AllowBlank="false" FieldCls="form-control placeholder-no-fix" Size="100" Height="35" InvalidCls="invalid" Vtype="email" />
 
                                  <label for="or" >Organización/empresa:</label>
                       <ext:TextField ID="or" runat="server"   AllowBlank="false" FieldCls="form-control placeholder-no-fix" Size="100" Height="35" InvalidCls="invalid" />

                                  <label for="con" >Contraseña:</label>

                          <ext:TextField ID="con" runat="server"  InputType="Password" FieldCls="form-control placeholder-no-fix" Size="100" Height="35" InvalidCls="invalid"
                                AllowBlank="false">
                                <Listeners>
                                    <ValidityChange Handler="this.next().validate();" />
                                    <Blur Handler="this.next().validate();" />
                                </Listeners>
                            </ext:TextField>
						<label for="conr" >Repetir contraseña:</label>

                        <ext:TextField ID="txt_Password2" runat="server" 
                                InputType="Password" AllowBlank="false" MsgTarget="Side" Vtype="password" FieldCls="form-control placeholder-no-fix" Size="100" Height="35" InvalidCls="invalid">
                                <CustomConfig>
                                    <ext:ConfigItem Name="initialPassField" Value="con" Mode="Value" />
                                </CustomConfig>
                            </ext:TextField>

   
                                  </div>


		                      <div class="modal-footer">
                               <ext:Button ID="Button2" runat="server" Text="<span style='color:WHITE;'>REGISTRATE</span>" Cls="btn btn-theme" >                              
                                    <Listeners>
                                        <Click Handler="if(#{nombre}.isValid() && #{apel}.isValid() && #{email}.isValid() && #{or}.isValid() && #{con}.isValid()  && #{txt_Password2}.isValid() ){App.direct.registrar();}
                                            else{ Ext.Msg.alert('Mensaje', 'Complete el formularo') }" />
                                    </Listeners>
                                </ext:Button>
		                      </div>
		                  </div>
		              </div>
		          </div>

		
		      </form>	  	
	  	
	  	</div>
	  </div>

    <!-- js placed at the end of the document so the pages load faster -->
    <script src="Scripts/js/jquery.min.js"></script>
    <script src="Scripts/js/bootstrap.min.js"></script>

    <!--BACKSTRETCH-->
    <!-- You can use an image of whatever size. This script will stretch to fit in any screen size.-->
    <script type="text/javascript" src="Scripts/js/jquery.backstretch.min.js"></script>
    <script>
        $.backstretch("Styles/img/FondoPrincipal4.jpg", {speed: 500});
    </script>












               </body>
		</html>
