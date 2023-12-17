export interface UserData {
  fullName: string;
  ci: string;
  idUsuario: string;
  usuario: string;
}

export interface Rol {
  id: string;
  nombre: string;
}

export interface Empresa {
  id: string;
  nombre: string;
  direccion: string;
  estado: boolean;
}

export interface UserRol {
  id: string;
  idRol: string
  rol: string;
  idEmpresa: string;
  empresa: string;
  estado: string;
}

export interface PersonaPage {
  userData: UserData;
  rols: Rol[];
  empresas: Empresa[];
}
