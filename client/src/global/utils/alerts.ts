import Swal from "sweetalert2";

export const errorAlert = (text: string) => {
  Swal.fire({
    icon: "error",
    title: "Error",
    text,
  });
};
