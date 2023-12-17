import { useNavigate } from "react-router-dom";
import Button from "../../global/components/buttons/button";
import PageContainer from "../../global/components/pageContainer";
import TableContainer from "../../global/components/table/tableContainer";
import EventosMock from "../../mocks/evento.json";

const Eventos = () => {
  const navigate = useNavigate();

  const columns = [
    {
      header: "Nombre",
      accessorKey: "nombre",
    },
    {
      header: "Fecha",
      accessorKey: "fecha",
    },
    {
      header: "Cantidad",
      accessorKey: "cantidad",
      cell: (info: any) => info.getValue() + " entradas",
    },
    {
      header: "Acciones",
      cell: (cell: any) => {
        /* const item = cell.row.original; */
        return (
          <div className="flex justify-center">
            <Button
              onClick={() => navigate(`/dashboard/eventos/1`)}
            >
              Ver
            </Button>
          </div>
        );
      },
    },
  ];

  return (
    <PageContainer title="Eventos">
      <TableContainer columns={columns} data={EventosMock} />
    </PageContainer>
  );
};

export default Eventos;
