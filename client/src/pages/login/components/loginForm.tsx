import LoginInput from "./loginInput";
import Grass from "../../../assets/images/login/grass.jpg";
import UserIcon from "../../../icons/iconProfile";
import LockIcon from "../../../icons/iconLock";
import { useState } from "react";
import { setAuthCookie } from "../../../global/utils/authCookie";
import { useNavigate } from "react-router-dom";
import { User } from "../../../global/interfaces/api/user";
import { successAlert } from "../../../global/utils/alerts";
import { useUser } from "../../../store/user";
import { useRequest } from "../../../hooks/useRequest";

const LoginForm = () => {
  const navigate = useNavigate();
  const { setUser } = useUser();
  const { sendRequest } = useRequest();

  const [form, setForm] = useState({
    usuario: "",
    contrasenia: "",
  });

  const handleSend = async (
    e: React.MouseEvent<HTMLButtonElement, MouseEvent>
  ) => {
    e.preventDefault();
    const resLogin = await sendRequest<string>("User/Login", form);
    if (!resLogin) return;
    const token = resLogin.data;
    setAuthCookie(token);
    const resUser = await sendRequest<User>("User/GetUserByToken", null, {
      method: "GET",
    });
    if (!resUser) return;
    const user = resUser.data;
    setUser(user);
    navigate("/dashboard/empresas");
    successAlert("Inicio de sesión correcto");
  };

  return (
    <section
      className="w-[90%] h-[90%] rounded-3xl relative overflow-hidden bg-cover"
      style={{
        backgroundImage: `url(${Grass})`,
      }}
    >
      <div
        className="absolute h-full w-full"
        style={{
          backgroundImage:
            "linear-gradient(50deg, rgba(30, 41, 59, 1),rgba(40, 51, 70, 1), rgba(50, 62, 81, 1),rgba(60, 74, 93, .7),rgba(71, 85, 105, .9)",
        }}
      >
        <div className="sm:w-[600px] w-[90%] h-full flex flex-col py-6 gap-3 ml-6 sm:ml-16 mt-12">
          <h2 className="text-2xl text-slate-100">Inicio sesion</h2>
          <form action="" className="flex flex-col gap-6">
            <div className="flex gap-1 text-xs">
              <label className="text-slate-200">No tienes una cuenta?</label>
              <a href="#" className="text-emerald-400">
                Crear cuenta
              </a>
            </div>
            <div className="flex flex-col gap-6 w-[80%]">
              <LoginInput
                label="Usuario"
                type="text"
                icon={<UserIcon />}
                value={form.usuario}
                onChange={(e) =>
                  setForm((old) => ({ ...old, usuario: e.target.value }))
                }
              />
              <LoginInput
                label="Contraseña"
                type="password"
                icon={<LockIcon />}
                value={form.contrasenia}
                onChange={(e) =>
                  setForm((old) => ({ ...old, contrasenia: e.target.value }))
                }
              />
            </div>
            <button
              onClick={handleSend}
              className="text-slate-800 bg-emerald-400 rounded-2xl h-8 w-[80%] hover:bg-emerald-500 focus:outline-none focus:bg-emerald-500"
            >
              Iniciar sesion
            </button>
          </form>
        </div>
      </div>
    </section>
  );
};

export default LoginForm;
