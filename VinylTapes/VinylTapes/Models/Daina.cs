using System;
using System.Collections.Generic;

namespace VinylTapes.Models;

public partial class Daina
{
    public string Pavadinimas { get; set; } = null!;

    public string Trukme { get; set; } = null!;

    public string Pozicija { get; set; } = null!;

    public int Id { get; set; }

    public int FkPlokstele { get; set; }

    public virtual Plokstele FkPloksteleNavigation { get; set; } = null!;

    public override string ToString()
    {
        return $"{Pavadinimas} - {Trukme};  {Pozicija}";
    }
}
