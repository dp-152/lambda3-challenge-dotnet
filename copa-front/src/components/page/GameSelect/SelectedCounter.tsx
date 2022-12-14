import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";
import Button from "react-bootstrap/Button";
import isPlural from "../../../util/isPlural";
import { useNavigate } from "react-router-dom";

interface IProps {
  selected: string[];
}

export default function SelectedCounter({ selected }: IProps) {
  const navigate = useNavigate();
  const selectedCount = selected.length;
  return (
    <Row>
      <Col>
        <h4>
          {isPlural(selectedCount) ? "Selecionados" : "Selecionado"}{" "}
          {selectedCount} {isPlural(selectedCount) ? "games" : "game"}
        </h4>
      </Col>
      <Col xs={8} />
      <Col style={{ textAlign: "right" }}>
        <Button
          onClick={() => navigate("/tournament-result", { state: selected })}
          disabled={selectedCount < 8}
          variant={selectedCount < 8 ? "secondary" : "primary"}
        >
          Gerar Meu Campeonato
        </Button>
      </Col>
    </Row>
  );
}
