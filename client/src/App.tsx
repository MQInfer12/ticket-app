import { HashRouter, Routes, Route } from "react-router-dom";
import Login from "./pages/login";
import Dashboard from "./layouts/dashboard";
import Empresas from "./pages/empresas";
import Personas from "./pages/personas";
import Persona from "./pages/persona";

function App() {
  return (
    <HashRouter>
      <Routes>
        <Route path="/" element={<Login />} />
        <Route path="/dashboard" element={<Dashboard />}>
          <Route path="/dashboard/empresas" element={<Empresas />} />
          <Route path="/dashboard/personas" element={<Personas />} />
          <Route path="/dashboard/persona/:id" element={<Persona />} />
        </Route>
      </Routes>
    </HashRouter>
  );
}

export default App;
