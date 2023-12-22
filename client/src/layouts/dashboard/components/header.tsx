import { useNavigate } from "react-router-dom";
import CircleButton from "../../../global/components/buttons/circleButton";
import {
  deleteAuthCookie,
  getAuthCookie,
} from "../../../global/utils/authCookie";
import IconLogout from "../../../icons/iconLogout";
import { errorAlert, successAlert } from "../../../global/utils/alerts";
import { useUser } from "../../../store/user";
import IconMenu from "../../../icons/iconMenu";
import Button from "../../../global/components/buttons/button";
import { UserRol } from "../../../global/interfaces/api/rolUsuario";
import { useGet } from "../../../hooks/useGet";
import { useRequest } from "../../../hooks/useRequest";
import { setAuthCookie } from "../../../global/utils/authCookie";
import { User } from "../../../global/interfaces/api/user";

interface Props {
  setOpen: React.Dispatch<React.SetStateAction<boolean>>;
}

const Header = ({ setOpen }: Props) => {
  const navigate = useNavigate();
  const { user, logout, setUser } = useUser();
  const { res } = useGet<UserRol[]>(`RolUsuario/GetRolesByUser/${user?.userId}`);
  const { sendRequest } = useRequest()
  const resRol = res?.data.filter(x => x.rol != user?.roleName)
  
  const handleLogout = async () => {
    const token = getAuthCookie();
    if (!token) {
      navigate("/");
      errorAlert("No autorizado");
      return;
    }
    deleteAuthCookie();
    logout();
    navigate("/");
    successAlert("Cierre de sesión correcto");
  };

  const handleChangeRol = async (e:React.ChangeEvent<HTMLSelectElement>) => {
    const token = await sendRequest<string>(`User/LoginByRole/${e.target.value}`, null, {
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
    successAlert("Cambio de usuario realizado");
  }

  return (
    <header
      style={{ gridArea: "header" }}
      className="h-20 border-b border-solid border-slate-300 flex justify-between p-6 items-center"
    >
      <div className="flex gap-5">
        <div className="block lg:hidden">
          <CircleButton onClick={() => setOpen(true)} icon={<IconMenu />} />
        </div>
        <div className="flex flex-col">
          <small className="text-neutral-500">
            {user?.companyName} ({user?.roleName}),
          </small>
          <b className="text-neutral-800">{`${user?.personName} ${user?.personLastName}`}</b>
        </div>
      </div>
      <div className="group relative">
        <CircleButton onClick={() => null} icon={<IconLogout />} />
        <div className="hidden bg-slate-200 absolute w-auto h-52 right-6 top-8 rounded-lg border border-slate-300 group-hover:flex flex-col items-center z-10 gap-2">
          <p>Cambiar rol</p>
          <select onChange={(e) => handleChangeRol(e)}>
            <option value={user?.idRolUser}>{`${user?.companyName ? user?.companyName : ""} (${user?.roleName})`}</option>
            {resRol?.map((rol) => (
              <option key={rol.id} value={rol.id}>{`${rol.empresa ? rol.empresa:""} (${rol.rol})`}</option>
            ))}
          </select>
          <Button onClick={handleLogout}>Cerrar sesion</Button>
        </div>
      </div>
    </header>
  );
};

export default Header;
