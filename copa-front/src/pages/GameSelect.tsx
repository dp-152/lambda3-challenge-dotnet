import { useEffect, useState } from "react";
import Container from "react-bootstrap/Container";
import TitleCard from "../components/page/GameSelect/TitleCard";
import SelectedCounter from "../components/page/GameSelect/SelectedCounter";
import { getAllGames } from "../repository/gameApiRepository";
import IGameData from "../interfaces/IGameData";
import GameCard from "../components/page/GameSelect/GameCard";
import Row from "react-bootstrap/Row";

export default function GameSelect() {
  const [selected, setSelected] = useState(0);
  const [apiError, setApiError] = useState(false);

  const [gameList, setGameList] = useState<IGameData[] | null>(null);

  useEffect(() => {
    async function getGameData() {
      try {
        const apiData = await getAllGames();
        setGameList(apiData);
        setApiError(false);
      } catch (err) {
        console.dir(
          JSON.parse(JSON.stringify(err, Object.getOwnPropertyNames(err))),
        );
        setGameList(null);
        setApiError(true);
      }
    }

    if (!gameList) {
      getGameData();
    }
  }, [gameList]);

  return (
    <Container>
      <Container as="header">
        <TitleCard />
      </Container>
      <Container as="nav">
        <SelectedCounter selected={selected} />
      </Container>
      <Container>
        <Row>
          {gameList
            ? gameList.map(game => <GameCard game={game} />)
            : apiError ? "Error fetching game list" : "Loading..."}
        </Row>
      </Container>
    </Container>
  );
}
