import * as Yup from "yup";

export const categoriaSchema = Yup.object({
  idEmpresa: Yup.string().required("Empresa es requerida"),
  nombreCategoria: Yup.string().required("Categoria es requerida"),
});

export interface CategoriaForm extends Yup.InferType<typeof categoriaSchema> {}
