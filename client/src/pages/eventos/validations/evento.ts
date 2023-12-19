import * as Yup from "yup";

export const eventoSchema = Yup.object({
  idEmpresa: Yup.string().required("Empresa es requerida"),
  nombre: Yup.string().required("Nombre es requerido"),
  fecha: Yup.string().required("Fecha es requerida"),
  cantidad: Yup.number().required("Cantidad es requerida"),
});

export interface EventoForm extends Yup.InferType<typeof eventoSchema> {}
