using System;
using System.Collections.Generic;

namespace VinylTapes.Models;

public partial class Statusai
{
    public int IdStatusai { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Mainai> Mainais { get; set; } = new List<Mainai>();
}
