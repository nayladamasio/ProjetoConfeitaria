using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Confeitaria.Data.Migrations
{
    /// <inheritdoc />
    public partial class CriacaoTabelasSistemas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_CLIENTES",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "varchar(100)", nullable: false),
                    Cpf = table.Column<string>(type: "varchar(14)", nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", nullable: false),
                    Telefone = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_CLIENTES", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TB_PRODDUTOS",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "varchar(100)", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(Max)", nullable: false),
                    Imagem = table.Column<string>(type: "varchar(100)", nullable: true),
                    Peso = table.Column<string>(type: "varchar(50)", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Categoria = table.Column<int>(type: "int", nullable: false),
                    Disponivel = table.Column<bool>(type: "bit", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_PRODDUTOS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TB_PEDIDOS",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataEntrega = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataPedido = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HorarioEntrega = table.Column<int>(type: "int", nullable: false),
                    FormaPagamento = table.Column<int>(type: "int", nullable: false),
                    QuantProduto = table.Column<int>(type: "int", nullable: false),
                    ClienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_PEDIDOS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_PEDIDOS_TB_CLIENTES_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "TB_CLIENTES",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PedidosProdutos",
                columns: table => new
                {
                    PedidosId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProdutosId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidosProdutos", x => new { x.PedidosId, x.ProdutosId });
                    table.ForeignKey(
                        name: "FK_PedidosProdutos_TB_PEDIDOS_PedidosId",
                        column: x => x.PedidosId,
                        principalTable: "TB_PEDIDOS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PedidosProdutos_TB_PRODDUTOS_ProdutosId",
                        column: x => x.ProdutosId,
                        principalTable: "TB_PRODDUTOS",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TB_ENDERECOPEDIDO",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Cep = table.Column<string>(type: "varchar(8)", nullable: false),
                    Estado = table.Column<string>(type: "varchar(150)", nullable: false),
                    Cidade = table.Column<string>(type: "varchar(150)", nullable: false),
                    Bairro = table.Column<string>(type: "varchar(100)", nullable: false),
                    Logradouro = table.Column<string>(type: "varchar(100)", nullable: false),
                    Numero = table.Column<string>(type: "varchar(50)", nullable: false),
                    Complemento = table.Column<string>(type: "varchar(100)", nullable: true),
                    PedidoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_ENDERECOPEDIDO", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_ENDERECOPEDIDO_TB_PEDIDOS_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "TB_PEDIDOS",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PedidosProdutos_ProdutosId",
                table: "PedidosProdutos",
                column: "ProdutosId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_ENDERECOPEDIDO_PedidoId",
                table: "TB_ENDERECOPEDIDO",
                column: "PedidoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TB_PEDIDOS_ClienteId",
                table: "TB_PEDIDOS",
                column: "ClienteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PedidosProdutos");

            migrationBuilder.DropTable(
                name: "TB_ENDERECOPEDIDO");

            migrationBuilder.DropTable(
                name: "TB_PRODDUTOS");

            migrationBuilder.DropTable(
                name: "TB_PEDIDOS");

            migrationBuilder.DropTable(
                name: "TB_CLIENTES");
        }
    }
}
