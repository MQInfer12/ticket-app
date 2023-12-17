import { Navigate, Outlet } from "react-router-dom";
import Aside from "./components/aside/aside";
import Header from "./components/header";
import styles from "./dashboard.module.css";
import { useUser } from "../../store/user";
import Loader from "../../global/components/loader/loader";
import { useGet } from "../../hooks/useGet";
import { useEffect, useState } from "react";
import { User } from "../../global/interfaces/api/user";

const Index = () => {
  const { state, setUser } = useUser();
  const { res } = useGet<User>("User/GetUserByToken", state === "loading");
  const [openAside, setOpenAside] = useState(false);

  useEffect(() => {
    if (res) {
      setUser(res.data);
    }
  }, [res]);

  if (state === "loading")
    return (
      <div className="w-screen h-screen">
        <Loader text="Cargando datos del usuario..." />
      </div>
    );
  if (state === "unlogged") return <Navigate to="/" />;
  return (
    <div className={`${styles.container} bg-slate-100`}>
      <Aside open={openAside} setOpen={setOpenAside} />
      <Header setOpen={setOpenAside} />
      <div style={{ gridArea: "outlet" }}>
        <Outlet />
      </div>
    </div>
  );
};

export default Index;
