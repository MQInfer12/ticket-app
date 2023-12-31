import { useNavigate } from "react-router-dom";
import Logo from "../../../../assets/images/login/GoalGuardian.png";
import CircleButton from "../../../../global/components/buttons/circleButton";
import IconMenuLeft from "../../../../icons/iconMenuLeft";

interface Props {
  setOpen: React.Dispatch<React.SetStateAction<boolean>>;
}

const Head = ({ setOpen }: Props) => {
  const navigate = useNavigate();

  return (
    <div
      onClick={() => navigate("/dashboard/inicio")}
      className="flex justify-between items-center mb-4 cursor-pointer hover:bg-slate-50 transition-all duration-300 rounded-md"
    >
      <div className="flex items-center gap-2">
        <div className="w-10 h-10">
          <img src={Logo} alt="logo" className="rounded-lg object-cover" />
        </div>
        <h1 className="font-semibold text-neutral-700">Goal Guardian</h1>
      </div>
      <div className="block lg:hidden">
        <CircleButton onClick={() => setOpen(false)} icon={<IconMenuLeft />} />
      </div>
    </div>
  );
};

export default Head;
