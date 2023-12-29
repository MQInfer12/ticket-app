import { useGet } from "../../hooks/useGet";
import { Compras as ComprasType } from "../../global/interfaces/api/compras";
import PageContainer from "../../global/components/pageContainer";
import TableContainer from "../../global/components/table/tableContainer";
import Button from "../../global/components/buttons/button";
import { useNavigate } from "react-router-dom";

const Compras = () => {
  const { res, getData } = useGet<ComprasType[]>(
    "Transaccion/GetTransactionTicketByUser"
  );
  const navigate = useNavigate();

  const columns = [
    {
      header: "Evento",
      accessorKey: "nombreEvento",
    },
    {
      header: "Total",
      accessorFn: (row: ComprasType) =>
        `Bs. ${row.total}`,
    },
    {
      header: "Cantidad",
      accessorKey: "cantidad",
    },
    {
      header: "Fecha evento",
      accessorKey: "fechaEvento",
    },
    {
      header: "Fecha Compra",
      accessorKey: "fechaCompra",
    },
    {
      header: "Acciones",
      cell: (cell: any) => {
        const item: ComprasType = cell.row.original;
        return (
          <div className="flex justify-center">
            <Button
              onClick={() => navigate(`/dashboard/inicio/gracias/${item.id}`)}
            >
              Ver Detalles
            </Button>
          </div>
        );
      },
    },
  ];

  return (
    <PageContainer title="Tickets">
      <TableContainer
        data={res?.data}
        columns={columns}
        reload={getData}
        reports={false}
      />
    </PageContainer>
  );
};

export default Compras;