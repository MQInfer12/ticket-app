import { Navigate } from "react-router-dom";
import { useUser } from "../../store/user";
import { Roles } from "../interfaces/types/roles";

interface Props {
  roles: Roles[];
  children: JSX.Element;
}

const ValidateRol = ({ children, roles }: Props) => {
  const { user } = useUser();

  if (!user) return <Navigate to="/" />;
  if (!roles.includes(user.roleName))
    return <Navigate to="/dashboard/inicio" />;
  return children;
};

export default ValidateRol;
