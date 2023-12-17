import * as Yup from "yup";

export const contactoSchema = Yup.object({
  contacto: Yup.string().required("El dato es requerido"),
  tipoContacto: Yup.string().required("Tipo es requerido"),
  personaempresa: Yup.string().required("Este campo es requerido"),
});

export interface ContactoForm extends Yup.InferType<typeof contactoSchema> {}
