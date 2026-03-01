using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;

namespace VinylTapes.Models;

public partial class Buseno
{
    public int IdBusenos { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Plokstele> Ploksteles { get; set; } = new List<Plokstele>();
    public Buseno() {
        IdBusenos = 1;
    }
    public Buseno(int id, string name)
    {
        IdBusenos = id;
        Name = name;
    }
}
