import { Cuenta } from "../../global/interfaces/api/cuenta";
import { useGet } from "../../hooks/useGet";
import PageContainer from "../../global/components/pageContainer";
import TableContainer from "../../global/components/table/tableContainer";
import { useUser } from "../../store/user";
import { Roles } from "../../global/interfaces/types/roles";

const Ingresos = () => {
  const { res, getData } = useGet<Cuenta[]>("Cuenta");
  const { user } = useUser();

  const columns = [
    {
      header: "Cuenta",
      accessorKey: "nombre",
    },
    {
      header: "Tipo",
      accessorKey: "tipo",
    },
  ];

  if (user?.roleName === Roles.superadmin) {
    columns.unshift({
      header: "Empresa",
      accessorKey: "nombreEmpresa",
    });
  }
  return (
    <PageContainer title="Ingresos">
      <TableContainer columns={columns} data={res?.data} reload={getData} />
    </PageContainer>
  );
};

export default Ingresos;
