import { AnyFunction } from "../interfaces/anyFunction";

type Props = {
  children: string;
  onClick: AnyFunction;
};

const Button = ({ children, onClick }: Props) => {
  return (
    <button
      onClick={onClick}
      className="px-4 py-2 border border-solid border-gray-600 rounded-lg"
    >
      {children}
    </button>
  );
};

export default Button;
