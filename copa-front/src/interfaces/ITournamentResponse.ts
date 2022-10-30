import IGameData from "./IGameData";

interface ITournamentResponse {
  winner: IGameData,
  runnerUp: IGameData,
}

export default ITournamentResponse;
