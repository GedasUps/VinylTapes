using System;
using System.Collections.Generic;

namespace VinylTapes.Models;

public partial class Mainai
{
    public DateOnly MainuData { get; set; }

    public int Statusas { get; set; }

    public int Id { get; set; }

    public string FkNaudotojai { get; set; } = null!;

    public string FkNaudotojai1 { get; set; } = null!;

    public virtual Naudotojai FkNaudotojai1Navigation { get; set; } = null!;

    public virtual Naudotojai FkNaudotojaiNavigation { get; set; } = null!;

    public virtual ICollection<Sarasai> Sarasais { get; set; } = new List<Sarasai>();

    public virtual Statusai StatusasNavigation { get; set; } = null!;
}
