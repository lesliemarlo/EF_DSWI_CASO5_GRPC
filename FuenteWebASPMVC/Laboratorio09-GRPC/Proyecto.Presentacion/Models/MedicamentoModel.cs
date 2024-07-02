using System.ComponentModel;

namespace Proyecto.Presentacion.Models
{
    public class MedicamentoModel
    {
        [DisplayName("CÓDIGO")]
        public int ide_med { get; set; }

        [DisplayName("NOMBRE")]
        public string? nom_med { get; set; }

        [DisplayName("DESCRIPCIÓN")]
        public string? des_med { get; set; }

        [DisplayName("RECETA")]
        public string? rec_med { get; set; }

        [DisplayName("VÍA")]
        public string? via_med { get; set; }

        [DisplayName("COSTO")]
        public double cos_med { get; set; }

        [DisplayName("PAÍS")]
        public string? nom_pai { get; set; }

    }
}
