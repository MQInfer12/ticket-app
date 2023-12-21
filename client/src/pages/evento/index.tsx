import { useParams } from "react-router-dom";
import PageContainer from "../../global/components/pageContainer";
import TableContainer from "../../global/components/table/tableContainer";
import { useGet } from "../../hooks/useGet";
import Loader from "../../global/components/loader/loader";
import { Evento as EventoType } from "../../global/interfaces/api/evento";
import { Entrada } from "../../global/interfaces/api/entrada";
import Modal from "../../global/components/modal";
import { useModal } from "../../hooks/useModal";
import Form from "../../global/components/form/form";
import { EntradaForm, entradaSchema } from "./validations/entrada";
import FormInput from "../../global/components/form/formInput";

const Evento = () => {
  const { id } = useParams();
  const { res } = useGet<EventoType>(`EventType/GetEventByEventTypeId/${id}`);
  const {
    res: dataRes,
    pushData,
    modifyData,
    filterData,
    getData
  } = useGet<Entrada[]>(`TipoEntrada/GetTipoEntradaById/${id}`);
  const { state, item, openModal, closeModal } = useModal<Entrada>(
    "Formulario de entradas"
  );

  const columns = [
    {
      header: "Nombre",
      accessorKey: "nombre",
    },
    {
      header: "Costo",
      accessorKey: "costo",
    },
    {
      header: "Cantidad inicial",
      accessorKey: "cantidadinicial",
    },
    {
      header: "Stock",
      accessorKey: "stock",
    },
  ];

  return (
    <PageContainer backRoute="/dashboard/eventos" title="Evento">
      {!res ? (
        <Loader />
      ) : (
        <div className="flex flex-col h-full gap-5">
          <div className="flex flex-col items-center gap-2">
            <div className="flex flex-col items-center">
              <h3 className="font-bold text-lg text-neutral-800">
                {res.data.typeEventName}
              </h3>
              <p className="text-xs text-neutral-500">{`${res.data.date} (${res.data.amount} entradas)`}</p>
            </div>
          </div>
          <div className="h-[calc(100%_-_64px)]">
            <TableContainer
              data={dataRes?.data}
              columns={columns}
              reload={getData}
              add={() => openModal()}
              onClickRow={(row) => openModal(row)}
            />
            <Modal state={state}>
              <Form<Entrada | null, EntradaForm>
                item={item}
                initialValues={{
                  nombreEvent: item?.nombre || "",
                  costoEvent: item?.costo || "",
                  cantidadinicialEvent: item?.cantidadinicial || "",
                  stockEvent: item?.stock || "stock",
                }}
                validationSchema={entradaSchema}
                post={{
                  route: "TipoEntrada/PostTipoEntrada",
                  onBody: (value) => ({
                    ...value,
                    stockEvent: value.cantidadinicialEvent,
                    idEvent: id,
                  }),
                  onSuccess: (data) => {
                    pushData(data);
                    closeModal();
                  },
                }}
                put={{
                  route: `TipoEntrada/${item?.id}`,
                  onBody: (value) => ({
                    ...value,
                    idEvent: id,
                  }),
                  onSuccess: (data) => {
                    modifyData(data, (value) => value.id === data.id);
                    closeModal();
                  },
                }}
                del={{
                  route: `TipoEntrada/${item?.id}`,
                  onSuccess: (data) => {
                    filterData((value) => value.id !== data.id);
                    closeModal();
                  },
                }}
              >
                <FormInput title="Nombre" name="nombreEvent" />
                <FormInput type="number" title="Costo" name="costoEvent" />
                <FormInput
                  type="number"
                  title="Cantidad inicial"
                  name="cantidadinicialEvent"
                />
                {item && (
                  <FormInput type="number" title="Stock" name="stockEvent" />
                )}
              </Form>
            </Modal>
          </div>
        </div>
      )}
    </PageContainer>
  );
};

export default Evento;
