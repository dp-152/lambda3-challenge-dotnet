import Card from "react-bootstrap/Card";
import Container from "react-bootstrap/Container";

export default function TitleCard() {
  return (
    <Card bg="light" className="title-card__card">
      <Card.Subtitle>CAMPEONATO DE GAMES</Card.Subtitle>
      <Card.Title as="h2">Resultado Final</Card.Title>
      <hr />
      <Container className="title-card__card__text">
        <Card.Text>
          Veja o resultado final do campeonato de game de forma simples e rápida
        </Card.Text>
      </Container>
    </Card>
  );
}
