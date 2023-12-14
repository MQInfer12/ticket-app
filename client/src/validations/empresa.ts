import * as Yup from "yup";

export const empresaSchema = Yup.object({
  nombre: Yup.string().required("Nombre es requerido"),
  direccion: Yup.string()
});

export interface EmpresaForm extends Yup.InferType<typeof empresaSchema> {}