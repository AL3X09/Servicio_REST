using System;
using System.Collections.Generic;

namespace Servicio_REST.EFModels
{
    public partial class Soat
    {
        public long IdS { get; set; }
        public DateOnly FechaInicio { get; set; }
        public DateOnly FechaFin { get; set; }
        public DateOnly FechaVemciIemtpActual { get; set; }
        public string PlacaAutomotor { get; set; } = null!;
        public long FkCiudad { get; set; }
        public long FkUsuario { get; set; }

        public virtual Ciudad FkCiudadNavigation { get; set; } = null!;
        public virtual Usuario FkUsuarioNavigation { get; set; } = null!;
    }
}
