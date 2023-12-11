import { useId } from "react";

type Props = {
  label: string,
  type: string,
  icon: JSX.Element
}

const LoginInput = ({label, type, icon} : Props) => {
  const id = useId();

  return (
    <div className="flex gap-4 items-center">
      <div className="flex h-8 relative rounded-lg overflow-hidden items-center">
        <label htmlFor="" className="absolute p-1 text-slate-400 text-md left-1">{label}</label>
        <input 
          id={id} 
          type={type} 
          className="h-full "
        />
        <div className="h-full aspect-square bg-white">
          {icon}
        </div>
      </div>
    </div>
  )
}

export default LoginInput