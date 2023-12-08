type Props = {
  label: string
}

const sidebarOptions = ({label}: Props) => {
  return (
    <li className="p-5">
      <label htmlFor="" className="text-teal-400">{label}</label>
    </li>
  )
}

export default sidebarOptions