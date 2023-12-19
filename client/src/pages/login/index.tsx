import { useState } from "react";
import LoginForm from "./components/loginForm";
import RolForm from "./components/rolForm";
import { UserRol } from "../../global/interfaces/api/rolUsuario";

const Login = () => {
  const [rolesEmpresa, setRolesEmpresa] = useState<UserRol[] | null>(null);

  return (
    <section className="w-screen h-screen flex justify-center items-center bg-slate-200 px-2 sm:px-0">
      {!rolesEmpresa ? <LoginForm change={(id) => setRolesEmpresa(id)}/> : <RolForm rolesEmpresa={rolesEmpresa}/>}
    </section>
  );
};

export default Login;
