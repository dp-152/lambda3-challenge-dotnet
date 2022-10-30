import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";
import Button from "react-bootstrap/Button";
import isPlural from "../../../util/isPlural";

interface IProps {
  selected: number;
}

export default function SelectedCounter({ selected }: IProps) {
  return (
    <Row>
      <Col>
        <h4>
          {isPlural(selected) ? "Selecionados" : "Selecionado"} {selected}{" "}
          {isPlural(selected) ? "games" : "game"}
        </h4>
      </Col>
      <Col xs={8} />
      <Col style={{ textAlign: "right" }}>
        <Button>Gerar Meu Campeonato</Button>
      </Col>
    </Row>
  );
}
