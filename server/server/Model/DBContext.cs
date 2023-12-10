using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace server.Model;

public partial class DBContext : DbContext
{
    public DBContext()
    {
    }

    public DBContext(DbContextOptions<DBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Caja> Cajas { get; set; }

    public virtual DbSet<Categorium> Categoria { get; set; }

    public virtual DbSet<Contacto> Contactos { get; set; }

    public virtual DbSet<Cuentum> Cuenta { get; set; }

    public virtual DbSet<Descuento> Descuentos { get; set; }

    public virtual DbSet<DetalleTransaccione> DetalleTransacciones { get; set; }

    public virtual DbSet<Empresa> Empresas { get; set; }

    public virtual DbSet<Historial> Historials { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<Persona> Personas { get; set; }

    public virtual DbSet<RolUsuario> RolUsuarios { get; set; }

    public virtual DbSet<Socio> Socios { get; set; }

    public virtual DbSet<TipoEntradum> TipoEntrada { get; set; }

    public virtual DbSet<TipoEvento> TipoEventos { get; set; }

    public virtual DbSet<TipoRol> TipoRols { get; set; }

    public virtual DbSet<TipoSocio> TipoSocios { get; set; }

    public virtual DbSet<Transaccion> Transaccions { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<UsuarioCaja> UsuarioCajas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=localhost;Port=5932;Database=ticketAppDB;User Id=postgres;Password=root;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("uuid-ossp");

        modelBuilder.Entity<Caja>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("caja_pkey");

            entity.ToTable("caja");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.Idempresa).HasColumnName("idempresa");
            entity.Property(e => e.Nombre)
                .HasMaxLength(75)
                .HasColumnName("nombre");

            entity.HasOne(d => d.IdempresaNavigation).WithMany(p => p.Cajas)
                .HasForeignKey(d => d.Idempresa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("caja_idempresa_fkey");
        });

        modelBuilder.Entity<Categorium>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("categoria_pkey");

            entity.ToTable("categoria");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.Idempresa).HasColumnName("idempresa");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");

            entity.HasOne(d => d.IdempresaNavigation).WithMany(p => p.Categoria)
                .HasForeignKey(d => d.Idempresa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("categoria_idempresa_fkey");
        });

        modelBuilder.Entity<Contacto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("contacto_pkey");

            entity.ToTable("contacto");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.Contacto1)
                .HasMaxLength(50)
                .HasColumnName("contacto");
            entity.Property(e => e.Personaempresa).HasColumnName("personaempresa");
            entity.Property(e => e.Tipo)
                .HasMaxLength(50)
                .HasColumnName("tipo");
        });

        modelBuilder.Entity<Cuentum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("cuenta_pkey");

            entity.ToTable("cuenta");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.Idempresa).HasColumnName("idempresa");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
            entity.Property(e => e.Tipo)
                .HasMaxLength(10)
                .HasColumnName("tipo");

            entity.HasOne(d => d.IdempresaNavigation).WithMany(p => p.Cuenta)
                .HasForeignKey(d => d.Idempresa)
                .HasConstraintName("cuenta_idempresa_fkey");
        });

        modelBuilder.Entity<Descuento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("descuento_pkey");

            entity.ToTable("descuento");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.Descuento1).HasColumnName("descuento");
            entity.Property(e => e.Estado)
                .IsRequired()
                .HasDefaultValueSql("true")
                .HasColumnName("estado");
            entity.Property(e => e.Idtipoentrada).HasColumnName("idtipoentrada");

            entity.HasOne(d => d.IdtipoentradaNavigation).WithMany(p => p.Descuentos)
                .HasForeignKey(d => d.Idtipoentrada)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("descuento_idtipoentrada_fkey");
        });

        modelBuilder.Entity<DetalleTransaccione>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("detalle_transacciones_pkey");

            entity.ToTable("detalle_transacciones");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.Cantidad)
                .HasDefaultValueSql("1")
                .HasColumnName("cantidad");
            entity.Property(e => e.Ci)
                .HasMaxLength(50)
                .HasColumnName("ci");
            entity.Property(e => e.Detalle)
                .HasMaxLength(255)
                .HasColumnName("detalle");
            entity.Property(e => e.Idtransaccion).HasColumnName("idtransaccion");
            entity.Property(e => e.Preciounitario).HasColumnName("preciounitario");
            entity.Property(e => e.Verificado)
                .IsRequired()
                .HasDefaultValueSql("true")
                .HasColumnName("verificado");

            entity.HasOne(d => d.IdtransaccionNavigation).WithMany(p => p.DetalleTransacciones)
                .HasForeignKey(d => d.Idtransaccion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("detalle_transacciones_idtransaccion_fkey");
        });

        modelBuilder.Entity<Empresa>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("empresa_pkey");

            entity.ToTable("empresa");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.Direccion)
                .HasMaxLength(75)
                .HasColumnName("direccion");
            entity.Property(e => e.Estado)
                .IsRequired()
                .HasDefaultValueSql("true")
                .HasColumnName("estado");
            entity.Property(e => e.Nombre)
                .HasMaxLength(75)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Historial>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("historial_pkey");

            entity.ToTable("historial");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.Anterior)
                .HasMaxLength(255)
                .HasColumnName("anterior");
            entity.Property(e => e.Fecha)
                .HasMaxLength(10)
                .HasColumnName("fecha");
            entity.Property(e => e.Idempresa).HasColumnName("idempresa");
            entity.Property(e => e.Idfila).HasColumnName("idfila");
            entity.Property(e => e.Idusuario).HasColumnName("idusuario");
            entity.Property(e => e.Nuevo)
                .HasMaxLength(255)
                .HasColumnName("nuevo");
            entity.Property(e => e.Tabla)
                .HasMaxLength(50)
                .HasColumnName("tabla");

            entity.HasOne(d => d.IdempresaNavigation).WithMany(p => p.Historials)
                .HasForeignKey(d => d.Idempresa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("historial_idempresa_fkey");

            entity.HasOne(d => d.IdusuarioNavigation).WithMany(p => p.Historials)
                .HasForeignKey(d => d.Idusuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("historial_idusuario_fkey");
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("item_pkey");

            entity.ToTable("item");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.Cantidadinicial).HasColumnName("cantidadinicial");
            entity.Property(e => e.Costo).HasColumnName("costo");
            entity.Property(e => e.Detalle)
                .HasMaxLength(100)
                .HasColumnName("detalle");
            entity.Property(e => e.Fecharegistro)
                .HasMaxLength(10)
                .HasColumnName("fecharegistro");
            entity.Property(e => e.Idcategoria).HasColumnName("idcategoria");
            entity.Property(e => e.Stock).HasColumnName("stock");

            entity.HasOne(d => d.IdcategoriaNavigation).WithMany(p => p.Items)
                .HasForeignKey(d => d.Idcategoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("item_idcategoria_fkey");
        });

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("persona_pkey");

            entity.ToTable("persona");

            entity.HasIndex(e => e.Ci, "persona_ci_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.Apmaterno)
                .HasMaxLength(50)
                .HasColumnName("apmaterno");
            entity.Property(e => e.Appaterno)
                .HasMaxLength(50)
                .HasColumnName("appaterno");
            entity.Property(e => e.Ci)
                .HasMaxLength(50)
                .HasColumnName("ci");
            entity.Property(e => e.Nombres)
                .HasMaxLength(50)
                .HasColumnName("nombres");
        });

        modelBuilder.Entity<RolUsuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("rol_usuario_pkey");

            entity.ToTable("rol_usuario");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.Estado)
                .IsRequired()
                .HasDefaultValueSql("true")
                .HasColumnName("estado");
            entity.Property(e => e.Idempresa).HasColumnName("idempresa");
            entity.Property(e => e.Idtiporol).HasColumnName("idtiporol");
            entity.Property(e => e.Idusuario).HasColumnName("idusuario");

            entity.HasOne(d => d.IdempresaNavigation).WithMany(p => p.RolUsuarios)
                .HasForeignKey(d => d.Idempresa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("rol_usuario_idempresa_fkey");

            entity.HasOne(d => d.IdtiporolNavigation).WithMany(p => p.RolUsuarios)
                .HasForeignKey(d => d.Idtiporol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("rol_usuario_idtiporol_fkey");

            entity.HasOne(d => d.IdusuarioNavigation).WithMany(p => p.RolUsuarios)
                .HasForeignKey(d => d.Idusuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("rol_usuario_idusuario_fkey");
        });

        modelBuilder.Entity<Socio>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("socio_pkey");

            entity.ToTable("socio");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.Estado)
                .IsRequired()
                .HasDefaultValueSql("true")
                .HasColumnName("estado");
            entity.Property(e => e.Fechafinal)
                .HasMaxLength(10)
                .HasColumnName("fechafinal");
            entity.Property(e => e.Fechainicio)
                .HasMaxLength(10)
                .HasColumnName("fechainicio");
            entity.Property(e => e.Idpersona).HasColumnName("idpersona");
            entity.Property(e => e.Idtiposocio).HasColumnName("idtiposocio");
            entity.Property(e => e.Partidos).HasColumnName("partidos");

            entity.HasOne(d => d.IdpersonaNavigation).WithMany(p => p.Socios)
                .HasForeignKey(d => d.Idpersona)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("socio_idpersona_fkey");

            entity.HasOne(d => d.IdtiposocioNavigation).WithMany(p => p.Socios)
                .HasForeignKey(d => d.Idtiposocio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("socio_idtiposocio_fkey");
        });

        modelBuilder.Entity<TipoEntradum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tipo_entrada_pkey");

            entity.ToTable("tipo_entrada");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.Cantidadinicial).HasColumnName("cantidadinicial");
            entity.Property(e => e.Costo).HasColumnName("costo");
            entity.Property(e => e.Idtipoevento).HasColumnName("idtipoevento");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
            entity.Property(e => e.Stock).HasColumnName("stock");

            entity.HasOne(d => d.IdtipoeventoNavigation).WithMany(p => p.TipoEntrada)
                .HasForeignKey(d => d.Idtipoevento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tipo_entrada_idtipoevento_fkey");
        });

        modelBuilder.Entity<TipoEvento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tipo_evento_pkey");

            entity.ToTable("tipo_evento");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.Fecha)
                .HasMaxLength(10)
                .HasColumnName("fecha");
            entity.Property(e => e.Idempresa).HasColumnName("idempresa");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");

            entity.HasOne(d => d.IdempresaNavigation).WithMany(p => p.TipoEventos)
                .HasForeignKey(d => d.Idempresa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tipo_evento_idempresa_fkey");
        });

        modelBuilder.Entity<TipoRol>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tipo_rol_pkey");

            entity.ToTable("tipo_rol");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<TipoSocio>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tipo_socio_pkey");

            entity.ToTable("tipo_socio");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.Costo).HasColumnName("costo");
            entity.Property(e => e.Descuento)
                .HasDefaultValueSql("0")
                .HasColumnName("descuento");
            entity.Property(e => e.Duracion).HasColumnName("duracion");
            entity.Property(e => e.Estado)
                .IsRequired()
                .HasDefaultValueSql("true")
                .HasColumnName("estado");
            entity.Property(e => e.Idempresa).HasColumnName("idempresa");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
            entity.Property(e => e.Partidos).HasColumnName("partidos");

            entity.HasOne(d => d.IdempresaNavigation).WithMany(p => p.TipoSocios)
                .HasForeignKey(d => d.Idempresa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tipo_socio_idempresa_fkey");
        });

        modelBuilder.Entity<Transaccion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("transaccion_pkey");

            entity.ToTable("transaccion");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.Extra).HasColumnName("extra");
            entity.Property(e => e.Fecha)
                .HasMaxLength(10)
                .HasColumnName("fecha");
            entity.Property(e => e.Idcaja).HasColumnName("idcaja");
            entity.Property(e => e.Idcuenta).HasColumnName("idcuenta");
            entity.Property(e => e.Idusuario).HasColumnName("idusuario");
            entity.Property(e => e.Montototal).HasColumnName("montototal");
            entity.Property(e => e.Tipoentrega)
                .HasMaxLength(50)
                .HasColumnName("tipoentrega");

            entity.HasOne(d => d.IdcajaNavigation).WithMany(p => p.Transaccions)
                .HasForeignKey(d => d.Idcaja)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("transaccion_idcaja_fkey");

            entity.HasOne(d => d.IdcuentaNavigation).WithMany(p => p.Transaccions)
                .HasForeignKey(d => d.Idcuenta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("transaccion_idcuenta_fkey");

            entity.HasOne(d => d.IdusuarioNavigation).WithMany(p => p.Transaccions)
                .HasForeignKey(d => d.Idusuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("transaccion_idusuario_fkey");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("usuario_pkey");

            entity.ToTable("usuario");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.Contrasenia)
                .HasMaxLength(50)
                .HasColumnName("contrasenia");
            entity.Property(e => e.Idpersona).HasColumnName("idpersona");
            entity.Property(e => e.Usuario1)
                .HasMaxLength(50)
                .HasColumnName("usuario");

            entity.HasOne(d => d.IdpersonaNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.Idpersona)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("usuario_idpersona_fkey");
        });

        modelBuilder.Entity<UsuarioCaja>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("usuario_caja_pkey");

            entity.ToTable("usuario_caja");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.Idcaja).HasColumnName("idcaja");
            entity.Property(e => e.Idusuario).HasColumnName("idusuario");

            entity.HasOne(d => d.IdcajaNavigation).WithMany(p => p.UsuarioCajas)
                .HasForeignKey(d => d.Idcaja)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("usuario_caja_idcaja_fkey");

            entity.HasOne(d => d.IdusuarioNavigation).WithMany(p => p.UsuarioCajas)
                .HasForeignKey(d => d.Idusuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("usuario_caja_idusuario_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
