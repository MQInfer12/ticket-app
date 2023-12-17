import Profile1 from "../../assets/images/profile/1.png";
import Profile2 from "../../assets/images/profile/2.png";
import Profile3 from "../../assets/images/profile/3.png";
import Profile4 from "../../assets/images/profile/4.png";

interface Props {
  userId: string;
  size?: string;
}

const ProfilePic = ({ userId, size = "w-10" }: Props) => {
  const profilePics = [Profile1, Profile2, Profile3, Profile4];
  const numbersOfId = userId.match(/\d+/g)?.join("") || 0;
  const numberIndex = Number(BigInt(numbersOfId) % 4n);
  return (
    <div className={`${size} aspect-square`}>
      <img
        src={profilePics[numberIndex]}
        className="object-cover w-full h-full rounded-full"
      />
    </div>
  );
};

export default ProfilePic;
