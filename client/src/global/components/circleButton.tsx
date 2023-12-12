interface Props {
  icon: JSX.Element;
  onClick: () => any;
}

const CircleButton = ({ icon, onClick }: Props) => {
  return (
    <button onClick={onClick} className="w-10 h-10 rounded-full bg-slate-50 p-3 cursor-pointer hover:opacity-80 transition-all duration-300">
      {icon}
    </button>
  );
};

export default CircleButton;
