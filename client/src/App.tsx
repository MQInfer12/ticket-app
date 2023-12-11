import { HashRouter, Routes, Route } from "react-router-dom"
import Login from "./pages/login"
import Dashboard from "./layouts/dashboard"
import Empresa from "./pages/empresa"

function App() {
  return (
    <HashRouter>
      <Routes>
        <Route path="/" element={<Login/>} />
        <Route path="/dashboard" element={<Dashboard/>}>
          <Route path="/dashboard/empresas" element={<Empresa />} />
          <Route path="/dashboard/personas" element={<Empresa />} />
        </Route>
      </Routes>
    </HashRouter>
  )
}

export default App
