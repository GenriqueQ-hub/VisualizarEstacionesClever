
using Microsoft.EntityFrameworkCore;
using VisualizadorEstacionesClever.Api.Models;

namespace VisualizadorEstacionesClever.Api.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Usuario> Usuarios => Set<Usuario>();

    public DbSet<Rol> Roles => Set<Rol>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.ToTable("Usuarios", "dbo");

            entity.HasKey(u => u.IdUsuario);

            entity.Property(u => u.IdUsuario)
                .HasColumnName("IdUsuario");

            entity.Property(u => u.Nombre)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(u => u.Apellido)
                .HasMaxLength(100);

            entity.Property(u => u.Correo)
                .HasMaxLength(254)
                .IsRequired();

            entity.Property(u => u.NombreUsuario)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(u => u.Password)
                .HasColumnName("Password")
                .HasMaxLength(500);

            entity.Property(u => u.Activo)
                .IsRequired();

            entity.Property(u => u.FechaCreacion)
                .HasColumnType("datetime2(0)")
                .IsRequired();

            entity.Property(u => u.UsuarioAD)
                .HasMaxLength(150);

            entity.HasIndex(u => u.NombreUsuario)
                .IsUnique()
                .HasDatabaseName("UQ_Usuarios_NombreUsuario");

            entity.HasIndex(u => u.Correo)
                .IsUnique()
                .HasDatabaseName("UQ_Usuarios_Correo");

            entity.HasIndex(u => u.UsuarioAD)
                .IsUnique()
                .HasFilter("[UsuarioAD] IS NOT NULL")
                .HasDatabaseName("UX_Usuarios_UsuarioAD");

            entity.HasOne(u => u.Rol)
                .WithMany(r => r.Usuarios)
                .HasForeignKey(u => u.IdRol)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.ToTable("Roles", "dbo");

            entity.HasKey(r => r.IdRol);

            entity.Property(r => r.Nombre)
                .HasMaxLength(50)
                .IsRequired();

            entity.Property(r => r.Descripcion)
                .HasMaxLength(200);
        });
    }
}
