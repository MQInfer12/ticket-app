import TableContainer from "../../global/components/table/tableContainer.tsx";
import PageContainer from "../../global/components/pageContainer.tsx";
import { useGet } from "../../hooks/useGet.tsx";
import { Empresa } from "../../global/interfaces/api/empresa.ts";
import Modal from "../../global/components/modal.tsx";
import { useModal } from "../../hooks/useModal.tsx";
import Form from "../../global/components/form/form.tsx";
import FormInput from "../../global/components/form/formInput.tsx";
import FormSelect from "../../global/components/form/formSelect.tsx";
import { EmpresaForm, empresaSchema } from "./validations/empresa.ts";

const Index = () => {
  const { res, getData, pushData, filterData, modifyData } =
    useGet<Empresa[]>("Empresa");
  const { state, item, openModal, closeModal } = useModal<Empresa>(
    "Formulario de empresa"
  );

  const columns = [
    {
      header: "#",
      accessorFn: (_: any, i: number) => i + 1,
    },
    {
      header: "Nombre",
      accessorKey: "nombre",
    },
    {
      header: "Dirección",
      accessorKey: "direccion",
      cell: (info: any) => info.getValue() || "N/A",
    },
    {
      header: "Estado",
      accessorKey: "estado",
      cell: (info: any) => (info.getValue() ? "Activo" : "Desactivo"),
    },
  ];

  return (
    <PageContainer title="Empresas">
      <TableContainer
        columns={columns}
        data={res?.data}
        reload={getData}
        add={() => openModal()}
        onClickRow={(row) => openModal(row)}
      />
      <Modal state={state}>
        <Form<Empresa | null, EmpresaForm>
          item={item}
          initialValues={{
            nombre: item?.nombre || "",
            direccion: item?.direccion || "",
            estado: !item ? "Activo" : item.estado ? "Activo" : "Desactivo",
          }}
          validationSchema={empresaSchema}
          post={{
            route: "Empresa",
            onBody: value => ({
              ...value,
              estado: value.estado === "Activo",
            }),
            onSuccess: data => {
              pushData(data);
              closeModal();
            }
          }}
          put={{
            route: `Empresa/${item?.id}`,
            onBody: value => ({
              ...value,
              estado: value.estado === "Activo",
            }),
            onSuccess: data => {
              modifyData(data, (value) => value.id === data.id);
              closeModal();
            }
          }}
          del={{
            route: `Empresa/${item?.id}`,
            onSuccess: data => {
              filterData((value) => value.id !== data.id);
              closeModal();
            }
          }}
        >
          <FormInput title="Nombre" name="nombre" />
          <FormInput title="Dirección" name="direccion" />
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
    </PageContainer>
  );
};

export default Index;
