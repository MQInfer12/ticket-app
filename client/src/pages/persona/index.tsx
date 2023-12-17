import { useParams } from "react-router-dom";
import PageContainer from "../../global/components/pageContainer";
import TableContainer from "../../global/components/table/tableContainer";
import Mock from "../../mocks/rolUsuario.json";
import { useGet } from "../../hooks/useGet";

const Index = () => {
  const { id } = useParams();
  const { res } = useGet(`User/GetUserById/${id}`);

  const columns = [
    {
      header: "Rol",
      accessorKey: "tipoRol",
    },
    {
      header: "Empresa",
      accessorKey: "empresa",
    },
    {
      header: "Estado",
      accessorKey: "estado",
    },
  ];

  console.log(res);

  return (
    <PageContainer title="Persona">
      <div className="flex flex-col h-full gap-5">
        <div className="flex flex-col items-center">
          <h3 className="font-bold text-lg text-neutral-800">
            Mauricio Molina Quinteros
          </h3>
          <p className="text-sm text-neutral-500">@mqinfer12 (CI: 13621632)</p>
        </div>
        <div className="h-[calc(100%_-_68px)]">
          <TableContainer columns={columns} data={Mock} />
        </div>
      </div>
    </PageContainer>
  );
};

export default Index;
