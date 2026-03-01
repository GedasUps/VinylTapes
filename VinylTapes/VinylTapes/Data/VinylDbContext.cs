using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using VinylTapes.Models;

namespace VinylTapes.Data;

public partial class VinylDbContext : DbContext
{
    public VinylDbContext()
    {
    }

    public VinylDbContext(DbContextOptions<VinylDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Buseno> Busenos { get; set; }

    public virtual DbSet<Daina> Dainas { get; set; }

    public virtual DbSet<Mainai> Mainais { get; set; }

    public virtual DbSet<Naudotojai> Naudotojais { get; set; }

    public virtual DbSet<Plokstele> Ploksteles { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Sarasai> Sarasais { get; set; }

    public virtual DbSet<SarasuTipai> SarasuTipais { get; set; }

    public virtual DbSet<Statusai> Statusais { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=vinyl_collection;Username=postgres;Password=root", x => x.UseVector());

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("vector");

        modelBuilder.Entity<Buseno>(entity =>
        {
            entity.HasKey(e => e.IdBusenos).HasName("busenos_pkey");

            entity.ToTable("busenos");

            entity.Property(e => e.IdBusenos)
                .ValueGeneratedNever()
                .HasColumnName("id_busenos");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsFixedLength()
                .HasColumnName("name");
        });

        modelBuilder.Entity<Daina>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("daina_pkey");

            entity.ToTable("daina");

            entity.Property(e => e.Id)
                .UseIdentityByDefaultColumn()
                .HasColumnName("id");
            entity.Property(e => e.FkPlokstele).HasColumnName("fk_plokstele");
            entity.Property(e => e.Pavadinimas)
                .HasMaxLength(100)
                .HasColumnName("pavadinimas");
            entity.Property(e => e.Pozicija)
                .HasMaxLength(10)
                .HasColumnName("pozicija");
            entity.Property(e => e.Trukme)
                .HasMaxLength(10)
                .HasColumnName("trukme");

            entity.HasOne(d => d.FkPloksteleNavigation).WithMany(p => p.Dainas)
                .HasForeignKey(d => d.FkPlokstele)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("daina_fk_plokstele_fkey");
        });

        modelBuilder.Entity<Mainai>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("mainai_pkey");

            entity.ToTable("mainai");

            entity.Property(e => e.Id)
                .UseIdentityByDefaultColumn()
                .HasColumnName("id");
            entity.Property(e => e.FkNaudotojai)
                .HasMaxLength(255)
                .HasColumnName("fk_naudotojai");
            entity.Property(e => e.FkNaudotojai1)
                .HasMaxLength(255)
                .HasColumnName("fk_naudotojai1");
            entity.Property(e => e.MainuData).HasColumnName("mainu_data");
            entity.Property(e => e.Statusas).HasColumnName("statusas");

            entity.HasOne(d => d.FkNaudotojaiNavigation).WithMany(p => p.MainaiFkNaudotojaiNavigations)
                .HasForeignKey(d => d.FkNaudotojai)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("pardavejas");

            entity.HasOne(d => d.FkNaudotojai1Navigation).WithMany(p => p.MainaiFkNaudotojai1Navigations)
                .HasForeignKey(d => d.FkNaudotojai1)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("pirkejas");

            entity.HasOne(d => d.StatusasNavigation).WithMany(p => p.Mainais)
                .HasForeignKey(d => d.Statusas)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("mainai_statusas_fkey");
        });

        modelBuilder.Entity<Naudotojai>(entity =>
        {
            entity.HasKey(e => e.ElPastas).HasName("naudotojai_pkey");

            entity.ToTable("naudotojai");

            entity.Property(e => e.ElPastas)
                .HasMaxLength(255)
                .HasColumnName("el_pastas");
            entity.Property(e => e.DiscogsPaslaptis)
                .HasMaxLength(255)
                .HasColumnName("discogs_paslaptis");
            entity.Property(e => e.DisgocsZetonas)
                .HasMaxLength(255)
                .HasColumnName("disgocs_zetonas");
            entity.Property(e => e.Role).HasColumnName("role");
            entity.Property(e => e.Slaptazodis)
                .HasMaxLength(255)
                .HasColumnName("slaptazodis");
            entity.Property(e => e.Vardas)
                .HasMaxLength(255)
                .HasColumnName("vardas");

            entity.HasOne(d => d.RoleNavigation).WithMany(p => p.Naudotojais)
                .HasForeignKey(d => d.Role)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("naudotojai_role_fkey");
        });

        modelBuilder.Entity<Plokstele>(entity =>
        {
            entity.HasKey(e => e.Discogsid).HasName("plokstele_pkey");

            entity.ToTable("plokstele");

            entity.Property(e => e.Discogsid)
                .ValueGeneratedNever()
                .HasColumnName("discogsid");
            entity.Property(e => e.Albumas)
                .HasMaxLength(255)
                .HasColumnName("albumas");
            entity.Property(e => e.Atlikejas)
                .HasMaxLength(100)
                .HasColumnName("atlikejas");
            entity.Property(e => e.BruksninisKodas).HasColumnName("bruksninis_kodas");
            entity.Property(e => e.Busena).HasColumnName("busena");
            entity.Property(e => e.Formatas)
                .HasMaxLength(255)
                .HasColumnName("formatas");
            entity.Property(e => e.IrasuKompanija)
                .HasMaxLength(255)
                .HasColumnName("irasu_kompanija");
            entity.Property(e => e.IsigijimoKaina)
                .HasPrecision(10, 2)
                .HasColumnName("isigijimo_kaina");
            entity.Property(e => e.Kontekstas).HasColumnName("kontekstas");
            entity.Property(e => e.Matrica)
                .HasMaxLength(255)
                .HasColumnName("matrica");
            entity.Property(e => e.Metai).HasColumnName("metai");
            entity.Property(e => e.Stilius)
                .HasMaxLength(255)
                .HasColumnName("stilius");
            entity.Property(e => e.Vektorius)
                .HasMaxLength(1536)
                .HasColumnName("vektorius");
            entity.Property(e => e.Zanras)
                .HasMaxLength(255)
                .HasColumnName("zanras");

            entity.HasOne(d => d.BusenaNavigation).WithMany(p => p.Ploksteles)
                .HasForeignKey(d => d.Busena)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("plokstele_busena_fkey");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRoles).HasName("roles_pkey");

            entity.ToTable("roles");

            entity.Property(e => e.IdRoles)
                .ValueGeneratedNever()
                .HasColumnName("id_roles");
            entity.Property(e => e.Name)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("name");
        });

        modelBuilder.Entity<Sarasai>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("sarasai_pkey");

            entity.ToTable("sarasai");

            entity.Property(e => e.Id)
                .UseIdentityByDefaultColumn()
                .HasColumnName("id");
            entity.Property(e => e.FkMainai).HasColumnName("fk_mainai");
            entity.Property(e => e.FkNaudotojai)
                .HasMaxLength(255)
                .HasColumnName("fk_naudotojai");
            entity.Property(e => e.Tipas).HasColumnName("tipas");

            entity.HasOne(d => d.FkMainaiNavigation).WithMany(p => p.Sarasais)
                .HasForeignKey(d => d.FkMainai)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("sarasai_fk_mainai_fkey");

            entity.HasOne(d => d.FkNaudotojaiNavigation).WithMany(p => p.Sarasais)
                .HasForeignKey(d => d.FkNaudotojai)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("sarasai_fk_naudotojai_fkey");

            entity.HasOne(d => d.TipasNavigation).WithMany(p => p.Sarasais)
                .HasForeignKey(d => d.Tipas)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("sarasai_tipas_fkey");

            entity.HasMany(d => d.FkPloksteles).WithMany(p => p.FkSarasais)
                .UsingEntity<Dictionary<string, object>>(
                    "SarasaiPlokstele",
                    r => r.HasOne<Plokstele>().WithMany()
                        .HasForeignKey("FkPlokstele")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("sarasai_plokstele_fk_plokstele_fkey"),
                    l => l.HasOne<Sarasai>().WithMany()
                        .HasForeignKey("FkSarasai")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("sarasai_plokstele_fk_sarasai_fkey"),
                    j =>
                    {
                        j.HasKey("FkSarasai", "FkPlokstele").HasName("sarasai_plokstele_pkey");
                        j.ToTable("sarasai_plokstele");
                        j.IndexerProperty<int>("FkSarasai").HasColumnName("fk_sarasai");
                        j.IndexerProperty<int>("FkPlokstele").HasColumnName("fk_plokstele");
                    });
        });

        modelBuilder.Entity<SarasuTipai>(entity =>
        {
            entity.HasKey(e => e.IdSarasuTipai).HasName("sarasu_tipai_pkey");

            entity.ToTable("sarasu_tipai");

            entity.Property(e => e.IdSarasuTipai)
                .ValueGeneratedNever()
                .HasColumnName("id_sarasu_tipai");
            entity.Property(e => e.Name)
                .HasMaxLength(8)
                .IsFixedLength()
                .HasColumnName("name");
        });

        modelBuilder.Entity<Statusai>(entity =>
        {
            entity.HasKey(e => e.IdStatusai).HasName("statusai_pkey");

            entity.ToTable("statusai");

            entity.Property(e => e.IdStatusai)
                .ValueGeneratedNever()
                .HasColumnName("id_statusai");
            entity.Property(e => e.Name)
                .HasMaxLength(8)
                .IsFixedLength()
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
