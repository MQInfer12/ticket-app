import { useNavigate } from "react-router-dom";
import { Cuenta } from "../../global/interfaces/api/cuenta";
import { useGet } from "../../hooks/useGet";
import { useModal } from "../../hooks/useModal";
import PageContainer from "../../global/components/pageContainer";
import TableContainer from "../../global/components/table/tableContainer";
import { useUser } from "../../store/user";
import { Roles } from "../../global/interfaces/types/roles";

const Cuentas = () => {
  const { res, getData, pushData, filterData, modifyData } =
    useGet<Cuenta[]>("Cuenta");
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
    <PageContainer title="Cajas">
      <TableContainer columns={columns} data={res?.data} reload={getData} />
    </PageContainer>
  );
};

export default Cuentas;
