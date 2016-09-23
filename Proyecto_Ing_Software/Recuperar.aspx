<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Recuperar.aspx.cs" Inherits="Proyecto_Ing_Software.Recuperar" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

   <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>RQSoft - Software para elicitación de requisitos</title>
    <link href='http://fonts.googleapis.com/css?family=Varela+Round' rel='stylesheet' type='text/css'>
    <link href="Styles/css/bootstrap.min.css" rel="stylesheet">
    <link href="http://netdna.bootstrapcdn.com/font-awesome/4.1.0/css/font-awesome.min.css" rel="stylesheet">
    <link href="Styles/css/flexslider.css" rel="stylesheet">
    <link href="Styles/css/styles.css" rel="stylesheet">
    <link href="Styles/css/queries.css" rel="stylesheet">
    <link href="Styles/css/animate.css" rel="stylesheet">
    <style>
        .but{
            cursor: pointer;
         background: #1161ee;
             border: 2px;
         padding: 15px 20px;
       border-radius: 25px;
      background: rgba(255,255,255,.1);
        }
    </style>

     <style type="text/css">      
	    #unlicensed{
		    display: none !important;
	    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
  
        <header id="home" style="height:150px;">
       
        <section class="hero" id="hero">
            <div class="container">
                <div class="row">
                    </br>
                    </br>
                    </br>
                    </br>

                </div>
               
            </div>
        </section>
    </header>
                <ext:ResourceManager ID="ResourceManager1" runat="server" />

    <section class="intro text-center " id="intro" style="height:400px;" runat="server">
        <div class="container" runat="server">
            <div class="row" runat="server">
                <div class="col-md-8 col-md-offset-2 wp1">
                    <h1 class="arrow">¿Olvidaste tu contraseña?</h1>
                    <p>Ingresa en el campo el correo con el que te has registrado en la plataforma</p>
                    </br>
                    <div class="group" runat="server">
						<label for="pass">Correo electrónico:</label>
                        <asp:TextBox ID="TextBox1"  runat="server" CssClass="input" style="width:190px;"></asp:TextBox>

					</div>

                    </br>
                   <div class="group">
                       <asp:Button ID="Button1" runat="server" Text="Enviar" CssClass="but" OnClick="Button1_Click" cli />
                   </div>

                </div>
            </div>
        </div>
    </section>
  
    <!-- jQuery (necessary for Bootstrap's JavaScript plugins) -->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>
    <!-- Include all compiled plugins (below), or include individual files as needed -->
    <script src="Scripts/js/waypoints.min.js"></script>
    <script src="Scripts/js/bootstrap.min.js"></script>
    <script src="Scripts/js/scripts.js"></script>
    <script src="Scripts/js/jquery.flexslider.js"></script>
    <script src="Scripts/js/modernizr.js"></script>





    </form>
</body>
</html>
