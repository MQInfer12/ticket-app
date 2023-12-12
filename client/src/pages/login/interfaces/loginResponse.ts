export interface LoginResponse {
  token: string
  data: Data
}

export interface Data {
  id: string
  idpersona: string
  nombreUsuario: string
  contrasenia: string
  historials: any[]
  idpersonaNavigation: any
  rolUsuarios: any[]
  transaccions: any[]
  usuarioCajas: any[]
}