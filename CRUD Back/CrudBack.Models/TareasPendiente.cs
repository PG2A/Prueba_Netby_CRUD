using System;
using System.Collections.Generic;

namespace CrudBack.Models;

public partial class TareasPendiente
{
    public int Id { get; set; }

    public string? Titulo { get; set; }

    public string? Descripcion { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public DateTime? FechaVencimiento { get; set; }

    public bool? Completada { get; set; }
}
