import { useId } from "react";

type Props = {
  label: string,
  type: string
}

const LoginInput = ({label, type} : Props) => {
  const id = useId();

  return (
    <div className="flex gap-4 items-center">
      <label htmlFor={id}>{label}</label>
      <div className="flex h-8">
        <div className="h-full aspect-square border border-solid border-gray-600 border-r-0"></div>
        <input 
          id={id} 
          type={type} 
          className="h-full border border-solid border-gray-600"
        />
      </div>
    </div>
  )
}

export default LoginInput