import { NavLink } from "react-router-dom";
import styles from "./iconLink.module.css";

type Props = {
  to: string;
  label: string;
  icon: JSX.Element;
};

const IconLink = ({ to, label, icon }: Props) => {
  return (
    <NavLink
      id="/dashboard/empresas"
      className={`${styles.asidelink} flex gap-3 px-3 py-2 rounded-lg transition-all duration-300 hover:opacity-80`}
      to={to}
    >
      <div className="w-5 h-5">{icon}</div>
      <p className="text-sm text-neutral-700">{label}</p>
    </NavLink>
  );
};

export default IconLink;
