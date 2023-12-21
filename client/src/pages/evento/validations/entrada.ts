import * as Yup from "yup";

export const entradaSchema = Yup.object({
  nombreEvent: Yup.string().required("Nombre es requerido"),
  costoEvent: Yup.number().required("Costo es requerido"),
  cantidadinicialEvent: Yup.number().required("Cantidad inicial es requerida"),
  stockEvent: Yup.number().required("Stock es requerido")
});

export interface EntradaForm extends Yup.InferType<typeof entradaSchema> {}
