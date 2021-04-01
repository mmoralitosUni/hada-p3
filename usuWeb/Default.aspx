<%@ Page Title="Página de usuarios" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="usuWeb.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function showLabel() {
            document.getElementById("Label1").style.display = 'inherit';
        }
    </script>
    <style>
        .button {
            margin-left:0.5%;
            margin-right:0.5%;
        }
    </style>
    <h1 style="margin-bottom: 2%;">Página de usuarios</h1>
    <p>
        NIF:  <asp:TextBox ID="nifTB" runat="server" Width="20%" BorderStyle="Solid" BorderWidth ="1" BorderColor="Gray" ></asp:TextBox></p>
    <p>
        Nombre:  <asp:TextBox ID="nombreTB" runat="server" Width="30%" BorderStyle="Solid" BorderWidth ="1" BorderColor="Gray"></asp:TextBox></p>
    <p>
        Edad:  <asp:TextBox ID="edadTB" runat="server" Width="10%" BorderStyle="Solid" BorderWidth ="1" BorderColor="Gray"></asp:TextBox></p>
    <div style="display:flex; justify-content:flex-start;">
        <asp:Button ID="ButtonLeer" CssClass="button" runat="server" Text="Leer" OnClick="ButtonLeer_Click" /> 
        <asp:Button ID="ButtonLeerPrimero" CssClass="button" runat="server" Text="Leer Primero" OnClick="ButtonLeerPrimero_Click" />
        <asp:Button ID="ButtonLeerAnterior" CssClass="button" runat="server" Text="Leer Anterior" OnClick="ButtonLeerAnterior_Click" />
        <asp:Button ID="ButtonLeerSiguiente" CssClass="button" runat="server" Text="Leer Siguiente" OnClick="ButtonLeerSiguiente_Click" />
        <asp:Button ID="ButtonCrear" CssClass="button" runat="server" Text="Crear" OnClick="ButtonCrear_Click" />
        <asp:Button ID="ButtonActualizar" CssClass="button" runat="server" Text="Actualizar" OnClick="ButtonActualizar_Click" />
        <asp:Button ID="ButtonBorrar" CssClass="button" runat="server" Text="Borrar" OnClick="ButtonBorrar_Click" />
    </div>
    <p>
        <asp:Label ID="Messages" runat="server" Text=""></asp:Label></p>
</asp:Content>
