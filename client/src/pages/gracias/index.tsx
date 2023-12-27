import { useParams } from "react-router-dom";
import Button from "../../global/components/buttons/button";
import IconHeart from "../../icons/iconHeart";

const Gracias = () => {
  const { id } = useParams();

  return (
    <div className="w-full h-full flex flex-col gap-4 items-center justify-center animate-[appear_1.5s]">
      <div className="w-40 text-emerald-500">
        <IconHeart />
      </div>
      <p className="text-neutral-500">¡Gracias por su compra!</p>
      <a href={`${import.meta.env.VITE_BACKEND}Transaccion/GeneratePdf/${id}`}>
        <Button>Ver detalles</Button>
      </a>
    </div>
  );
};

export default Gracias;
