using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace PruebaBackendASEINFO.Models
{
    public partial class TiendaASEINFOContext : DbContext
    {
        public TiendaASEINFOContext()
        {
        }

        public TiendaASEINFOContext(DbContextOptions<TiendaASEINFOContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categoria> Categorias { get; set; }
        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<Compra> Compras { get; set; }
        public virtual DbSet<Login> Logins { get; set; }
        public virtual DbSet<Marca> Marcas { get; set; }
        public virtual DbSet<Producto> Productos { get; set; }
        public virtual DbSet<RecuperaContrasenia> RecuperaContrasenias { get; set; }
        public virtual DbSet<Rol> Rols { get; set; }
        public virtual DbSet<Tipo> Tipos { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasKey(e => e.IdCategoria)
                    .HasName("PK__Categori__A3C02A1031C2BAD6");

                entity.Property(e => e.FechaModifica).HasColumnType("datetime");

                entity.Property(e => e.Habilitado)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IpModifica)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.IdCliente)
                    .HasName("PK__Clientes__D5946642C6023967");

                entity.Property(e => e.Apellidos)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Direccion)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.Property(e => e.FechaModifica).HasColumnType("datetime");

                entity.Property(e => e.Genero)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Habilitado)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IpModifica)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Clientes)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FKClienteUsuario");
            });

            modelBuilder.Entity<Compra>(entity =>
            {
                entity.HasKey(e => e.IdCompra)
                    .HasName("PK__Compras__0A5CDB5C4445480E");

                entity.Property(e => e.FechaModifica).HasColumnType("datetime");

                entity.Property(e => e.Habilitado)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IpModifica)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.TotalPagar).HasColumnType("decimal(8, 2)");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.Compras)
                    .HasForeignKey(d => d.IdProducto)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FKCompraProducto");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Compras)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FKCompraUsuario");
            });

            modelBuilder.Entity<Login>(entity =>
            {
                entity.HasKey(e => e.IdLogin)
                    .HasName("PK__Logins__C3C6C6F18191B692");

                entity.Property(e => e.FechaLogin).HasColumnType("datetime");

                entity.Property(e => e.FechaModifica).HasColumnType("datetime");

                entity.Property(e => e.Habilitado)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IpModifica)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Logins)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FKLoginsUsuario");
            });

            modelBuilder.Entity<Marca>(entity =>
            {
                entity.HasKey(e => e.IdMarca)
                    .HasName("PK__Marcas__4076A887F534A70C");

                entity.Property(e => e.FechaModifica).HasColumnType("datetime");

                entity.Property(e => e.Habilitado)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IpModifica)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.IdProducto)
                    .HasName("PK__Producto__09889210C2CDD8B5");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.Property(e => e.FechaModifica).HasColumnType("datetime");

                entity.Property(e => e.Habilitado)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Imagen)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.IpModifica)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Precio).HasColumnType("decimal(8, 2)");

                entity.HasOne(d => d.IdCategoriaNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.IdCategoria)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FKProductoCategoria");

                entity.HasOne(d => d.IdMarcaNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.IdMarca)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FKProductoMarca");

                entity.HasOne(d => d.IdTipoNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.IdTipo)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FKProductoTipo");
            });

            modelBuilder.Entity<RecuperaContrasenia>(entity =>
            {
                entity.HasKey(e => e.IdRecupera)
                    .HasName("PK__Recupera__1014E14A61A278EB");

                entity.Property(e => e.FechaModifica).HasColumnType("datetime");

                entity.Property(e => e.Habilitado)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IpModifica)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.Token)
                    .IsRequired();

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.RecuperaContrasenia)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FKRContraUsuario");
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.HasKey(e => e.IdRol)
                    .HasName("PK__Rol__2A49584C78A778DB");

                entity.ToTable("Rol");

                entity.Property(e => e.FechaModifica).HasColumnType("datetime");

                entity.Property(e => e.Habilitado)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IpModifica)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Tipo>(entity =>
            {
                entity.HasKey(e => e.IdTipo)
                    .HasName("PK__Tipos__9E3A29A5B95CFFBF");

                entity.Property(e => e.FechaModifica).HasColumnType("datetime");

                entity.Property(e => e.Habilitado)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IpModifica)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PK__Usuarios__5B65BF972BE496B7");

                entity.Property(e => e.Contrasenia).IsRequired();

                entity.Property(e => e.Salt).IsRequired().HasMaxLength(50);

                entity.Property(e => e.Correo)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FechaModifica).HasColumnType("datetime");

                entity.Property(e => e.Habilitado)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Imagen).HasMaxLength(250);

                entity.Property(e => e.IpModifica)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.NombreUsuario)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdRol)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FKUsuarioRol");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
