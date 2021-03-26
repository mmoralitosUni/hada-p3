﻿<%@ Page Title="Página de usuarios" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="usuWeb.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function showLabel() {
            document.getElementById("Label1").style.display = 'inherit';
        }
    </script>
    <p>
        <h1 style="margin-bottom: -2%;">Página de usuarios</h1></p>
    <p>
        <asp:Label ID="Label1" runat="server" Text="" style="display: none;"></asp:Label></p>
    <p>
        NIF:  <asp:TextBox ID="TextBox1" runat="server" Width="20%" BorderStyle="Solid" BorderWidth ="1" BorderColor="Gray"></asp:TextBox></p>
    <p>
        Nombre:  <asp:TextBox ID="TextBox2" runat="server" Width="30%" BorderStyle="Solid" BorderWidth ="1" BorderColor="Gray"></asp:TextBox></p>
    <p>
        Edad:  <asp:TextBox ID="TextBox3" runat="server" Width="10%" BorderStyle="Solid" BorderWidth ="1" BorderColor="Gray"></asp:TextBox></p>
    <p>
        <asp:Button ID="Button1" runat="server" Text="Leer" OnClick="Button1_Click" />
        <asp:Button ID="Button2" runat="server" Text="Leer Primero" OnClick="Button2_Click" />
        <asp:Button ID="Button3" runat="server" Text="Leer Anterior" OnClick="Button3_Click" />
        <asp:Button ID="Button4" runat="server" Text="Leer Siguiente" OnClick="Button4_Click" />
        <asp:Button ID="Button5" runat="server" Text="Crear" OnClick="Button5_Click" />
        <asp:Button ID="Button6" runat="server" Text="Actualizar" OnClick="Button6_Click" />
        <asp:Button ID="Button7" runat="server" Text="Borrar" OnClick="Button7_Click" />
    </p>
</asp:Content>
