import TableContainer from "../../global/components/table/tableContainer.tsx";
import PageContainer from "../../global/components/pageContainer.tsx";
import { useGet } from "../../hooks/useGet.tsx";
import { Empresa } from "../../global/interfaces/empresa.ts";

const Index = () => {
  const { res, getData } = useGet<Empresa[]>("Empresa");

  const columns = [
    {
      header: "#",
      accessorFn: (_: any, i: number) => i + 1,
    },
    {
      header: "Nombre",
      accessorKey: "nombre",
    },
    {
      header: "Dirección",
      accessorKey: "direccion",
      cell: (info: any) => info.getValue() || "N/A",
    },
    {
      header: "Estado",
      accessorKey: "estado",
      cell: (info: any) => (info.getValue() ? "Activo" : "Desactivo"),
    },
  ];

  return (
    <PageContainer title="Empresas">
      <TableContainer columns={columns} data={res?.data} reload={getData} />
    </PageContainer>
  );
};

export default Index;
