import { Roles } from "../types/roles";

export interface User {
  userId: string;
  nombreUsuario: string;
  nombres: string;
  ci: string;
  apPaterno: string | null;
  apMaterno: string | null;
  nombreRol: Roles
}
