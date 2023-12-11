import { useState } from "react";
import { NavLink } from "react-router-dom";

type Props = {
  to: string
  label: string
  icons: [JSX.Element, JSX.Element]
}

const IconLink = ({ to, label, icons }: Props) => {
  const [active, setActive] = useState(false);
  return (
    <NavLink 
      className={({ isActive }) => {
        setActive(isActive);
        return `flex gap-3 px-3 py-2 rounded-lg transition-all duration-300 ${isActive ? "bg-slate-50" : ""}`
      }}
      to={to}
    >
      <div className="w-5 h-5">
        {active ? icons[1] : icons[0]}
      </div>
      <p className="text-sm text-neutral-700">{label}</p>
    </NavLink>
  )
}

export default IconLink