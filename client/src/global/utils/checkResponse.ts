import { NavigateFunction } from "react-router-dom";
import { ApiResponse } from "../interfaces/types/apiResponse";
import { errorAlert } from "./alerts";

export const checkResponse = async <T>(
  response: Response,
  navigate: NavigateFunction
): Promise<ApiResponse<T> | null> => {
  if (response.status === 401) {
    errorAlert("No autorizado");
    navigate("/");
    return null;
  }
  const json: ApiResponse<T> = await response.json();
  if (!response.ok) {
    errorAlert(json.message);
    return null;
  }
  return json;
};
