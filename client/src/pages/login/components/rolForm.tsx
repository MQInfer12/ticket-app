import Card from "./card";
import { UserRol } from "../../../global/interfaces/api/rolUsuario";
import { useNavigate } from "react-router-dom";
import { successAlert } from "../../../global/utils/alerts";
import { useRequest } from "../../../hooks/useRequest";
import { setAuthCookie } from "../../../global/utils/authCookie";
import { User } from "../../../global/interfaces/api/user";
import { useUser } from "../../../store/user";

interface Props {
  rolesEmpresa: UserRol[];
}

const RolForm = ({ rolesEmpresa }: Props) => {
  const navigate = useNavigate();
  const { sendRequest } = useRequest();
  const { setUser } = useUser();

  const handleRolLogin = async (id: string) => {
    const token = await sendRequest<string>(`User/LoginByRole/${id}`, null, {
      method: "GET",
    });
    if (!token) return;
    setAuthCookie(token.data);
    const resUser = await sendRequest<User>("User/GetUserByToken", null, {
      method: "GET",
    });
    if (!resUser) return;
    const user = resUser.data;
    setUser(user);
    navigate("/dashboard/inicio");
    successAlert("Inicio de sesión correcto");
  };

  return (
    <Card title="Seleccione cuenta">
      <div className="flex flex-col gap-4">
        {rolesEmpresa.map((data) => (
          <div className="flex flex-col gap-2" key={data.id}>
            <p className="text-neutral-50">
              {data.empresa} {data.rol}
            </p>
            <button
              onClick={() => handleRolLogin(data.id)}
              className="text-slate-800 bg-emerald-400 rounded-2xl h-8 w-[80%] hover:bg-emerald-500 focus:outline-none focus:bg-emerald-500"
            >
              Ingresar
            </button>
          </div>
        ))}
      </div>
    </Card>
  );
};

export default RolForm;
