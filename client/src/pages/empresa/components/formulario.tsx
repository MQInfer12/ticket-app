import { Form, Formik } from "formik";
import Button from "../../../global/components/button";
import FormInput from "../../../global/components/formInput";
import { Empresa } from "../../../global/interfaces/api/empresa";
import { EmpresaForm, empresaSchema } from "../../../validations/empresa";
import { useRequest } from "../../../hooks/useRequest";
import { successAlert } from "../../../global/utils/alerts";

interface Props {
  empresa: Empresa | null;
  onSuccess: (data: Empresa) => void;
  onDelete: (data: Empresa) => void;
}

const Formulario = ({ empresa, onSuccess, onDelete }: Props) => {
  const { sendRequest } = useRequest();

  const handleSend = async (value: EmpresaForm) => {
    const res = await sendRequest<Empresa>(
      empresa ? `Empresa/${empresa.id}` : "Empresa",
      value,
      {
        method: empresa ? "PUT" : "POST",
      }
    );
    if (res) {
      successAlert(res.message);
      onSuccess(res.data);
    }
  };

  const handleDelete = async () => {
    if (!empresa) return;
    const res = await sendRequest(`Empresa/${empresa.id}`, null, {
      method: "DELETE",
    });
    if (res) {
      successAlert(res.message);
      onDelete(empresa);
    }
  };

  return (
    <Formik
      initialValues={{
        nombre: empresa?.nombre || "",
        direccion: empresa?.direccion || "",
      }}
      validationSchema={empresaSchema}
      onSubmit={handleSend}
    >
      <Form className="flex flex-col gap-6">
        <div className="flex flex-col gap-2">
          <FormInput title="Nombre" name="nombre" />
          <FormInput title="Dirección" name="direccion" />
        </div>
        <div className="self-center flex gap-4">
          <Button type="submit">Enviar</Button>
          {empresa && (
            <Button onClick={handleDelete} bg="rose-800">
              Eliminar
            </Button>
          )}
        </div>
      </Form>
    </Formik>
  );
};

export default Formulario;
