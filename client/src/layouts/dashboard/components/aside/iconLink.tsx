import { useState } from "react";
import { NavLink } from "react-router-dom";

type Props = {
  to: string
  label: string
  icon: (active: boolean) => JSX.Element
}

const IconLink = ({ to, label, icon }: Props) => {
  const [active, setActive] = useState(false);
  return (
    <NavLink 
      className={({ isActive }) => {
        setActive(isActive);
        return `flex gap-3 px-3 py-2 rounded-lg transition-all duration-300 hover:opacity-80 ${isActive ? "bg-slate-50" : ""}`
      }}
      to={to}
    >
      <div className="w-5 h-5">
        {icon(active)}
      </div>
      <p className="text-sm text-neutral-700">{label}</p>
    </NavLink>
  )
}

export default IconLink