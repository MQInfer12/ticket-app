export type ContactoTipo = "telefono" | "correo";

export interface Contacto {
  id: string;
  contactoName: string;
  tipo: ContactoTipo;
  personaempresa: string;
}
