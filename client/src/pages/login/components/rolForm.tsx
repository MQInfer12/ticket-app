import { useGet } from "../../../hooks/useGet";
import Card from "./card";
import { UserRol } from "../../../global/interfaces/api/rolUsuario";
import { useNavigate } from "react-router-dom";
import { successAlert } from "../../../global/utils/alerts";

interface Props {
  id: string;
}

const RolForm = ({ id }: Props) => {
  const navigate = useNavigate();
  const { res } = useGet<UserRol[]>(
    `TipoRol/GetRolesByPersona/f25e03fe-e60c-4554-9032-029712fe8820`
  );

  const handleLogin = () => {
    navigate("/dashboard/empresas");
    successAlert("Inicio de sesión correcto");
  };

  return (
    <Card title="Seleccione cuenta">
      <div className="flex flex-col gap-4">
        {res?.data.map((data) => (
          <div className="flex flex-col gap-2">
            <p className="text-neutral-50">
              {data.empresa} {data.rol}
            </p>
            <button
              onClick={handleLogin}
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
