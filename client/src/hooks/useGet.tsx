import { useEffect, useState } from "react";
import { ApiResponse } from "../global/interfaces/types/apiResponse";
import { checkResponse } from "../global/utils/checkResponse";
import { getAuthCookie } from "../global/utils/authCookie";
import { useNavigate } from "react-router-dom";

interface ReturnValues<T> {
  res: ApiResponse<T> | null;
  getData: () => void;
  pushData: (data: T) => void;
  filterData: (filter: (value: T extends Array<infer U> ? U : T) => boolean) => void;
}

export const useGet = <T,>(
  route: string,
  send: boolean = true
): ReturnValues<T> => {
  const navigate = useNavigate();
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
      const json = await checkResponse<T>(response, navigate);
      setRes(json);
    } catch (error) {
      console.log(error);
    }
  };

  const pushData = (data: T) => {
    setRes((old) => {
      if (!old) return null;
      if (!Array.isArray(old.data)) return old;
      if (!Array.isArray(data)) return old;
      return {
        ...old,
        data: [...old.data, ...data] as T,
      };
    });
  };

  const filterData = (filter: (value: T extends Array<infer U> ? U : T) => boolean) => {
    setRes((old) => {
      if (!old) return null;
      if (!Array.isArray(old.data)) return old;
      return {
        ...old,
        data: old.data.filter((value) => filter(value)) as T,
      };
    });
  };

  useEffect(() => {
    if (send) {
      getData();
    }
  }, []);

  return {
    res,
    getData,
    pushData,
    filterData
  };
};
