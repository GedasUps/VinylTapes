using System;
using System.Collections.Generic;

namespace VinylTapes.Models;

public partial class Role
{
    public int IdRoles { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Naudotojai> Naudotojais { get; set; } = new List<Naudotojai>();
}
