import "./assets/css/bootstrap.css";
import { BrowserRouter, Navigate, Route, Routes } from "react-router-dom";
import GameSelect from "./pages/GameSelect";
import TournamentResult from "./pages/TournamentResult";

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<GameSelect />} />
        <Route path="/tournament-result" element={<TournamentResult />} />
        <Route path="*" element={<Navigate to="/" replace />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;
