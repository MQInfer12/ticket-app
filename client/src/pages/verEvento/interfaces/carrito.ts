export interface Carrito {
  idUsuario?: string;
  items: CarritoItem[];
}

export interface CarritoItem {
  idEntrada: string;
  cantidad: number;
  ci: string[];
}
