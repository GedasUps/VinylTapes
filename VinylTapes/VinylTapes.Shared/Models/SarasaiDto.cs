using System.Collections.Generic;

namespace VinylTapes.Shared.Models
{
    public class SarasaiDto
    {
        public int Id { get; set; }
        public int Tipas { get; set; }
        public string? TipasName { get; set; }
        public int? FkMainai { get; set; }
        public string FkNaudotojai { get; set; } = null!;
        public List<PloksteleDto> Ploksteles { get; set; } = new();
    }
}
