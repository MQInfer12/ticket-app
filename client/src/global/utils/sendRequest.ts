import { ApiResponse } from "../interfaces/apiResponse";
import { getAuthCookie } from "./authCookie";
import { checkResponse } from "./checkResponse";

interface Options {
  method: string;
}

const BACKEND_URL = import.meta.env.VITE_BACKEND;

export const sendRequest = async <T>(
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
    body: thisOptions.method !== "GET" ? JSON.stringify(body || {}) : undefined,
  });
  const json = await checkResponse<T>(response);
  return json;
};