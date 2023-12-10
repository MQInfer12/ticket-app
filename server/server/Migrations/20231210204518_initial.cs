using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.CreateTable(
                name: "contacto",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    contacto = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    tipo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    personaempresa = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("contacto_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "empresa",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    nombre = table.Column<string>(type: "character varying(75)", maxLength: 75, nullable: false),
                    direccion = table.Column<string>(type: "character varying(75)", maxLength: 75, nullable: true),
                    estado = table.Column<bool>(type: "boolean", nullable: false, defaultValueSql: "true")
                },
                constraints: table =>
                {
                    table.PrimaryKey("empresa_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "persona",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    ci = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    nombres = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    appaterno = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    apmaterno = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("persona_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tipo_rol",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("tipo_rol_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "caja",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    idempresa = table.Column<Guid>(type: "uuid", nullable: false),
                    nombre = table.Column<string>(type: "character varying(75)", maxLength: 75, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("caja_pkey", x => x.id);
                    table.ForeignKey(
                        name: "caja_idempresa_fkey",
                        column: x => x.idempresa,
                        principalTable: "empresa",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "categoria",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    idempresa = table.Column<Guid>(type: "uuid", nullable: false),
                    nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("categoria_pkey", x => x.id);
                    table.ForeignKey(
                        name: "categoria_idempresa_fkey",
                        column: x => x.idempresa,
                        principalTable: "empresa",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "cuenta",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    idempresa = table.Column<Guid>(type: "uuid", nullable: true),
                    nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    tipo = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("cuenta_pkey", x => x.id);
                    table.ForeignKey(
                        name: "cuenta_idempresa_fkey",
                        column: x => x.idempresa,
                        principalTable: "empresa",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "tipo_evento",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    idempresa = table.Column<Guid>(type: "uuid", nullable: false),
                    nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    fecha = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    cantidad = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("tipo_evento_pkey", x => x.id);
                    table.ForeignKey(
                        name: "tipo_evento_idempresa_fkey",
                        column: x => x.idempresa,
                        principalTable: "empresa",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "tipo_socio",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    idempresa = table.Column<Guid>(type: "uuid", nullable: false),
                    nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    costo = table.Column<double>(type: "double precision", nullable: false),
                    descuento = table.Column<double>(type: "double precision", nullable: true, defaultValueSql: "0"),
                    duracion = table.Column<int>(type: "integer", nullable: true),
                    partidos = table.Column<int>(type: "integer", nullable: true),
                    estado = table.Column<bool>(type: "boolean", nullable: false, defaultValueSql: "true")
                },
                constraints: table =>
                {
                    table.PrimaryKey("tipo_socio_pkey", x => x.id);
                    table.ForeignKey(
                        name: "tipo_socio_idempresa_fkey",
                        column: x => x.idempresa,
                        principalTable: "empresa",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "usuario",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    idpersona = table.Column<Guid>(type: "uuid", nullable: false),
                    usuario = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    contrasenia = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("usuario_pkey", x => x.id);
                    table.ForeignKey(
                        name: "usuario_idpersona_fkey",
                        column: x => x.idpersona,
                        principalTable: "persona",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "item",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    idcategoria = table.Column<Guid>(type: "uuid", nullable: false),
                    detalle = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    fecharegistro = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    cantidadinicial = table.Column<int>(type: "integer", nullable: false),
                    stock = table.Column<int>(type: "integer", nullable: false),
                    costo = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("item_pkey", x => x.id);
                    table.ForeignKey(
                        name: "item_idcategoria_fkey",
                        column: x => x.idcategoria,
                        principalTable: "categoria",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "tipo_entrada",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    idtipoevento = table.Column<Guid>(type: "uuid", nullable: false),
                    nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    costo = table.Column<double>(type: "double precision", nullable: false),
                    cantidadinicial = table.Column<int>(type: "integer", nullable: false),
                    stock = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("tipo_entrada_pkey", x => x.id);
                    table.ForeignKey(
                        name: "tipo_entrada_idtipoevento_fkey",
                        column: x => x.idtipoevento,
                        principalTable: "tipo_evento",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "socio",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    idtiposocio = table.Column<Guid>(type: "uuid", nullable: false),
                    idpersona = table.Column<Guid>(type: "uuid", nullable: false),
                    fechainicio = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    fechafinal = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    partidos = table.Column<int>(type: "integer", nullable: true),
                    estado = table.Column<bool>(type: "boolean", nullable: false, defaultValueSql: "true")
                },
                constraints: table =>
                {
                    table.PrimaryKey("socio_pkey", x => x.id);
                    table.ForeignKey(
                        name: "socio_idpersona_fkey",
                        column: x => x.idpersona,
                        principalTable: "persona",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "socio_idtiposocio_fkey",
                        column: x => x.idtiposocio,
                        principalTable: "tipo_socio",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "historial",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    idempresa = table.Column<Guid>(type: "uuid", nullable: false),
                    idusuario = table.Column<Guid>(type: "uuid", nullable: false),
                    fecha = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    tabla = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    idfila = table.Column<Guid>(type: "uuid", nullable: false),
                    anterior = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    nuevo = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("historial_pkey", x => x.id);
                    table.ForeignKey(
                        name: "historial_idempresa_fkey",
                        column: x => x.idempresa,
                        principalTable: "empresa",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "historial_idusuario_fkey",
                        column: x => x.idusuario,
                        principalTable: "usuario",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "rol_usuario",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    idusuario = table.Column<Guid>(type: "uuid", nullable: false),
                    idtiporol = table.Column<Guid>(type: "uuid", nullable: false),
                    idempresa = table.Column<Guid>(type: "uuid", nullable: false),
                    estado = table.Column<bool>(type: "boolean", nullable: false, defaultValueSql: "true")
                },
                constraints: table =>
                {
                    table.PrimaryKey("rol_usuario_pkey", x => x.id);
                    table.ForeignKey(
                        name: "rol_usuario_idempresa_fkey",
                        column: x => x.idempresa,
                        principalTable: "empresa",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "rol_usuario_idtiporol_fkey",
                        column: x => x.idtiporol,
                        principalTable: "tipo_rol",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "rol_usuario_idusuario_fkey",
                        column: x => x.idusuario,
                        principalTable: "usuario",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "transaccion",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    idcuenta = table.Column<Guid>(type: "uuid", nullable: false),
                    idusuario = table.Column<Guid>(type: "uuid", nullable: false),
                    idcaja = table.Column<Guid>(type: "uuid", nullable: false),
                    montototal = table.Column<double>(type: "double precision", nullable: false),
                    cantidad = table.Column<int>(type: "integer", nullable: false),
                    extra = table.Column<double>(type: "double precision", nullable: false),
                    tipoentrega = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    fecha = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("transaccion_pkey", x => x.id);
                    table.ForeignKey(
                        name: "transaccion_idcaja_fkey",
                        column: x => x.idcaja,
                        principalTable: "caja",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "transaccion_idcuenta_fkey",
                        column: x => x.idcuenta,
                        principalTable: "cuenta",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "transaccion_idusuario_fkey",
                        column: x => x.idusuario,
                        principalTable: "usuario",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "usuario_caja",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    idcaja = table.Column<Guid>(type: "uuid", nullable: false),
                    idusuario = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("usuario_caja_pkey", x => x.id);
                    table.ForeignKey(
                        name: "usuario_caja_idcaja_fkey",
                        column: x => x.idcaja,
                        principalTable: "caja",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "usuario_caja_idusuario_fkey",
                        column: x => x.idusuario,
                        principalTable: "usuario",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "descuento",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    idtipoentrada = table.Column<Guid>(type: "uuid", nullable: false),
                    descuento = table.Column<double>(type: "double precision", nullable: false),
                    estado = table.Column<bool>(type: "boolean", nullable: false, defaultValueSql: "true")
                },
                constraints: table =>
                {
                    table.PrimaryKey("descuento_pkey", x => x.id);
                    table.ForeignKey(
                        name: "descuento_idtipoentrada_fkey",
                        column: x => x.idtipoentrada,
                        principalTable: "tipo_entrada",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "detalle_transacciones",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    idtransaccion = table.Column<Guid>(type: "uuid", nullable: false),
                    detalle = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    cantidad = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "1"),
                    preciounitario = table.Column<double>(type: "double precision", nullable: false),
                    ci = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    verificado = table.Column<bool>(type: "boolean", nullable: false, defaultValueSql: "true")
                },
                constraints: table =>
                {
                    table.PrimaryKey("detalle_transacciones_pkey", x => x.id);
                    table.ForeignKey(
                        name: "detalle_transacciones_idtransaccion_fkey",
                        column: x => x.idtransaccion,
                        principalTable: "transaccion",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_caja_idempresa",
                table: "caja",
                column: "idempresa");

            migrationBuilder.CreateIndex(
                name: "IX_categoria_idempresa",
                table: "categoria",
                column: "idempresa");

            migrationBuilder.CreateIndex(
                name: "IX_cuenta_idempresa",
                table: "cuenta",
                column: "idempresa");

            migrationBuilder.CreateIndex(
                name: "IX_descuento_idtipoentrada",
                table: "descuento",
                column: "idtipoentrada");

            migrationBuilder.CreateIndex(
                name: "IX_detalle_transacciones_idtransaccion",
                table: "detalle_transacciones",
                column: "idtransaccion");

            migrationBuilder.CreateIndex(
                name: "IX_historial_idempresa",
                table: "historial",
                column: "idempresa");

            migrationBuilder.CreateIndex(
                name: "IX_historial_idusuario",
                table: "historial",
                column: "idusuario");

            migrationBuilder.CreateIndex(
                name: "IX_item_idcategoria",
                table: "item",
                column: "idcategoria");

            migrationBuilder.CreateIndex(
                name: "persona_ci_key",
                table: "persona",
                column: "ci",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_rol_usuario_idempresa",
                table: "rol_usuario",
                column: "idempresa");

            migrationBuilder.CreateIndex(
                name: "IX_rol_usuario_idtiporol",
                table: "rol_usuario",
                column: "idtiporol");

            migrationBuilder.CreateIndex(
                name: "IX_rol_usuario_idusuario",
                table: "rol_usuario",
                column: "idusuario");

            migrationBuilder.CreateIndex(
                name: "IX_socio_idpersona",
                table: "socio",
                column: "idpersona");

            migrationBuilder.CreateIndex(
                name: "IX_socio_idtiposocio",
                table: "socio",
                column: "idtiposocio");

            migrationBuilder.CreateIndex(
                name: "IX_tipo_entrada_idtipoevento",
                table: "tipo_entrada",
                column: "idtipoevento");

            migrationBuilder.CreateIndex(
                name: "IX_tipo_evento_idempresa",
                table: "tipo_evento",
                column: "idempresa");

            migrationBuilder.CreateIndex(
                name: "IX_tipo_socio_idempresa",
                table: "tipo_socio",
                column: "idempresa");

            migrationBuilder.CreateIndex(
                name: "IX_transaccion_idcaja",
                table: "transaccion",
                column: "idcaja");

            migrationBuilder.CreateIndex(
                name: "IX_transaccion_idcuenta",
                table: "transaccion",
                column: "idcuenta");

            migrationBuilder.CreateIndex(
                name: "IX_transaccion_idusuario",
                table: "transaccion",
                column: "idusuario");

            migrationBuilder.CreateIndex(
                name: "IX_usuario_idpersona",
                table: "usuario",
                column: "idpersona");

            migrationBuilder.CreateIndex(
                name: "IX_usuario_caja_idcaja",
                table: "usuario_caja",
                column: "idcaja");

            migrationBuilder.CreateIndex(
                name: "IX_usuario_caja_idusuario",
                table: "usuario_caja",
                column: "idusuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "contacto");

            migrationBuilder.DropTable(
                name: "descuento");

            migrationBuilder.DropTable(
                name: "detalle_transacciones");

            migrationBuilder.DropTable(
                name: "historial");

            migrationBuilder.DropTable(
                name: "item");

            migrationBuilder.DropTable(
                name: "rol_usuario");

            migrationBuilder.DropTable(
                name: "socio");

            migrationBuilder.DropTable(
                name: "usuario_caja");

            migrationBuilder.DropTable(
                name: "tipo_entrada");

            migrationBuilder.DropTable(
                name: "transaccion");

            migrationBuilder.DropTable(
                name: "categoria");

            migrationBuilder.DropTable(
                name: "tipo_rol");

            migrationBuilder.DropTable(
                name: "tipo_socio");

            migrationBuilder.DropTable(
                name: "tipo_evento");

            migrationBuilder.DropTable(
                name: "caja");

            migrationBuilder.DropTable(
                name: "cuenta");

            migrationBuilder.DropTable(
                name: "usuario");

            migrationBuilder.DropTable(
                name: "empresa");

            migrationBuilder.DropTable(
                name: "persona");
        }
    }
}
