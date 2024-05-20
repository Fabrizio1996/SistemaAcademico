using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CapaEntidad;
using CapaNegocio;


namespace CapaPresentacion
{
    public partial class frmDocente : System.Web.UI.Page
    {
        private void Listar()
        {
            DocenteBL docenteBL = new DocenteBL();
            gvDocente.DataSource = docenteBL.Listar();
            gvDocente.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                Listar();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Docente docente = new Docente();
            docente.CodDocente = txtCodDocente.Text.Trim();
            docente.APaterno = txtAPaterno.Text.Trim();
            docente.AMaterno = txtAMaterno.Text.Trim();
            docente.Nombres = txtNombres.Text.Trim();
            docente.CodUsuario = txtCodUsuario.Text.Trim();
            docente.Contrasena = txtContrasena.Text.Trim();

            DocenteBL docenteBL = new DocenteBL();
            if (docenteBL.Agregar(docente))
                Listar();
            lblMensaje.Text = docenteBL.Mensaje;
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            string codAlumno = txtCodDocente.Text.Trim(); // Suponiendo que el código del alumno se captura en txtCodAlumno

            DocenteBL docenteBL = new DocenteBL();
            if (docenteBL.Eliminar(codAlumno))
            {
                Listar();
                lblMensaje.Text = "Docente eliminado correctamente.";
            }
            else
            {
                lblMensaje.Text = "Error al eliminar el docente.";
            }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            Docente docente = new Docente();
            docente.CodDocente = txtCodDocente.Text.Trim();
            docente.APaterno = txtAPaterno.Text.Trim();
            docente.AMaterno = txtAMaterno.Text.Trim();
            docente.Nombres = txtNombres.Text.Trim();
            docente.CodUsuario = txtCodUsuario.Text.Trim();
            docente.Contrasena = txtContrasena.Text.Trim();

            // Lógica para actualizar al alumno en la base de datos
            DocenteBL docenteBL = new DocenteBL();
            if (docenteBL.Actualizar(docente))
            {
                Listar();
                lblMensaje.Text = "Docente actualizado correctamente.";
            }
            else
            {
                lblMensaje.Text = "Error al actualizar el docente.";
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string codDocente = txtBuscar.Text.Trim();

                if (!string.IsNullOrEmpty(codDocente))
                {
                    DocenteBL docenteBL = new DocenteBL();
                    DataTable dt = docenteBL.Buscar(codDocente);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0];
                        string mensaje = $"Código encontrado: {row["CodDocente"]}<br/> ApellidoPaterno: {row["APaterno"]}<br/> ApellidoMaterno: {row["AMaterno"]}<br/> Nombre: {row["Nombres"]}<br/> CodigoUsuario: {row["CodUsuario"]}<br/>";
                        lblMensaje.Text = mensaje;
                    }
                    else
                    {
                        lblMensaje.Text = "No se encontraron docentes con el código proporcionado.";
                    }
                }
                else
                {
                    lblMensaje.Text = "Por favor ingrese un código de docente para buscar.";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al buscar docente: " + ex.Message;
            }
        }
    }
}