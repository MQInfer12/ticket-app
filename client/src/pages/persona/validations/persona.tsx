import * as Yup from "yup";

export const usuarioRolSchema = Yup.object({
  idUsuario: Yup.string(),
  rol: Yup.string().required("Rol es requerido"),
  empresa: Yup.string().required("Empresa es requerida"),
  estado: Yup.string().required("Estado es requerido"),
});

export interface UsuarioRolForm
  extends Yup.InferType<typeof usuarioRolSchema> {}
