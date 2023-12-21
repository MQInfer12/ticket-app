import Button from "../../global/components/buttons/button"
import IconHeart from "../../icons/iconHeart"

const Gracias = () => {
  return (
    <div className="w-full h-full flex flex-col gap-4 items-center justify-center animate-[appear_1.5s]">
      <div className="w-40 text-emerald-500">
        <IconHeart />
      </div>
      <p className="text-neutral-500">¡Gracias por su compra!</p>
      <Button>Ver detalles</Button>
    </div>
  )
}

export default Gracias