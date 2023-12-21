import { useGet } from "../../hooks/useGet";
import PageContainer from "../../global/components/pageContainer";
import TableContainer from "../../global/components/table/tableContainer";
import { useUser } from "../../store/user";
import { Roles } from "../../global/interfaces/types/roles";
import { Ingreso } from "../../global/interfaces/api/ingreso";

const Ingresos = () => {
  const { res, getData } = useGet<Ingreso[]>("Transaccion");
  const { user } = useUser();

  const columns = [
    {
      header: "Cliente",
      accessorKey: "nombreUsuario",
    },
    {
      header: "Total",
      accessorKey: "total",
    },
    {
      header: "Cantidad",
      accessorKey: "cantidad",
    },
    {
      header: "Extra",
      accessorKey: "extra",
    },
    {
      header: "Tipo de entrega",
      accessorKey: "tipoEntrega",
    },
    {
      header: "Fecha",
      accessorKey: "fecha",
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
