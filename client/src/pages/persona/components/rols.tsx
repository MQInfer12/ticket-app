import { useParams } from "react-router-dom";
import TableContainer from "../../../global/components/table/tableContainer";
import { useGet } from "../../../hooks/useGet";
import {
  Empresa,
  UserRol,
} from "../../../global/interfaces/api/rolUsuario";
import { useModal } from "../../../hooks/useModal";
import Modal from "../../../global/components/modal";
import Form from "../../../global/components/form/form";
import { UsuarioRolForm, usuarioRolSchema } from "../validations/persona";
import FormSelect from "../../../global/components/form/formSelect";
import { Rol } from "../../../global/interfaces/api/rol";
import { useUser } from "../../../store/user";
import { Roles } from "../../../global/interfaces/types/roles";

interface Props {
  idUser: string;
  empresas: Empresa[];
  roles: Rol[];
}

const Rols = ({ idUser, empresas, roles }: Props) => {
  const { id } = useParams();
  const { res, getData, pushData, modifyData, filterData } = useGet<UserRol[]>(
    `TipoRol/GetRolesByPersona/${id}`
  );
  const { state, item, openModal, closeModal } = useModal<UserRol>(
    "Formulario de roles de usuario"
  );
  const { user } = useUser();

  const columns = [
    {
      header: "Rol",
      accessorKey: "rol",
    },
    {
      header: "Empresa",
      accessorKey: "empresa",
    },
    {
      header: "Estado",
      accessorKey: "estado",
      cell: (info: any) => (info.getValue() ? "Activo" : "Desactivo"),
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
        <Form<UserRol | null, UsuarioRolForm>
          item={item}
          initialValues={{
            idUsuario: idUser,
            rol: item?.idRol || "",
            empresa: user?.roleName === Roles.superadmin ? item?.idEmpresa || "" : user?.companyId,
            estado: !item ? "Activo" : item.estado ? "Activo" : "Desactivo",
          }}
          validationSchema={usuarioRolSchema}
          post={{
            route: "RolUsuario",
            onBody: (value) => ({
              ...value,
              estado: value.estado === "Activo",
            }),
            onSuccess: (data) => {
              pushData(data);
              closeModal();
            },
          }}
          put={{
            route: `RolUsuario/${item?.id}`,
            onBody: (value) => ({
              ...value,
              estado: value.estado === "Activo",
            }),
            onSuccess: (data) => {
              modifyData(data, (value) => value.id === data.id);
              closeModal();
            },
          }}
          del={{
            route: `RolUsuario/${item?.id}`,
            onSuccess: (data) => {
              filterData((value) => value.id !== data.id);
              closeModal();
            },
          }}
        >
          {
            user?.roleName === Roles.superadmin &&
            <FormSelect name="empresa" title="Empresa">
              <option value="">Seleccione empresa</option>
              {empresas.map((data) => (
                <option key={data.id} value={data.id}>
                  {data.nombre}
                </option>
              ))}
            </FormSelect>
          }
          <FormSelect name="rol" title="Rol">
            <option value="">Seleccione rol</option>
            {roles.map((data) => (
              <option key={data.id} value={data.id}>
                {data.nombre}
              </option>
            ))}
          </FormSelect>
          <FormSelect
            title="Estado"
            name="estado"
            placeholder="Seleccione estado"
          >
            <option value="Activo">Activo</option>
            <option value="Desactivo">Desactivo</option>
          </FormSelect>
        </Form>
      </Modal>
    </>
  );
};

export default Rols;
