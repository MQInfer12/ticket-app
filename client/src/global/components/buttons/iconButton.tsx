import { AnyFunction } from "../../interfaces/types/anyFunction"

interface Props {
  icon: JSX.Element
  onClick: AnyFunction
}

const IconButton = ({ icon, onClick }: Props) => {
  return (
    <div 
      onClick={onClick}
      className="w-5 h-5 cursor-pointer"
    >{icon}</div>
  )
}

export default IconButton