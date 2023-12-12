import { useEffect, useState } from "react";
import { ApiResponse } from "../global/interfaces/types/apiResponse";
import { checkResponse } from "../global/utils/checkResponse";
import { getAuthCookie } from "../global/utils/authCookie";

interface ReturnValues<T> {
  res: ApiResponse<T> | null;
  getData: () => void;
}

export const useGet = <T,>(
  route: string,
  send: boolean = true
): ReturnValues<T> => {
  const [res, setRes] = useState<ApiResponse<T> | null>(null);

  const getData = async () => {
    try {
      const token = getAuthCookie();
      const response = await fetch(
        import.meta.env.VITE_BACKEND + route,
        token
          ? {
              headers: {
                Accept: "application/json",
                Authorization: "Bearer " + token,
              },
            }
          : undefined
      );
      const json = await checkResponse<T>(response);
      setRes(json);
    } catch (error) {
      console.log(error);
    }
  };

  useEffect(() => {
    if (send) {
      getData();
    }
  }, []);

  return {
    res,
    getData,
  };
};
