import { useNavigate } from "react-router-dom";
import IconBack from "../../icons/iconBack";
import IconButton from "./buttons/iconButton";

interface Props {
  title: string;
  backRoute?: string;
  children: React.ReactNode;
}

const PageContainer = ({ title, backRoute, children }: Props) => {
  const navigate = useNavigate();

  return (
    <div className="p-6 h-[calc(100dvh_-_80px)] flex flex-col overflow-hidden">
      <div className="flex items-center mb-6 gap-3">
        {backRoute && <IconButton onClick={() => navigate(backRoute)} icon={<IconBack />} />}
        <h2 className="font-bold text-2xl text-neutral-800">{title}</h2>
      </div>
      {children}
    </div>
  );
};

export default PageContainer;
