import * as Yup from "yup";

export const userCajaSchema = Yup.object({
  userId: Yup.string().required("Usuario es requerida"),
});

export interface UserCajaForm extends Yup.InferType<typeof userCajaSchema> {}
