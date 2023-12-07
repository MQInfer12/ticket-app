import { HashRouter, Routes, Route } from "react-router-dom"
import Login from "./pages/login"

function App() {
  return (
    <HashRouter>
      <main>
        <Routes>
          <Route path="/" element={<Login />} />
        </Routes>
      </main>
    </HashRouter>
  )
}

export default App
