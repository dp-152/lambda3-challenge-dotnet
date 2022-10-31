import { useEffect, useState } from "react";
import { Spinner } from "react-bootstrap";
import Container from "react-bootstrap/Container";
import { Navigate, useLocation } from "react-router-dom";
import TitleCard from "../components/page/TournamentResult/TitleCard";
import TournamentResultList from "../components/page/TournamentResult/TournamentResultList"; // eslint-disable-line max-len
import ITournamentResponse from "../interfaces/ITournamentResponse";
import { runTournament } from "../repository/gameApiRepository";

export default function TournamentResult() {
  const [apiError, setApiError] = useState(false);
  const [result, setResult] = useState<ITournamentResponse | null>(null);
  const location = useLocation();

  useEffect(() => {
    async function getResult() {
      try {
        const response = await runTournament({ gameIds: location.state });
        setResult(response.data);
        setApiError(false);
      } catch (err) {
        setResult(null);
        setApiError(true);
      }
    }

    if (!result) {
      getResult();
    }
  }, [location.state, result]);

  return location.state ? (
    <Container>
      <Container as="header">
        <TitleCard />
      </Container>
      <Container as="main" style={{ textAlign: "center" }}>
        {result ? (
          <TournamentResultList
            winner={result!.winner}
            runnerUp={result!.runnerUp}
          />
        ) : (
          apiError ? (
            "Erro ao buscar resultado"
          ) : (
            <Spinner animation="border" />
          )
        )}
      </Container>
    </Container>
  ) : (
    <Navigate to="/" replace />
  );
}
