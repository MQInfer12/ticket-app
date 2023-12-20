import { useParams } from "react-router-dom";
import { useGet } from "../../../hooks/useGet";
import { UsuarioCaja } from "../../../global/interfaces/api/usuarioCaja";
import TableContainer from "../../../global/components/table/tableContainer";
import { useModal } from "../../../hooks/useModal";
import Modal from "../../../global/components/modal";
import Form from "../../../global/components/form/form";
import { UserCajaForm, userCajaSchema } from "../validations/user";
import FormSelect from "../../../global/components/form/formSelect";
import { CajaUser } from "../../../global/interfaces/api/rolUsuario";

interface Props {
  idEmpresa: string;
}
const User = ({ idEmpresa }: Props) => {
  const { id } = useParams();
  const { res, getData, pushData, filterData, modifyData } = useGet<
    UsuarioCaja[]
  >(`UsuarioCaja/${id}`);
  const { res: dataCajero } = useGet<CajaUser[]>(
    `RolUsuario/GetUserCajaByCompany/${idEmpresa}`
  );
  const { state, item, openModal, closeModal } = useModal<UsuarioCaja>(
    "Formulario de Usuario Caja"
  );

  const columns = [
    {
      header: "Nombre",
      accessorKey: "name",
    },
    {
      header: "Apellidos",
      accessorKey: "lastName",
    },
  ];

  return (
    <>
      <TableContainer
        columns={columns}
        data={res?.data}
        reload={getData}
        add={() => openModal()}
        onClickRow={(row) => openModal(row)}
      />
      <Modal state={state}>
        <Form<UsuarioCaja | null, UserCajaForm>
          item={item}
          initialValues={{
            cajaId: id,
            userId: item?.idUsuario || "",
          }}
          validationSchema={userCajaSchema}
          post={{
            route: "UsuarioCaja",
            onBody: (value) => value,
            onSuccess: (data) => {
              pushData(data);
              closeModal();
            },
          }}
          put={{
            route: `UsuarioCaja/${item?.id}`,
            onBody: (value) => value,
            onSuccess: (data) => {
              modifyData(data, (value) => value.id === data.id);
              closeModal();
            },
          }}
          del={{
            route: `UsuarioCaja/${item?.id}`,
            onSuccess: (data) => {
              filterData((value) => value.id !== data.id);
              closeModal();
            },
          }}
        >
          <FormSelect name="userId" title="Usuario">
            <option value="">Seleccione usuario</option>
            {dataCajero?.data.map((data) => (
              <option key={data.id} value={data.id}>
                {data.fullName}
              </option>
            ))}
          </FormSelect>
        </Form>
      </Modal>
    </>
  );
};

export default User;
