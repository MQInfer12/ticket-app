import * as Yup from "yup";

export const personaUsuarioSchema = Yup.object({
  ci: Yup.string().required("CI es requerido"),
  nombres: Yup.string().required("Nombre es requerido"),
  appaterno: Yup.string(),
  apMaterno: Yup.string(),
  nombreUsurio: Yup.string().required("Usuario es requerido"),
  password: Yup.string().required("Contraseña es requerida"),
});

export interface PersonaUsuarioForm
  extends Yup.InferType<typeof personaUsuarioSchema> {}
