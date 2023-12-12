import PageContainer from "../../global/components/pageContainer";
import TableContainer from "../../global/components/table/tableContainer";
import PersonasMock from "../../mocks/persona.json";

const Index = () => {
  const columns = [
    {
      header: "ID",
      accessorKey: "idPersona",
    },
    {
      header: "CI",
      accessorKey: "ci",
    },
    {
      header: "Nombres",
      accessorKey: "nombres",
    },
    {
      header: "Usuario",
      accessorKey: "usuario",
    },
    {
      header: "Rol",
      accessorKey: "nombreRol",
    },
    {
      header: "Empresa",
      accessorKey: "nombreEmpresa",
    },
  ];

  return (
    <PageContainer title="Personas">
      <TableContainer data={PersonasMock} columns={columns} />
    </PageContainer>
  );
};

export default Index;
