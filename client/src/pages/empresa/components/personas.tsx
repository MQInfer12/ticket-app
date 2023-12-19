import { useParams } from "react-router-dom";
import { useGet } from "../../../hooks/useGet";
import { UserRol } from "../../../global/interfaces/api/rolUsuario";
import TableContainer from "../../../global/components/table/tableContainer";
import { useModal } from "../../../hooks/useModal";
import Modal from "../../../global/components/modal";
import Form from "../../../global/components/form/form";
import FormSelect from "../../../global/components/form/formSelect";
import { usuarioRolSchema } from "../../persona/validations/persona";
import { Rol } from "../../../global/interfaces/api/rol";
import { PersonaForEmpresa } from "../../../global/interfaces/api/empresa";

interface Props {
  roles: Rol[];
  personas: PersonaForEmpresa[];
}

const Personas = ({ roles, personas }: Props) => {
  const { id } = useParams();
  const { res, getData, pushData, modifyData, filterData } = useGet<UserRol[]>(
    `TipoRol/GetRolesByEmpresa/${id}`
  );
  const { state, item, openModal, closeModal } =
    useModal<UserRol>("Formulario de rol");

  const columns = [
    {
      header: "Persona",
      accessorFn: (row: UserRol) =>
        `${row.nombreUsuario} ${row.apellidoPaterno || ""} ${row.apellidoMaterno || ""}`,
    },
    {
      header: "Rol",
      accessorKey: "rol",
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
        <Form<UserRol | null, UserRol>
          item={item}
          initialValues={{
            idUsuario: item?.idUsuario || "",
            rol: item?.idRol || "",
            empresa: id,
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
          <FormSelect name="idUsuario" title="Personas">
            <option value="">Seleccione la persona</option>
            {personas.map((data) => (
              <option key={data.idUsuario} value={data.idUsuario}>
                {data.nombreCompleto}
              </option>
            ))}
          </FormSelect>
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

export default Personas;
