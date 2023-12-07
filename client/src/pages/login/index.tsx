import Grass from "../../assets/images/login/grass.jpg";
import Logo from "../../assets/images/login/GoalGuardian.png";
import QR from "../../assets/images/login/QR.png";
import LoginForm from "./components/loginForm";

const Index = () => {
  return (
    <section className="w-screen h-screen flex">
      <div className="h-full w-[30%]">
        <img 
          className="h-full object-cover"
          src={Grass} 
          alt="grass" 
        />
      </div>
      <div className="h-full flex-1 bg-gray-200 relative flex items-center justify-center">
        <div className="absolute top-8 left-8 flex items-center gap-4">
          <img src={Logo} alt="logo" className="w-48 rounded-lg" />
          <h1 className="font-semibold text-3xl">Goal Guardian</h1>
        </div>
        <LoginForm />
        <div className="absolute bottom-8 left-8 flex flex-col items-center">
          <img src={QR} alt="qr" className="w-36" />
          <small>Copyright am2ps 2023</small>
        </div>
      </div>
    </section>
  )
}

export default Index