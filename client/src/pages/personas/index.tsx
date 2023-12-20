import PageContainer from "../../global/components/pageContainer";
import TableContainer from "../../global/components/table/tableContainer";
import {
  EmpresasRolsForPersonas,
  PersonaUsuario,
} from "../../global/interfaces/api/personaUsuario";
import { useGet } from "../../hooks/useGet";
import { useModal } from "../../hooks/useModal";
import Modal from "../../global/components/modal.tsx";
import Form from "../../global/components/form/form.tsx";
import {
  PersonaUsuarioForm,
  personaUsuarioSchema,
} from "./validations/persona.tsx";
import FormInput from "../../global/components/form/formInput";
import Button from "../../global/components/buttons/button.tsx";
import { useNavigate } from "react-router-dom";
import FormSelect from "../../global/components/form/formSelect.tsx";
import { useUser } from "../../store/user.ts";
import { Roles } from "../../global/interfaces/types/roles.ts";
import { useState, useEffect } from "react";

const Personas = () => {
  const { res, getData, pushData, modifyData, filterData } =
    useGet<PersonaUsuario[]>("Persona");
  const { res: resEmpresasRols } = useGet<EmpresasRolsForPersonas>(
    "Persona/EmpresasRols"
  );
  const { state, item, openModal, closeModal, open } = useModal<PersonaUsuario>(
    "Formulario de persona"
  );
  const navigate = useNavigate();
  const { user } = useUser();
  const [rolType, setRolType] = useState("");
  const roles =
    user?.roleName === Roles.superadmin
      ? resEmpresasRols?.data.rols
      : resEmpresasRols?.data.rols.filter((x) => x.nombre != Roles.superadmin);

  const columns = [
    {
      header: "CI",
      accessorKey: "ci",
    },
    {
      header: "Nombres",
      accessorKey: "nombres",
    },
    {
      header: "Apellidos",
      accessorFn: (row: PersonaUsuario) =>
        `${row.appaterno || ""} ${row.apmaterno || ""}`,
    },
    {
      header: "Usuario",
      accessorKey: "usuario",
    },
    {
      header: "Acciones",
      cell: (cell: any) => {
        const item: PersonaUsuario = cell.row.original;
        return (
          <div className="flex justify-center">
            <Button
              onClick={() => navigate(`/dashboard/personas/${item.idPersona}`)}
            >
              Ver
            </Button>
          </div>
        );
      },
    },
  ];

  useEffect(() => {
    if (open == false) {
      setRolType("");
    }
  }, [open]);

  return (
    <PageContainer title="Personas">
      <TableContainer
        data={res?.data}
        columns={columns}
        reload={getData}
        add={() => openModal()}
        onClickRow={(row) => openModal(row)}
      />
      <Modal state={state}>
        <Form<PersonaUsuario | null, PersonaUsuarioForm>
          item={item}
          initialValues={{
            ci: item?.ci || "",
            nombres: item?.nombres || "",
            appaterno: item?.appaterno || "",
            apmaterno: item?.apmaterno || "",
            nombreUsurio: item?.usuario || "",
            password: item ? "password" : "",
            idTipoRol: item ? item.idPersona : "",
            idEmpresa:
              user?.roleName === Roles.superadmin
                ? item
                  ? item.idPersona
                  : ""
                : user?.companyId,
          }}
          validationSchema={personaUsuarioSchema}
          post={{
            route: "Persona",
            onBody: (value) => ({
              ...value,
              idEmpresa: rolType === Roles.superadmin ? null : value.idEmpresa
            }),
            onSuccess: (data) => {
              pushData(data);
              closeModal();
            },
          }}
          put={{
            route: `Persona/${item?.idPersona}`,
            onBody: (value) => value,
            onSuccess: (data) => {
              modifyData(data, (value) => value.idPersona === data.idPersona);
              closeModal();
            },
          }}
          del={{
            route: `Persona/${item?.idPersona}`,
            onSuccess: (data) => {
              filterData((value) => value.idPersona !== data.idPersona);
              closeModal();
            },
          }}
          onChange={(e) => {
            if (e.target.name == "idTipoRol")
              setRolType(
                resEmpresasRols?.data.rols.find(
                  (rol) => rol.id === e.target.value
                )?.nombre || ""
              );
          }}
        >
          <FormInput title="CI" name="ci" />
          <FormInput title="Nombres" name="nombres" />
          <FormInput title="Apellido paterno" name="appaterno" />
          <FormInput title="Apellido materno" name="apmaterno" />
          <FormInput title="Usuario" name="nombreUsurio" />
          {!item && <FormInput title="Contraseña" name="password" />}
          {!item && (
            <FormSelect title="Rol inicial" name="idTipoRol">
              <option value="">Seleccionar rol</option>
              {roles?.map((rol) => (
                <option key={rol.id} value={rol.id}>
                  {rol.nombre}
                </option>
              ))}
            </FormSelect>
          )}
          {user?.roleName === Roles.superadmin &&
            !item &&
            rolType !== Roles.superadmin && (
              <FormSelect title="Empresa inicial" name="idEmpresa">
                <option value="">Seleccionar empresa</option>
                {resEmpresasRols?.data.empresas.map((empresa) => (
                  <option key={empresa.id} value={empresa.id}>
                    {empresa.nombre}
                  </option>
                ))}
              </FormSelect>
            )}
        </Form>
      </Modal>
    </PageContainer>
  );
};

export default Personas;
