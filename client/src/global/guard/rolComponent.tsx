import { useUser } from "../../store/user";
import { Roles } from "../interfaces/types/roles";

interface Props {
  roles: Roles[];
  children: JSX.Element | JSX.Element[];
}

const RolComponent = ({ children, roles }: Props) => {
  const { user } = useUser();

  if (!user) return null;
  if (!roles.includes(user.roleName)) return null;
  return children;
};

export default RolComponent;
