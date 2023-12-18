import LoginInput from "./loginInput";
import UserIcon from "../../../icons/iconProfile";
import LockIcon from "../../../icons/iconLock";
import { useState } from "react";
import { setAuthCookie } from "../../../global/utils/authCookie";
import { useNavigate } from "react-router-dom";
import { User } from "../../../global/interfaces/api/user";
import { successAlert } from "../../../global/utils/alerts";
import { useUser } from "../../../store/user";
import { useRequest } from "../../../hooks/useRequest";
import Card from "./card";

interface Props {
  change: (id: string) => void;
}

const LoginForm = ({ change }: Props) => {
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
    change(token);
  };

  return (
    <Card title="Inicio Sesion">
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
    </Card>
  );
};

export default LoginForm;
