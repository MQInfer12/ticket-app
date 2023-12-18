import { useState } from "react";
import LoginForm from "./components/loginForm";
import RolForm from "./components/rolForm";

const Login = () => {
  const [id, setId] = useState("");

  return (
    <section className="w-screen h-screen flex justify-center items-center bg-slate-200 px-2 sm:px-0">
      {!id ? <LoginForm change={(id) => setId(id)}/> : <RolForm id={id}/>}
    </section>
  );
};

export default Login;
