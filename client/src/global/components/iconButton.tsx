interface Props {
  icon: JSX.Element
}

const IconButton = ({ icon }: Props) => {
  return (
    <div 
      className="w-5 h-5 cursor-pointer"
    >{icon}</div>
  )
}

export default IconButton