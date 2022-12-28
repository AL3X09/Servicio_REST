using System;
using System.Collections.Generic;

namespace Servicio_REST.EFModels
{
    public partial class Ciudad
    {
        public Ciudad()
        {
            Soats = new HashSet<Soat>();
        }

        public long IdC { get; set; }
        public string Nombre { get; set; } = null!;

        public virtual ICollection<Soat> Soats { get; set; }
    }
}
