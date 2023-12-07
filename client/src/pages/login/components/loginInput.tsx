import { useId } from "react";

const LoginInput = () => {
  const id = useId();

  return (
    <div className="flex gap-4 items-center">
      <label htmlFor={id}>Email</label>
      <div className="flex h-8">
        <div className="h-full aspect-square border border-solid border-gray-600 border-r-0"></div>
        <input 
          id={id} 
          type="text" 
          className="h-full border border-solid border-gray-600"
        />
      </div>
    </div>
  )
}

export default LoginInput