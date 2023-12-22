import * as Yup from "yup";

export const entradaSchema = Yup.object({
  cantidad: Yup.number().required("Cantidad es requerida").max(10,"Solo puede añadir 10 entradas"),
});

export interface EntradaForm
  extends Yup.InferType<typeof entradaSchema> {}
