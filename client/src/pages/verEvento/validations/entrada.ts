import * as Yup from "yup";

export const entradaSchema = Yup.object({
  cantidad: Yup.number().required("Cantidad es requerida"),
});

export interface EntradaForm
  extends Yup.InferType<typeof entradaSchema> {}
