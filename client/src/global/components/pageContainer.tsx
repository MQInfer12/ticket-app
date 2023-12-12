interface Props {
  title: string;
  children: React.ReactNode;
}

const PageContainer = ({ title, children }: Props) => {
  return (
    <div className="p-6 h-[calc(100dvh_-_80px)] flex flex-col overflow-hidden">
      <h2 className="mb-6 font-bold text-2xl text-neutral-800">{title}</h2>
      {children}
    </div>
  );
};

export default PageContainer;
