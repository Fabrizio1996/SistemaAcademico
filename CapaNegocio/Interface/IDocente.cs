using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaEntidad; //Llamar al mepeado Objeto relacional
using System.Data; // Llama a los buffer de memoria: Tablas con registros

namespace CapaNegocio.Interface
{
    interface IDocente
    {
        // Declarar los metodos de la Clase Alumno

        DataTable Listar();

        bool Agregar(Docente docente);

        bool Eliminar(string codDocente);

        bool Actualizar(Docente docente);

        DataTable Buscar(string codDocente);
    }
}
