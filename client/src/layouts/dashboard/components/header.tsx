import { useNavigate } from "react-router-dom";
import CircleButton from "../../../global/components/circleButton";
import {
  deleteAuthCookie,
  getAuthCookie,
} from "../../../global/utils/authCookie";
import IconLogout from "../../../icons/iconLogout";
import { errorAlert, successAlert } from "../../../global/utils/alerts";
import { useUser } from "../../../store/user";

const Header = () => {
  const navigate = useNavigate();
  const { user, logout } = useUser();

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

  return (
    <header
      style={{ gridArea: "header" }}
      className="h-20 border-b border-solid border-slate-300 flex justify-between p-6 items-center"
    >
      <div className="flex flex-col">
        <small className="text-neutral-500">Bienvenido,</small>
        <b className="text-neutral-800">{`${user?.nombres} ${user?.apPaterno}`}</b>
      </div>
      <div>
        <CircleButton onClick={handleLogout} icon={<IconLogout />} />
      </div>
    </header>
  );
};

export default Header;
