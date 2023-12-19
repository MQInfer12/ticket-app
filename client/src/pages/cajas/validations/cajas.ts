import * as Yup from "yup";

export const cajaSchema = Yup.object({
  companyId: Yup.string().required("Empresa es requerida"),
  cajaName: Yup.string().required("Caja es requerida"),
});

export interface CajaForm extends Yup.InferType<typeof cajaSchema> {}
