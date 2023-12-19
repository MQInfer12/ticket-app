import { Rol } from "./rol";

export interface UserData {
  fullName: string;
  ci: string;
  idUsuario: string;
  usuario: string;
}

export interface Empresa {
  id: string;
  nombre: string;
  direccion: string;
  estado: boolean;
}

export interface UserRol {
  id: string;
  idRol: string;
  rol: string;
  idEmpresa: string;
  empresa: string;
  estado: string;
  idUsuario: string;
  nombreUsuario: string;
  apellidoPaterno: string | null;
  apellidoMaterno: string | null;
}

export interface PersonaPage {
  userData: UserData;
  rols: Rol[];
  empresas: Empresa[];
}
