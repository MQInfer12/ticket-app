import { useParams } from "react-router";
import Loader from "../../global/components/loader/loader";
import PageContainer from "../../global/components/pageContainer";
import { useGet } from "../../hooks/useGet";
import { Item } from "../../global/interfaces/api/item";
import { useModal } from "../../hooks/useModal";
import { createColumns } from "../../global/utils/createColumn";
import TableContainer from "../../global/components/table/tableContainer";
import Modal from "../../global/components/modal";
import Form from "../../global/components/form/form";
import { ItemForm, itemSchema } from "./validations/categoria";
import FormInput from "../../global/components/form/formInput";

const Categoria = () => {
  const { id } = useParams();
  const { res, getData, pushData, modifyData, filterData } = useGet<Item[]>(
    `Item/GetItemByCategory/${id}`
  );
  const { item, openModal, closeModal, state } =
    useModal<Item>("Formulario Item");

  const columns = createColumns<Item>([
    {
      header: "Detalle",
      accessorKey: "detalle",
    },
    {
      header: "Fecha registro",
      accessorKey: "fechaRegistro",
    },
    {
      header: "Cantidad inicial",
      accessorKey: "cantidadInicial",
    },
    {
      header: "Stock",
      accessorKey: "stock",
    },
    {
      header: "Costo",
      accessorKey: "costo",
    },
  ]);
  return (
    <PageContainer backRoute="/dashboard/categorias" title="Items">
      {!res ? (
        <Loader />
      ) : (
        <>
          <TableContainer
            columns={columns}
            data={res?.data}
            add={() => openModal()}
            onClickRow={(row) => openModal(row)}
            reload={getData}
          ></TableContainer>
          <Modal state={state}>
            <Form<Item | null, ItemForm>
              item={item}
              validationSchema={itemSchema}
              initialValues={{
                idCategoria: id,
                detalleItem: item?.detalle || "",
                fechaRegistroItem: item?.fechaRegistro || "",
                cantidadinicialItem: item?.cantidadInicial || "",
                stockItem: item?.stock || "",
                costoItem: item?.costo || "",
              }}
              post={{
                route: "Item/PostItem",
                onBody: (value) => value,
                onSuccess: (data) => {
                  pushData(data);
                  closeModal();
                },
              }}
              put={{
                route: `Item/${item?.id}`,
                onBody: (value) => value,
                onSuccess: (data) => {
                  modifyData(data, (value) => value.id === data.id);
                  closeModal();
                },
              }}
              del={{
                route: `Item/${item?.id}`,
                onSuccess: (data) => {
                  filterData((value) => value.id !== data.id);
                  closeModal();
                },
              }}
            >
              <FormInput title="Detalle" name="detalleItem" />
              <FormInput
                title="Fecha registro"
                name="fechaRegistroItem"
                type={"Date"}
              />
              <FormInput title="Cantidad inicial" name="cantidadinicialItem" />
              <FormInput title="Stock" name="stockItem" />
              <FormInput title="Costo" name="costoItem" />
            </Form>
          </Modal>
        </>
      )}
    </PageContainer>
  );
};

export default Categoria;
