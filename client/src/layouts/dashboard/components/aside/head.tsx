import Logo from "../../../../assets/images/login/GoalGuardian.png";
import IconButton from "../../../../global/components/buttons/iconButton";
import IconMenuLeft from "../../../../icons/iconMenuLeft";

const Head = () => {
  return (
    <div className="flex justify-between items-center mb-4">
      <div className="flex items-center gap-2">
        <div className="w-10 h-10">
          <img src={Logo} alt="logo" className="rounded-lg object-cover" />
        </div>
        <h1 className="font-semibold text-neutral-700">Goal Guardian</h1>
      </div>
      <IconButton icon={<IconMenuLeft />} />
    </div>
  );
};

export default Head;
