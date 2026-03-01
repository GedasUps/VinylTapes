using System;
using System.Collections.Generic;
using Pgvector;

namespace VinylTapes.Models;

public partial class Plokstele
{
    public string Atlikejas { get; set; } = null!;

    public string Albumas { get; set; } = null!;

    public string IrasuKompanija { get; set; } = null!;

    public string Zanras { get; set; } = null!;

    public string Stilius { get; set; } = null!;

    public int Discogsid { get; set; }

    public string Formatas { get; set; } = null!;

    public short Metai { get; set; }

    public string? Kontekstas { get; set; }

    public Vector? Vektorius { get; set; }

    public string Matrica { get; set; } = null!;

    public int? BruksninisKodas { get; set; }

    public int Busena { get; set; }

    public decimal? IsigijimoKaina { get; set; }

    public virtual Buseno BusenaNavigation { get; set; } 

    public virtual ICollection<Daina> Dainas { get; set; } = new List<Daina>();

    public virtual ICollection<Sarasai> FkSarasais { get; set; } = new List<Sarasai>();

    public override string ToString()
    {
        string rez="";
        rez += $"{Atlikejas} {Albumas} {Zanras} {Stilius} {Metai} {Kontekstas} {Matrica} {Busena}";
        foreach (Daina d in Dainas)
            rez += $"{d.ToString()} \n";
        return rez!=null?rez:"bbd";
    }
}
