import Container from "react-bootstrap/Container";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";

import "../../../assets/css/components/page/TournamentResult/TournamentResultList.css"; // eslint-disable-line max-len
import IGameData from "../../../interfaces/IGameData";

interface IProps {
  winner: IGameData;
  runnerUp: IGameData;
}

export default function TournamentResultList({ winner, runnerUp }: IProps) {
  return (
    <Container className="tournament-result-title-card__container">
      {[winner, runnerUp].map((game, idx) => (
        <Row className="tournament-result-title-card__result-row">
          <Col
            sm={4}
            md={2}
            className="tournament-result-title-card__result-col-number"
          >
            {idx + 1}º
          </Col>
          <Col className="tournament-result-title-card__result-col-title">
            {game.title}
          </Col>
        </Row>
      ))}
    </Container>
  );
}
