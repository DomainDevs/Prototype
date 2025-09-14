using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Properties.Queries
{
    // Consulta para obtener todas las propiedades
    public record GetAllPropertiesQuery();

    // Consulta para obtener una propiedad por ID
    public record GetPropertyQuery(int Id);
}
