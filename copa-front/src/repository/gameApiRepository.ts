import IAPIResponse from "../interfaces/IAPIResponse";
import IGameData from "../interfaces/IGameData";
import axios from "axios";
import ITournamentRequest from "../interfaces/ITournamentRequest";
import ITournamentResponse from "../interfaces/ITournamentResponse";

const axiosInstance = axios.create({
  baseURL: process.env.REACT_APP_API_URL,
});

export async function getAllGames(): Promise<IGameData[]> {
  const { data: responseData } = await axiosInstance
    .get<IAPIResponse<IGameData[]>>("/game");

  return responseData.data;
}

export async function runTournament(payload: ITournamentRequest) {
  if (payload.gameIds.length !== 8) {
    throw new Error(
      `Must send exactly 8 contestants, sent ${payload.gameIds.length}`,
    );
  }

  const { data: responseData } = await axiosInstance
    .post<IAPIResponse<ITournamentResponse>>("/game", payload);

  return responseData;
}
