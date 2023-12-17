import { HashRouter, Routes, Route } from "react-router-dom";
import Login from "./pages/login";
import Dashboard from "./layouts/dashboard";
import Empresas from "./pages/empresas";
import Personas from "./pages/personas";
import Persona from "./pages/persona";
import Eventos from "./pages/eventos";

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
        </Route>
      </Routes>
    </HashRouter>
  );
}

export default App;
