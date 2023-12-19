import Grass from "../../../assets/images/login/grass.jpg";


interface Props {
  title: string;
  children: React.ReactNode;
}

const Card = ({title, children}: Props) => {
  return (
    <section
    className="w-[90%] h-[90%] rounded-3xl relative overflow-hidden bg-cover"
    style={{
      backgroundImage: `url(${Grass})`,
    }}
  >
    <div
      className="absolute h-full w-full"
      style={{
        backgroundImage:
          "linear-gradient(50deg, rgba(30, 41, 59, 1),rgba(40, 51, 70, 1), rgba(50, 62, 81, 1),rgba(60, 74, 93, .7),rgba(71, 85, 105, .9)",
      }}
    >
      <div className="sm:w-[600px] w-[90%] h-full flex flex-col py-6 gap-3 ml-6 sm:ml-16 mt-12">
        <h2 className="text-2xl text-slate-100">{title}</h2>
        {children}
      </div>
    </div>
  </section>
  )
}

export default Card