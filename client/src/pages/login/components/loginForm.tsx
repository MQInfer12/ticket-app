import Button from "../../../global/components/button";
import LoginInput from "./loginInput";
import Grass from "../../../assets/images/login/grass.jpg";
import UserIcon from "../../../icons/iconProfile";
import { useState } from "react";
import { sendRequest } from "../../../global/utils/sendRequest";
import { setAuthCookie } from "../../../global/utils/authCookie";
import { useNavigate } from "react-router-dom";
import { User } from "../../../global/interfaces/user";
import { successAlert } from "../../../global/utils/alerts";
import { useUser } from "../../../store/user";

const LoginForm = () => {
  const navigate = useNavigate();
  const { setUser } = useUser();

  const [form, setForm] = useState({
    usuario: "",
    contrasenia: "",
  });

  const handleSend = async (
    e: React.MouseEvent<HTMLFormElement, MouseEvent>
  ) => {
    e.preventDefault();
    const resLogin = await sendRequest<string>("User/Login", form);
    if (!resLogin) return;
    const token = resLogin.data;
    setAuthCookie(token);
    const resUser = await sendRequest<User>("User/GetUser", null, { method: "GET" });
    if (!resUser) return;
    const user = resUser.data;
    setUser(user);
    navigate("/dashboard/empresas");
    successAlert("Inicio de sesión correcto");
  };

  return (
    <div
      className="w-[500px] flex rounded-3xl overflow-hidden pl-8"
      style={{
        background:
          "linear-gradient(to right top, #1e293b, #283346, #323e51, #3c4a5d, #475569)",
      }}
    >
      <div className="h-full w-[70%] flex flex-col gap-2 py-14">
        <h2 className="text-slate-50 text-2xl">Inicio de sesion</h2>
        <form className="flex flex-col gap-5">
          <div className="flex gap-1">
            <label htmlFor="" className="text-slate-200">
              No tienes una cuenta?
            </label>
            <a href="#" className="text-emerald-400">
              Crear cuenta
            </a>
          </div>
          <div className="flex flex-col gap-2">
            <LoginInput
              value={form.usuario}
              onChange={(e) =>
                setForm((old) => ({ ...old, usuario: e.target.value }))
              }
              label="Usuario"
              type="text"
              icon={<UserIcon />}
            />
            <LoginInput
              value={form.contrasenia}
              onChange={(e) =>
                setForm((old) => ({ ...old, contrasenia: e.target.value }))
              }
              label="Contraseña"
              type="text"
              icon={<UserIcon />}
            />
          </div>
          <Button onClick={handleSend} children="Iniciar sesion" />
        </form>
      </div>
      <div className="flex-1 relative">
        <img
          src={Grass}
          alt="grass"
          className="h-full object-cover opacity-30"
        />
      </div>
    </div>
  );
};

export default LoginForm;
