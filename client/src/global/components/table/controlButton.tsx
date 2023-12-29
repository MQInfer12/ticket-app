interface Props extends React.ButtonHTMLAttributes<HTMLButtonElement> {
  icon: JSX.Element;
}

const ControlButton = ({ icon, ...props }: Props) => {
  return (
    <button
      {...props}
      className="text-neutral-700 aspect-square h-10 p-[10px] bg-white border border-solid border-slate-300 cursor-pointer hover:opacity-80 transition-all duration-300 disabled:bg-slate-200 disabled:pointer-events-none"
    >
      {icon}
    </button>
  );
};

export default ControlButton;
