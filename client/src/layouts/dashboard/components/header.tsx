import CircleButton from "../../../global/components/circleButton"
import IconHelp from "../../../icons/iconHelp"

const Header = () => {
  return (
    <header
      style={{ gridArea: "header" }}
      className="h-20 border-b border-solid border-slate-300 flex justify-between p-6 items-center"
    >
      <div className="flex flex-col">
        <small className="text-neutral-500">Bienvenido,</small>
        <b className="text-neutral-800">Brooklin Simons</b>
      </div>
      <div>
        <CircleButton 
          icon={<IconHelp />}
        />
      </div>
    </header>
  )
}

export default Header