import ProfilePic from "../../../../assets/images/dashboard/defaultProfile.jpg";
import IconNext from "../../../../icons/iconNext";

const Profile = () => {
  return (
    <div className="p-3 flex justify-between bg-slate-50 items-center rounded-lg pr-8 cursor-pointer select-none transition-all duration-300 hover:opacity-80">
      <div className="flex items-center gap-2">
        <div className="w-10 h-10">
          <img className="w-full h-full rounded-full object-cover" src={ProfilePic} />
        </div>
        <div className="flex flex-col">
          <b className="text-neutral-800 text-sm">Brooklyn Simmons</b>
          <p className="text-neutral-700 text-[10px]">simmons@gmail.com</p>
        </div>
      </div>
      <div className="h-4 w-4">
        <IconNext />
      </div>
    </div>
  );
};

export default Profile;
