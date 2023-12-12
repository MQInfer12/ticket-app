interface Props {
  icon: JSX.Element
}

const CircleButton = ({ icon }: Props) => {
  return (
    <div className="w-10 h-10 rounded-full bg-slate-50 p-3 cursor-pointer hover:opacity-80 transition-all duration-300">
      { icon }
    </div>
  )
}

export default CircleButton