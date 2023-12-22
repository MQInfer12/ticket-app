import { Cuenta } from "../../global/interfaces/api/cuenta";
import { useGet } from "../../hooks/useGet";
import PageContainer from "../../global/components/pageContainer";
import TableContainer from "../../global/components/table/tableContainer";
import { useUser } from "../../store/user";
import { Roles } from "../../global/interfaces/types/roles";
import { useModal } from "../../hooks/useModal";
import Modal from "../../global/components/modal";
import Form from "../../global/components/form/form";
import { CuentaForm, cuentaSchema } from "./validations/cuentas";
import FormInput from "../../global/components/form/formInput";
import FormSelect from "../../global/components/form/formSelect";
import { Empresa } from "../../global/interfaces/api/empresa";
import {Cuentas as typeCuentas}  from "../../global/interfaces/types/cuentas";

const Cuentas = () => {
  const { user } = useUser();
  const { res, getData, pushData, modifyData, filterData } =
    useGet<Cuenta[]>("Cuenta");
  const { res: dataEmpresa } = useGet<Empresa[]>("Empresa");
  const { state, item, openModal, closeModal } =
    useModal<Cuenta>("Formulario cuenta");

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
      <TableContainer
        columns={columns}
        data={res?.data}
        reload={getData}
        add={() => openModal()}
        onClickRow={(row) => openModal(row)}
      />
      <Modal state={state}>
        <Form<Cuenta | null, CuentaForm>
          item={item}
          validationSchema={cuentaSchema}
          initialValues={{
            idEmpresa:
              user?.roleName === Roles.superadmin
                ? item
                  ? item.idEmpresa
                  : ""
                : user?.companyId,
            nombreCuenta: item?.nombre || "",
            tipo: item?.tipo || "",
          }}
          post={{
            route: "Cuenta",
            onBody: (value) => value,
            onSuccess: (data) => {
              pushData(data);
              closeModal();
            },
          }}
          put={{
            route: `Cuenta/${item?.id}`,
            onBody: (value) => value,
            onSuccess: (data) => {
              modifyData(data, (value) => data.id === value.id);
              closeModal();
            },
          }}
          del={{
            route: `Cuenta/${item?.id}`,
            onSuccess: (data) => {
              filterData((value) => value.id !== data.id);
            },
          }}
         showDelete={!Object.values(typeCuentas).includes(item?.nombre as typeCuentas)}
         showEnviar={!Object.values(typeCuentas).includes(item?.nombre as typeCuentas)}
        >
          {user?.roleName === Roles.superadmin && (
            <FormSelect name="idEmpresa" title="Empresa">
              <option value="">Seleccione una empresa</option>
              {dataEmpresa?.data.map((data) => (
                <option value={data.id} key={data.id}>
                  {data.nombre}
                </option>
              ))}
            </FormSelect>
          )}
          <FormInput name="nombreCuenta" title="Nombre" />
          <FormSelect name="tipo" title="Tipo">
            <option value="">Seleccione un tipo</option>
            <option value="Ingreso">Ingreso</option>
            <option value="Egreso">Egreso</option>
          </FormSelect>
        </Form>
      </Modal>
    </PageContainer>
  );
};

export default Cuentas;
