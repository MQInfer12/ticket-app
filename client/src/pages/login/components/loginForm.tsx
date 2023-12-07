import Button from "../../../global/components/button"
import LoginInput from "./loginInput"

const LoginForm = () => {
  return (
    <article className="w-[400px] flex flex-col items-center gap-2 border border-solid border-gray-600 p-8">
      <h2 className="border-b border-solid border-gray-600 w-full text-center pb-2 font-semibold text-2xl">Inicio de sesión</h2>
      <form className="flex flex-col gap-4 p-2 items-center" action="">
        <LoginInput />
        <LoginInput />
        <div className="flex flex-col gap-4 items-center">
          <div className="flex gap-2 items-center">
            <input 
              type="checkbox" 
              className="w-4 h-4"
            />
            <label>Recuérdame</label>
          </div>
          <Button>Iniciar sesión</Button>
        </div>
      </form>
    </article>
  )
}

export default LoginForm