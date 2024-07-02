using System.ComponentModel;

namespace Proyecto.Presentacion.Models
{
    public class PaisModel
    {
        [DisplayName("CÓDIGO")]
        public int ide_pai { get; set; }

        [DisplayName("NOMBRE")]
        public string? nom_pai { get; set; }
    }
}
