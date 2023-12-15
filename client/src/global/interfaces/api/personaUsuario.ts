export interface PersonaUsuario {
  idPersona: string,
  ci: string,
  nombres: string,
  appaterno: string | null,
  apmaterno: string | null,
  usuario: string,
  password: string | null
}