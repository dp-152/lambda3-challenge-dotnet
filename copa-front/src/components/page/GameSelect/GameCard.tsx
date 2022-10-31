import Card from "react-bootstrap/Card";
import Col from "react-bootstrap/Col";
import IGameData from "../../../interfaces/IGameData";
import "../../../assets/css/components/page/GameSelect/GameCard.css";

interface IProps {
  game: IGameData;
}

export default function GameCard({ game }: IProps) {
  return (
    <Col xs={6} sm={6} md={4} lg={3} className="game-card__card-col">
      <Card
        bg="light"
        className="game-card__card"
      >
        <Card.Title>{game.title}</Card.Title>
        <Card.Subtitle className="game-card__card-subtitle">
          {game.year} - {game.score}
        </Card.Subtitle>
      </Card>
    </Col>
  );
}
