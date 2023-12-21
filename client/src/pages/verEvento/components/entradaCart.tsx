import Button from "../../../global/components/buttons/button";
import { Entrada } from "../../../global/interfaces/api/entrada";
import { Form, Formik } from "formik";
import FormInput from "../../../global/components/form/formInput";
import { entradaSchema } from "../validations/entrada";

interface Props {
  entrada: Entrada;
  handleSend: (cantidad: number, idEntrada: string) => void;
}

const EntradaCart = ({ entrada, handleSend }: Props) => {
  return (
    <Formik
      initialValues={{
        cantidad: "",
      }}
      onSubmit={(values, { resetForm }) => {
        handleSend(+values.cantidad, entrada.id);
        resetForm();
      }}
      validationSchema={entradaSchema}
    >
      <Form>
        <div key={entrada.id}>
          <p>{entrada.nombre}</p>
          <small>{entrada.costo} Bs.</small>
          <label>Cantidad</label>
          <FormInput title="Cantidad" name="cantidad" />
          <Button>Añadir</Button>
        </div>
      </Form>
    </Formik>
  );
};

export default EntradaCart;
