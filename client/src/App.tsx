import { HashRouter, Routes, Route } from "react-router-dom";
import Login from "./pages/login";
import Dashboard from "./layouts/dashboard";
import Empresas from "./pages/empresas";
import Personas from "./pages/personas";
import Persona from "./pages/persona";
import Eventos from "./pages/eventos";
import Evento from "./pages/evento";
import Empresa from "./pages/empresa";
import Inicio from "./pages/inicio";
import ValidateRol from "./global/guard/validateRol";
import { Roles } from "./global/interfaces/types/roles";
import Cajas from "./pages/cajas";
import Caja from "./pages/caja";

function App() {
  return (
    <HashRouter>
      <Routes>
        <Route path="/" element={<Login />} />
        <Route path="/dashboard" element={<Dashboard />}>
          <Route path="/dashboard/inicio" element={<Inicio />} />
          <Route
            path="/dashboard/empresas"
            element={
              <ValidateRol roles={[Roles.superadmin]}>
                <Empresas />
              </ValidateRol>
            }
          />
          <Route
            path="/dashboard/empresas/:id"
            element={
              <ValidateRol roles={[Roles.superadmin, Roles.adminEmpresa]}>
                <Empresa />
              </ValidateRol>
            }
          />
          <Route
            path="/dashboard/personas"
            element={
              <ValidateRol roles={[Roles.superadmin, Roles.adminEmpresa]}>
                <Personas />
              </ValidateRol>
            }
          />
          <Route
            path="/dashboard/personas/:id"
            element={
              <ValidateRol roles={[Roles.superadmin, Roles.adminEmpresa]}>
                <Persona />
              </ValidateRol>
            }
          />
          <Route path="/dashboard/eventos" element={<Eventos />} />
          <Route path="/dashboard/eventos/:id" element={<Evento />} />
          <Route path="/dashboard/cajas" element={<Cajas />} />
          <Route path="/dashboard/cajas/:id" element={<Caja />} />
          <Route path="/dashboard/*" element={<div>404</div>} />
        </Route>
      </Routes>
    </HashRouter>
  );
}

export default App;
