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
    public partial class frmAlumno : System.Web.UI.Page
    {
        private void Listar()
        {
            AlumnoBL alumnoBL = new AlumnoBL();
            gvAlumno.DataSource = alumnoBL.Listar();
            gvAlumno.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                Listar();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Alumno alumno = new Alumno();
            alumno.CodAlumno = txtCodAlumno.Text.Trim();
            alumno.APaterno = txtAPaterno.Text.Trim();
            alumno.AMaterno = txtAMaterno.Text.Trim();
            alumno.Nombres = txtNombres.Text.Trim();
            alumno.CodUsuario = txtCodUsuario.Text.Trim();
            alumno.Contrasena = txtContrasena.Text.Trim();
            alumno.CodEscuela = txtCodEscuela.Text.Trim();

            AlumnoBL alumnoBL = new AlumnoBL();
            if (alumnoBL.Agregar(alumno))
                Listar();
            lblMensaje.Text = alumnoBL.Mensaje;
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            string codAlumno = txtCodAlumno.Text.Trim(); // Suponiendo que el código del alumno se captura en txtCodAlumno

            AlumnoBL alumnoBL = new AlumnoBL();
            if (alumnoBL.Eliminar(codAlumno))
            {
                Listar();
                lblMensaje.Text = "Alumno eliminado correctamente.";
            }
            else
            {
                lblMensaje.Text = "Error al eliminar el alumno.";
            }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            Alumno alumno = new Alumno();
            alumno.CodAlumno = txtCodAlumno.Text.Trim();
            alumno.APaterno = txtAPaterno.Text.Trim();
            alumno.AMaterno = txtAMaterno.Text.Trim();
            alumno.Nombres = txtNombres.Text.Trim();
            alumno.CodUsuario = txtCodUsuario.Text.Trim();
            alumno.Contrasena = txtContrasena.Text.Trim();
            alumno.CodEscuela = txtCodEscuela.Text.Trim();

            // Lógica para actualizar al alumno en la base de datos
            AlumnoBL alumnoBL = new AlumnoBL();
            if (alumnoBL.Actualizar(alumno))
            {
                Listar();
                lblMensaje.Text = "Alumno actualizado correctamente.";
            }
            else
            {
                lblMensaje.Text = "Error al actualizar el alumno.";
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string codAlumno = txtBuscar.Text.Trim();

                if (!string.IsNullOrEmpty(codAlumno))
                {
                    AlumnoBL alumnoBL = new AlumnoBL();
                    DataTable dt = alumnoBL.Buscar(codAlumno);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0];
                        string mensaje = $"Código encontrado: {row["CodAlumno"]}<br/> ApellidoPaterno: {row["APaterno"]}<br/> ApellidoMaterno: {row["AMaterno"]}<br/> Nombre: {row["Nombres"]}<br/> CodigoUsuario: {row["CodUsuario"]}<br/> CodigoEscuela: {row["CodEscuela"]}<br/>";
                        lblMensaje.Text = mensaje;
                    }
                    else
                    {
                        lblMensaje.Text = "No se encontraron alumnos con el código proporcionado.";
                    }
                }
                else
                {
                    lblMensaje.Text = "Por favor ingrese un código de alumno para buscar.";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al buscar alumno: " + ex.Message;
            }
        }
    }
}