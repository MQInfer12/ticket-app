import { Rol } from "./rol";

export interface Empresa {
  id: number;
  nombre: string;
  direccion: string | null;
  estado: boolean;
}

export interface EmpresaRes {
  empresa: Empresa;
  roles: Rol[];
  personas: PersonaForEmpresa[];
}

export interface PersonaForEmpresa {
  idUsuario: string;
  nombreCompleto: string;
}
