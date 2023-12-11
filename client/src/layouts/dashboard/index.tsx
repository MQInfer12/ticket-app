import { Outlet } from "react-router-dom"
import Aside from "./components/aside/aside"
import Header from "./components/header"
import styles from "./dashboard.module.css"

const Index = () => {
  return (
    <div className={styles.container}>
      <Aside />
      <Header />
      <div
        style={{ gridArea: "outlet" }}
      >
        <Outlet />
      </div>
    </div>
  )
}

export default Index