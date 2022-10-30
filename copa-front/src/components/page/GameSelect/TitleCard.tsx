import "../../../assets/css/components/page/GameSelect/TitleCard.css";
import Card from "react-bootstrap/Card";
import Container from "react-bootstrap/Container";

export default function TitleCard() {
  return (
    <Card bg="light" className="title-card__card">
      <Card.Subtitle>Challenge Games</Card.Subtitle>
      <Card.Title as="h2">Fase de Seleção</Card.Title>
      <hr />
      <Container className="title-card__card__text">
        <Card.Text>
          Selecione 8 games que você deseja que entrem na competição e depois
          pressione o botão Gerar Meu Campeonato para prosseguir
        </Card.Text>
      </Container>
    </Card>
  );
}
