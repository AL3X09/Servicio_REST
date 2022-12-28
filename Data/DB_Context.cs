using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Servicio_REST.EFModels;
using System;
using System.Collections.Generic;

namespace Servicio_REST.Data
{
    public partial class DB_Context : DbContext
    {
        public DB_Context()
        {
        }

        public DB_Context(DbContextOptions<DB_Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Ciudad> Ciudads { get; set; } = null!;
        public virtual DbSet<Soat> Soats { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=192.168.0.2;Database=SOAT;Username=postgres;Password=root");
            }
        }*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ciudad>(entity =>
            {
                entity.HasKey(e => e.IdC)
                    .HasName("ciudad_pkey");

                entity.ToTable("ciudad");

                entity.Property(e => e.IdC)
                    .HasColumnName("id_c")
                    .HasDefaultValueSql("nextval('ciudad_id_seq'::regclass)");

                entity.Property(e => e.Nombre).HasColumnName("nombre");
            });

            modelBuilder.Entity<Soat>(entity =>
            {
                entity.HasKey(e => e.IdS)
                    .HasName("soat_pkey");

                entity.ToTable("soat");

                entity.Property(e => e.IdS).HasColumnName("id_s");

                entity.Property(e => e.FechaFin).HasColumnName("fecha_fin");

                entity.Property(e => e.FechaInicio).HasColumnName("fecha_inicio");

                entity.Property(e => e.FechaVemciIemtpActual).HasColumnName("fecha_vemci,iemtp_actual");

                entity.Property(e => e.FkCiudad)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("fk_ciudad");

                entity.Property(e => e.FkUsuario)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("fk_usuario");

                entity.Property(e => e.PlacaAutomotor).HasColumnName("placa_automotor");

                entity.HasOne(d => d.FkCiudadNavigation)
                    .WithMany(p => p.Soats)
                    .HasForeignKey(d => d.FkCiudad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_ciudad");

                entity.HasOne(d => d.FkUsuarioNavigation)
                    .WithMany(p => p.Soats)
                    .HasForeignKey(d => d.FkUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_usuario");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdU)
                    .HasName("usuario_pkey");

                entity.ToTable("usuario");

                entity.Property(e => e.IdU)
                    .HasColumnName("id_u")
                    .HasDefaultValueSql("nextval('usuario_id_seq'::regclass)");

                entity.Property(e => e.Apellidos).HasColumnName("apellidos");

                entity.Property(e => e.Nombres).HasColumnName("nombres");

                entity.Property(e => e.NumIdentificacion).HasColumnName("num_identificacion");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}