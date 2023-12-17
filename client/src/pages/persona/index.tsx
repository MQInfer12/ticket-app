import { useParams } from "react-router-dom";
import PageContainer from "../../global/components/pageContainer";
import TableContainer from "../../global/components/table/tableContainer";
import { useGet } from "../../hooks/useGet";
import { PersonaPage, UserRol } from "../../global/interfaces/api/rolUsuario";
import { useModal } from "../../hooks/useModal";
import Modal from "../../global/components/modal.tsx";
import Form from "../../global/components/form/form.tsx";
import { UsuarioRolForm, usuarioRolSchema } from "./validations/persona.tsx";
import FormSelect from "../../global/components/form/formSelect";

const Index = () => {
  const { id } = useParams();
  const { res: personaPage } = useGet<PersonaPage>(
    `Persona/GetPersonaPage/${id}`
  );
  const { res, getData, pushData, modifyData, filterData } = useGet<UserRol[]>(
    `TipoRol/GetRolesByPersona/${id}`
  );
  const { state, item, openModal, closeModal } = useModal<UserRol>(
    "Formulario de roles de usuario"
  );

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

  if (!personaPage || !res) {
    return null;
  }

  return (
    <PageContainer title="Persona">
      <div className="flex flex-col h-full gap-5">
        <div className="flex flex-col items-center">
          <h3 className="font-bold text-lg text-neutral-800">
            {personaPage?.data.userData.fullName}
          </h3>
          <p className="text-sm text-neutral-500">{`${personaPage?.data.userData.usuario} (CI: ${personaPage?.data.userData.ci})`}</p>
        </div>
        <div className="h-[calc(100%_-_68px)]">
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
                idUsuario: personaPage.data.userData.idUsuario,
                rol: item?.idRol || "",
                empresa: item?.idEmpresa || "",
                estado: !item ? "Activo" : item.estado ? "Activo" : "Desactivo",
              }}
              validationSchema={usuarioRolSchema}
              post={{
                route: "RolUsuario",
                onBody: (value) => ({
                  ...value, 
                  estado: value.estado === "Activo" 
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
                  estado: value.estado === "Activo" 
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
              <FormSelect name="empresa" title="Empresa">
                <option value="">Seleccione empresa</option>
                {personaPage?.data.empresas.map((data) => (
                  <option key={data.id} value={data.id}>
                    {data.nombre}
                  </option>
                ))}
              </FormSelect>
              <FormSelect name="rol" title="Rol">
                <option value="">Seleccione rol</option>
                {personaPage?.data.rols.map((data) => (
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
        </div>
      </div>
    </PageContainer>
  );
};

export default Index;
