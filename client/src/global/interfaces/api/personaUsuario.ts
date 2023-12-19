import { Empresa } from "./empresa"
import { Rol } from "./rol"

export interface PersonaUsuario {
  idPersona: string,
  ci: string,
  nombres: string,
  appaterno: string | null,
  apmaterno: string | null,
  usuario: string,
  password: string | null
}

export interface EmpresasRolsForPersonas {
  empresas: Empresa[],
  rols: Rol[]
}