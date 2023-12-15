import { useNavigate } from "react-router-dom";
import { ApiResponse } from "../global/interfaces/types/apiResponse";
import { getAuthCookie } from "../global/utils/authCookie";
import { checkResponse } from "../global/utils/checkResponse";

interface Options {
  method: string;
}

const BACKEND_URL = import.meta.env.VITE_BACKEND;

export const useRequest = () => {
  const navigate = useNavigate();

  const sendRequest = async <T,>(
    route: string,
    body: Record<string, any> | null,
    options?: Options
  ): Promise<ApiResponse<T> | null> => {
    const thisOptions: Options = {
      method: options?.method || "POST",
    };
    const token = getAuthCookie();
    const response = await fetch(`${BACKEND_URL}${route}`, {
      method: thisOptions.method,
      headers: {
        "Content-Type": "application/json",
        Accept: "application/json",
        Authorization: token ? "Bearer " + token : "",
      },
      body:
        thisOptions.method !== "GET" ? JSON.stringify(body || {}) : undefined,
    });
    const json = await checkResponse<T>(response, navigate);
    return json;
  };

  return {
    sendRequest,
  };
};
