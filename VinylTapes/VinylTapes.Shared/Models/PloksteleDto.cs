using System;
using System.Collections.Generic;
using System.Text;

namespace VinylTapes.Shared.Models
{
    public class PloksteleDto
    {
        public int? DiscogsId { get; set; }
        public string Atlikejas { get; set; } = null!;
        public string Albumas { get; set; } = null!;
        public short Metai { get; set; }
        public string? IrasuKompanija { get; set; }
        public string? Formatas { get; set; }

        // Svarbu: naršyklei vektorius yra tiesiog skaičių masyvas
        public List<float>? Vektorius { get; set; }

        public List<DainaDto> Dainos { get; set; } = new();

        public string? ImageUrl { get; set; }
        public string? busena { get; set; }
        public float IsigijimoKaina { get; set; }

    }
    public class DainaDto
    {
        public string Pavadinimas { get; set; } = null!;
        public string Pozicija { get; set; } = null!;
        public string Trukme { get; set; } = null!;
    }
}
