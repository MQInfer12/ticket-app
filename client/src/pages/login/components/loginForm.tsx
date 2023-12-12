import Button from "../../../global/components/button"
import LoginInput from "./loginInput"
import Grass from "../../../assets/images/login/grass.jpg"
import User from "../../../icons/iconProfile"

const LoginForm = () => {
  return (
    <div className="w-[500px] flex rounded-3xl overflow-hidden pl-8"
    style={{ 
      background: 'linear-gradient(to right top, #1e293b, #283346, #323e51, #3c4a5d, #475569)'
    }}
    >
      <div className="h-full w-[70%] flex flex-col gap-2 py-14">
        <h2 className="text-slate-50 text-2xl">Inicio de sesion</h2>
        <form action="" className="flex flex-col gap-5">
          <div className="flex gap-1">
            <label htmlFor="" className="text-slate-200">No tienes una cuenta?</label>
            <a href="#" className="text-emerald-400">Crear cuenta</a>
          </div>
          <div className="flex flex-col gap-2">
            <LoginInput label="Usuario" type="text" icon={<User/>}/>
            <LoginInput label="Contraseña" type="text" icon={<User/>}/>
          </div>
          <Button children="Iniciar sesion"/>
        </form>
      </div>
      <div className="flex-1 relative">
        <img src={Grass} alt="grass" className="h-full object-cover opacity-30"/>
      </div>
    </div>
  )
}

export default LoginForm