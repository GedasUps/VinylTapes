using System;
using System.Collections.Generic;

namespace VinylTapes.Models;

public partial class Naudotojai
{
    public string Vardas { get; set; } = null!;

    public string ElPastas { get; set; } = null!;

    public string Slaptazodis { get; set; } = null!;

    public string? DisgocsZetonas { get; set; }

    public string? DiscogsPaslaptis { get; set; }

    public int Role { get; set; }

    public virtual ICollection<Mainai> MainaiFkNaudotojai1Navigations { get; set; } = new List<Mainai>();

    public virtual ICollection<Mainai> MainaiFkNaudotojaiNavigations { get; set; } = new List<Mainai>();

    public virtual Role RoleNavigation { get; set; } = null!;

    public virtual ICollection<Sarasai> Sarasais { get; set; } = new List<Sarasai>();
}
