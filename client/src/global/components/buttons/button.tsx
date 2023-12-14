interface Props extends React.ButtonHTMLAttributes<HTMLButtonElement> {
  bg?: string;
}

const Button = ({ bg = "bg-emerald-500", ...props }: Props) => {
  return (
    <button
      {...props}
      className={`text-white px-4 py-2 rounded-lg hover:opacity-80 transition-all duration-300 ${bg}`}
    >
      {props.children}
    </button>
  );
};

export default Button;
