import Card from "react-bootstrap/Card";
import Col from "react-bootstrap/Col";
import IGameData from "../../../interfaces/IGameData";
import "../../../assets/css/components/page/GameSelect/GameCard.css";

interface IProps {
  game: IGameData;
  isSelected: boolean;
  isDisabled: boolean;
  selectFunc: (id: string) => void;
}

export default function GameCard({
  game,
  isSelected,
  isDisabled,
  selectFunc,
}: IProps) {
  return (
    <Col xs={6} sm={6} md={4} lg={3} className="game-card__card-col">
      <Card
        role="button"
        onClick={() => selectFunc(game.id)}
        bg={isSelected ? "primary" : "light"}
        text={isSelected ? "light" : isDisabled ? "muted" : "dark"}
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
