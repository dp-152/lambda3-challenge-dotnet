import { useState } from "react";
import Container from "react-bootstrap/Container";
import TitleCard from "../components/page/GameSelect/TitleCard";
import SelectedCounter from "../components/page/GameSelect/SelectedCounter";

export default function GameSelect() {
  const [selected, setSelected] = useState(0);

  return (
    <Container>
      <Container as="header">
        <TitleCard />
      </Container>
      <Container as="nav">
        <SelectedCounter selected={selected} />
      </Container>
    </Container>
  );
}
