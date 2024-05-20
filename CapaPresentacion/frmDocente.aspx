<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmDocente.aspx.cs" Inherits="CapaPresentacion.frmDocente" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <p>Mantenimiento de la tabla Docente</p>
    <p>
        CodDocente <asp:TextBox runat="server" ID="txtCodDocente"></asp:TextBox>
    </p>
    <p>
        APaterno <asp:TextBox runat="server" ID="txtAPaterno"></asp:TextBox>
    </p>
    <p>
        AMaterno <asp:TextBox runat="server" ID="txtAMaterno"></asp:TextBox>
    </p>
    <p>
        Nombres <asp:TextBox runat="server" ID="txtNombres"></asp:TextBox>
    </p>
    <p>
        CodUsuario <asp:TextBox runat="server" ID="txtCodUsuario"></asp:TextBox>
    </p>
    <p>
        Contraseña <asp:TextBox runat="server" ID="txtContrasena" TextMode="Password"></asp:TextBox>
    </p>

    <p>
        <asp:Button Text="Agregar" runat="server" ID="btnAgregar" CssClass="btn btn-primary" OnClick="btnAgregar_Click" />
        <asp:Button Text="Eliminar" runat="server" ID="btnEliminar" CssClass="btn btn-warning" OnClick="btnEliminar_Click" OnClientClick="return confirmDelete();" />
        <asp:Button Text="Actualizar" runat="server" ID="btnActualizar" CssClass="btn btn-success" OnClick="btnActualizar_Click" />
    </p>

    <p>
        <asp:TextBox runat="server" ID="txtBuscar"></asp:TextBox>
        <asp:Button Text="Buscar" runat="server" ID="btnBuscar" CssClass="btn btn-info" OnClick="btnBuscar_Click" />
    </p>

            <script type="text/javascript">
            function confirmDelete() {
                return confirm('¿Está seguro que desea eliminar este docente?');
            }
            </script>

    <p>
        <asp:GridView runat="server" ID="gvDocente"></asp:GridView>
    </p>
    <p>
        <asp:Label Text="Mensaje" runat="server" ID="lblMensaje" />
    </p>
</asp:Content>
