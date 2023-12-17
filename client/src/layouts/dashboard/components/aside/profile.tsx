import ProfilePic from "../../../../global/components/profilePic";
import IconNext from "../../../../icons/iconNext";
import { useUser } from "../../../../store/user";

const Profile = () => {
  const { user } = useUser();

  if (!user) return null;
  return (
    <div className="p-3 flex justify-between bg-slate-50 items-center rounded-lg pr-8 cursor-pointer select-none transition-all duration-300 hover:opacity-80">
      <div className="flex items-center gap-2">
        <ProfilePic userId={user.userId} />
        <div className="flex flex-col">
          <b className="text-neutral-800 text-sm">{`${user?.personName} ${user?.personLastName}`}</b>
          <p className="text-neutral-700 text-[10px]">@{user?.userName}</p>
        </div>
      </div>
      <div className="h-4 w-4">
        <IconNext />
      </div>
    </div>
  );
};

export default Profile;
