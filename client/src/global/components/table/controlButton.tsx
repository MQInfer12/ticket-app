interface Props {
  icon: JSX.Element;
  onClick: () => void;
}

const ControlButton = ({ icon, onClick }: Props) => {
  return (
    <button 
      onClick={onClick}
      className="aspect-square h-10 p-[10px] bg-white border border-solid border-slate-300 cursor-pointer hover:opacity-80 transition-all duration-300"
    >
      {icon}
    </button>
  );
};

export default ControlButton;
