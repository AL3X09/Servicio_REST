using System;
using System.Collections.Generic;

namespace Servicio_REST.EFModels
{
    public partial class Usuario
    {
        public Usuario()
        {
            Soats = new HashSet<Soat>();
        }

        public long IdU { get; set; }
        public string Nombres { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public long NumIdentificacion { get; set; }

        public virtual ICollection<Soat> Soats { get; set; }
    }
}
