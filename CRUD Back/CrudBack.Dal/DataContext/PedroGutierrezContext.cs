using CrudBack.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudBack.Dal.DataContext;

public partial class PedroGutierrezContext : DbContext
{
    public PedroGutierrezContext()
    {
    }

    public PedroGutierrezContext(DbContextOptions<PedroGutierrezContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TareasPendiente> TareasPendientes { get; set; }
    public virtual DbSet<Usuario> Usuario { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TareasPendiente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TAREAS_P__3214EC078644B37E");

            entity.ToTable("TAREAS_PENDIENTES");

            entity.Property(e => e.Descripcion).HasMaxLength(150);
            entity.Property(e => e.Titulo).HasMaxLength(50);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Username).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Password).IsRequired().HasMaxLength(300);

            entity.HasIndex(e => e.Username).IsUnique();
            entity.HasIndex(e => e.Email).IsUnique();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
