using System;
using System.Collections.Generic;

namespace VinylTapes.Models;

public partial class SarasuTipai
{
    public int IdSarasuTipai { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Sarasai> Sarasais { get; set; } = new List<Sarasai>();
}
