import Swal from "sweetalert2";
import { AnyFunction } from "../interfaces/types/anyFunction";

export const errorAlert = (text: string) => {
  Swal.fire({
    icon: "error",
    title: "Error",
    text,
  });
};

export const successAlert = (text: string) => {
  Swal.fire({
    icon: "success",
    title: "Éxito",
    text,
  });
};

export const confirmAlert = (onConfirm: AnyFunction) => {
  Swal.fire({
    title: "¿Estás seguro?",
    text: "Se eliminará este elemento permanentemente.",
    icon: "warning",
    showCancelButton: true,
    confirmButtonColor: '#9f1239',
    cancelButtonColor: '#404040',
    confirmButtonText: "Confirmar"
  }).then((result) => {
    if(result.isConfirmed) {
      onConfirm();
    }
  });
}