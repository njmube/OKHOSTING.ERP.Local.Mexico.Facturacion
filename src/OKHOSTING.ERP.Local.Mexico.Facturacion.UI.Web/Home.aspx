<%@ Page Title="" Language="C#" MasterPageFile="~/Global.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="OKHOSTING.ERP.Local.Mexico.Facturacion.UI.Web.Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>
        Descarga todas tus facturas del SAT
    </h1>

    <p>
        Escribe los datos con los que entras al SAT (RFC y contrase&ntilde;) y nosotros descargaremos por tí todas las facturas que cumplan el criterio, después te las mandaremos a tu correo electr&oacute;nico 
		y despu&eacute;s borraremos todos los archivos de nuestro servidor. No conservamos copia ni de tus facturas ni de tu contraseña, mas que por algunos minutos para hacer la descarga, despu&eacute;s todo se borra
		para proteger tu privacidad.
    </p>
	<p>
		Este es un servicio gratuito y patrocinado por <a href="http://okhosting.com">OK HOSTING</a>. 
	</p>
	<p>
		<a href="https://github.com/okhosting/OKHOSTING.ERP.Local.Mexico.Facturacion.UI.Web">El c&oacute;digo fuente es open source y lo puedes ver aqu&iacute;.</a>
	</p>
	<table>
		<tr>
			<td>Desde</td>
			<td>
				<asp:Calendar ID="cldDesde" runat="server"></asp:Calendar>
			</td>
		</tr>
		<tr>
			<td>Hasta</td>
			<td>
				<asp:Calendar ID="cldHasta" runat="server"></asp:Calendar>
			</td>
		</tr>
		<tr>
			<td>Tipo de comprobante</td>
			<td>
				<asp:DropDownList ID="ddlBusqueda" runat="server">
					<asp:ListItem Text="Emitidas" Value="Emitidas"></asp:ListItem>
					<asp:ListItem Text="Recibidas" Value="Recibidas"></asp:ListItem>
				</asp:DropDownList>
			</td>
		</tr>
		<tr>
			<td>RFC</td>
				<td>
				<asp:TextBox ID="txtRFC" runat="server"></asp:TextBox>
					<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtRFC" ErrorMessage="Campo requerido"></asp:RequiredFieldValidator>
			</td>
		</tr>
		<tr>
			<td>Contraseña</td>
			<td>
				<asp:TextBox ID="txtContrasena" runat="server" TextMode="Password"></asp:TextBox>
				<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtContrasena" ErrorMessage="Campo requerido"></asp:RequiredFieldValidator>
			</td>
		</tr>
		<tr>
			<td>Email</td>
			<td>
				<asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
				<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtEmail" ErrorMessage="Campo requerido"></asp:RequiredFieldValidator>
				<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail" ErrorMessage="Email inválido" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
			</td>
		</tr>
		<tr>
			<td colspan="2">
				&nbsp;</td>
		</tr>
		<tr>
			<td colspan="2">
				<asp:Button ID="Button1" runat="server" Text="Descargar" OnClick="cmdSiguiente_Click" />
			</td>
		</tr>
	</table>
</asp:Content>