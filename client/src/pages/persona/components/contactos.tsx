import { useGet } from "../../../hooks/useGet";
import { Contacto } from "../../../global/interfaces/api/contacto";
import TableContainer from "../../../global/components/table/tableContainer";
import { useModal } from "../../../hooks/useModal";
import Modal from "../../../global/components/modal";
import Form from "../../../global/components/form/form";
import { ContactoForm, contactoSchema } from "../validations/contacto";
import FormInput from "../../../global/components/form/formInput";
import FormSelect from "../../../global/components/form/formSelect";
import { useParams } from "react-router-dom";

const Contactos = () => {
  const { id } = useParams();
  const { res, getData, pushData, modifyData, filterData } = useGet<Contacto[]>(
    `Contacto/GetContactoById/${id}`
  );
  const { state, item, openModal, closeModal } = useModal<Contacto>(
    "Formulario de contacto"
  );

  const columns = [
    {
      header: "Contacto",
      accessorKey: "contactoName",
    },
    {
      header: "Tipo de contacto",
      accessorKey: "tipo",
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
        <Form<Contacto | null, ContactoForm>
          item={item}
          initialValues={{
            contacto: item?.contactoName || "",
            tipoContacto: item?.tipo || "Teléfono",
            personaempresa: id,
          }}
          validationSchema={contactoSchema}
          post={{
            route: "Contacto/PostContacto",
            onBody: (value) => value,
            onSuccess: (data) => {
              pushData(data);
              closeModal();
            },
          }}
          put={{
            route: `Contacto/${item?.id}`,
            onBody: (value) => value,
            onSuccess: (data) => {
              modifyData(data, (value) => value.id === data.id);
              closeModal();
            },
          }}
          del={{
            route: `Contacto/${item?.id}`,
            onSuccess: (data) => {
              filterData((value) => value.id !== data.id);
              closeModal();
            },
          }}
        >
          <FormInput name="contacto" title="Dato de contacto" />
          <FormSelect name="tipoContacto" title="Tipo">
            <option value="Teléfono">Teléfono</option>
            <option value="Correo">Correo</option>
          </FormSelect>
        </Form>
      </Modal>
    </>
  );
};

export default Contactos;
