import { useEffect, useState } from "react";
import Container from "react-bootstrap/Container";
import TitleCard from "../components/page/GameSelect/TitleCard";
import SelectedCounter from "../components/page/GameSelect/SelectedCounter";
import { getAllGames } from "../repository/gameApiRepository";
import IGameData from "../interfaces/IGameData";
import GameCard from "../components/page/GameSelect/GameCard";
import Row from "react-bootstrap/Row";
import useGameSelect from "../hooks/useGameSelect";
import { Navigate } from "react-router-dom";

export default function GameSelect() {
  const [apiError, setApiError] = useState(false);

  const [gameList, setGameList] = useState<IGameData[] | null>(null);
  const [selected, setSelected] = useGameSelect([]);

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
        <SelectedCounter
          selected={selected}
        />
      </Container>
      <Container>
        <Row>
          {gameList
            ? gameList.map(game => (
              <GameCard
                key={game.id}
                game={game}
                isSelected={!!selected.find(el => game.id === el)}
                selectFunc={setSelected}
              />
            ))
            : apiError ? "Error fetching game list" : "Loading..."}
        </Row>
      </Container>
    </Container>
  );
}
