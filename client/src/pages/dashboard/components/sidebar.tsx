import Logo from "../../../assets/images/login/GoalGuardian.png";
import SidebarOptions from "./sidebarOptions";
import IconMenu from "../../../icons/iconMenu";


type Props = {}

const Sidebar = (props: Props) => {

  const sideOptions = ['Home', 'Profile', 'Messages'];

  return (
    <div className="h-full flex flex-col">
      <div className="h-20 flex items-center border border-solid border-stone-500">
        <div className="h-full p-2">
          <img src={Logo} alt="logo" className="h-full object-cover rounded-lg" />
        </div>
        <h1 className="font-semibold text-3xl">Logo</h1>
        <div className="aspect-square w-10 ml-auto mr-4">
          <IconMenu/>
        </div>
      </div>
      <div className="h-full flex-1">
        <ul className="p-2">
          {sideOptions.map((label, index) => (
            <SidebarOptions key={index} label={label}/>
          ))}
        </ul>
      </div>
    </div>
  )
}

export default Sidebar