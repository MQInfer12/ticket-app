import * as Yup from "yup";

export const itemSchema = Yup.object({
  detalleItem: Yup.string().required("Detalle es requerido"),
  fechaRegistroItem: Yup.string().required("Fecha de registro es requerida"),
  cantidadinicialItem: Yup.string().required("Cantidad inicial es requerida"),
  stockItem: Yup.string().required("Stock es requerido"),
  costoItem: Yup.string().required("Costo es requerido"),
});
export interface ItemForm extends Yup.InferType<typeof itemSchema> {}
