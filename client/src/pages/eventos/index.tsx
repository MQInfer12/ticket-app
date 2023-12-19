import { useNavigate } from "react-router-dom";
import Button from "../../global/components/buttons/button";
import PageContainer from "../../global/components/pageContainer";
import TableContainer from "../../global/components/table/tableContainer";
import { useGet } from "../../hooks/useGet";
import { useModal } from "../../hooks/useModal";
import { Evento } from "../../global/interfaces/api/evento";
import Form from "../../global/components/form/form.tsx";
import { EventoForm, eventoSchema } from "./validations/evento.ts";
import Modal from "../../global/components/modal.tsx";
import FormInput from "../../global/components/form/formInput.tsx";
import FormSelect from "../../global/components/form/formSelect.tsx";
import { Empresa } from "../../global/interfaces/api/empresa.ts";

const Eventos = () => {
  const navigate = useNavigate();
  const { res, getData, pushData, filterData, modifyData } =
    useGet<Evento[]>("EventType");
  const { res: dataEmpresa } = useGet<Empresa[]>("Empresa");
  const { state, item, openModal, closeModal } = useModal<Evento>(
    "Formulario de empresa"
  );

  const columns = [
    {
      header: "Empresa",
      accessorKey: "companyName",
    },
    {
      header: "Nombre",
      accessorKey: "typeEventName",
    },
    {
      header: "Fecha",
      accessorKey: "date",
    },
    {
      header: "Cantidad",
      accessorKey: "amount",
      cell: (info: any) => info.getValue() + " entradas",
    },
    {
      header: "Acciones",
      cell: () => {
        return (
          <div className="flex justify-center">
            <Button onClick={() => navigate(`/dashboard/eventos/${item?.id}`)}>
              Ver
            </Button>
          </div>
        );
      },
    },
  ];

  return (
    <PageContainer title="Eventos">
      <TableContainer
        columns={columns}
        data={res?.data}
        reload={getData}
        add={() => openModal()}
        onClickRow={(row) => openModal(row)}
      />
      <Modal state={state}>
        <Form<Evento | null, EventoForm>
          item={item}
          initialValues={{
            idEmpresa: item?.companyId || "",
            nombre: item?.typeEventName || "",
            fecha: item?.date.split("-").reverse().join("-") || "",
            cantidad: item?.amount || "",
          }}
          validationSchema={eventoSchema}
          post={{
            route: "EventType",
            onBody: (value) => ({
              ...value,
              fecha: value.fecha.split("-").reverse().join("-"),
            }),
            onSuccess: (data) => {
              pushData(data);
              closeModal();
            },
          }}
          put={{
            route: `EventType/${item?.id}`,
            onBody: (value) => value,
            onSuccess: (data) => {
              modifyData(data, (value) => value.id === data.id);
              closeModal();
            },
          }}
          del={{
            route: `EventType/${item?.id}`,
            onSuccess: (data) => {
              filterData((value) => value.id !== data.id);
              closeModal();
            },
          }}
        >
          {!item && (
            <FormSelect title="Empresa" name="idEmpresa">
              <option value="">Seleccione empresa...</option>
              {dataEmpresa?.data.map((data) => (
                <option value={data.id} key={data.id}>{data.nombre}</option>
              ))}
            </FormSelect>
          )}
          <FormInput title="Nombre" name="nombre" />
          <FormInput title="Cantidad" name="cantidad" type="Number" />
          <FormInput title="Fecha" name="fecha" type="Date" />
        </Form>
      </Modal>
    </PageContainer>
  );
};

export default Eventos;
