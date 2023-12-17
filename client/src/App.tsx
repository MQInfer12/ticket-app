import { HashRouter, Routes, Route } from "react-router-dom";
import Login from "./pages/login";
import Dashboard from "./layouts/dashboard";
import Empresas from "./pages/empresas";
import Personas from "./pages/personas";
import Persona from "./pages/persona";
import Eventos from "./pages/eventos";
import Evento from "./pages/evento";

function App() {
  return (
    <HashRouter>
      <Routes>
        <Route path="/" element={<Login />} />
        <Route path="/dashboard" element={<Dashboard />}>
          <Route path="/dashboard/empresas" element={<Empresas />} />
          <Route path="/dashboard/personas" element={<Personas />} />
          <Route path="/dashboard/personas/:id" element={<Persona />} />
          <Route path="/dashboard/eventos" element={<Eventos />} />
          <Route path="/dashboard/eventos/:id" element={<Evento />} />
          <Route path="/dashboard/*" element={<div>404</div>} />
        </Route>
      </Routes>
    </HashRouter>
  );
}

export default App;
