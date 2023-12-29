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
import Cuentas from "./pages/cuentas";
import VerEvento from "./pages/verEvento";
import Gracias from "./pages/gracias";
import Ingresos from "./pages/ingresos";
import Categorias from "./pages/categorias";
import Compras from "./pages/compras";
import Categoria from "./pages/categoria";

function App() {
  return (
    <HashRouter>
      <Routes>
        <Route path="/" element={<Login />} />
        <Route path="/dashboard" element={<Dashboard />}>
          <Route path="/dashboard/inicio" element={<Inicio />} />

          <Route
            path="/dashboard/compras"
            element={
              <ValidateRol roles={[Roles.cliente]}>
                <Compras />
              </ValidateRol>
            }
          />

          <Route
            path="/dashboard/inicio/verEvento/:id"
            element={
              <ValidateRol roles={[Roles.cliente]}>
                <VerEvento />
              </ValidateRol>
            }
          />
          <Route
            path="/dashboard/ingresos"
            element={
              <ValidateRol roles={[Roles.superadmin, Roles.adminEmpresa]}>
                <Ingresos />
              </ValidateRol>
            }
          />
          <Route
            path="/dashboard/inicio/gracias/:id"
            element={
              <ValidateRol roles={[Roles.cliente]}>
                <Gracias />
              </ValidateRol>
            }
          />
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
          <Route
            path="/dashboard/eventos"
            element={
              <ValidateRol roles={[Roles.superadmin, Roles.adminEmpresa]}>
                <Eventos />
              </ValidateRol>
            }
          />
          <Route
            path="/dashboard/eventos/:id"
            element={
              <ValidateRol roles={[Roles.superadmin, Roles.adminEmpresa]}>
                <Evento />
              </ValidateRol>
            }
          />
          <Route
            path="/dashboard/cajas"
            element={
              <ValidateRol roles={[Roles.superadmin, Roles.adminEmpresa]}>
                <Cajas />
              </ValidateRol>
            }
          />
          <Route
            path="/dashboard/cajas/:id"
            element={
              <ValidateRol roles={[Roles.superadmin, Roles.adminEmpresa]}>
                <Caja />
              </ValidateRol>
            }
          />
          <Route
            path="/dashboard/cuentas"
            element={
              <ValidateRol roles={[Roles.superadmin, Roles.adminEmpresa]}>
                <Cuentas />
              </ValidateRol>
            }
          />
          <Route
            path="/dashboard/categorias"
            element={
              <ValidateRol
                roles={[
                  Roles.superadmin,
                  Roles.adminEmpresa,
                  Roles.adminTienda,
                ]}
              >
                <Categorias />
              </ValidateRol>
            }
          />
          <Route
            path="/dashboard/categorias/:id"
            element={
              <ValidateRol
                roles={[
                  Roles.superadmin,
                  Roles.adminEmpresa,
                  Roles.adminTienda,
                ]}
              >
                <Categoria />
              </ValidateRol>
            }
          />
          <Route path="/dashboard/*" element={<div>404</div>} />
        </Route>
      </Routes>
    </HashRouter>
  );
}

export default App;
