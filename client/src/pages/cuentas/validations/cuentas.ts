import * as Yup from "yup";

export const cuentaSchema = Yup.object({
  idEmpresa: Yup.string().required("Empresa es requerida"),
  nombre: Yup.string().required("Cuenta es requerida"),
  tipo: Yup.string().required("Tipo es requerido"),
});

export interface CuentaForm extends Yup.InferType<typeof cuentaSchema> {}
