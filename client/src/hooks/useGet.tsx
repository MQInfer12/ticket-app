import { useEffect, useState } from "react";
import { errorAlert } from "../global/utils/alerts";
import { ApiResponse } from "../global/interfaces/apiResponse";

export const useGet = <T,>(route: string) => {
  const [res, setRes] = useState<ApiResponse<T> | null>(null);

  const getData = async () => {
    try {
      const response = await fetch(import.meta.env.VITE_BACKEND + route, {
        headers: {
          "Accept": "application/json",
          "Authorization": "Bearer " + "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJleHAiOjE3MDI0NDEyOTQsImlzcyI6Imh0dHRwczovL3BvbG8uY29tIiwiYXVkIjoiaHR0dHBzOi8vcG9sby5jb20ifQ.EDX56jUl4ZACkGIHDlOexjbY6R9k7ahVi3jB6QqVUP0"
        },
      });
      if (response.status === 401) {
        return errorAlert("Sin autorización");
      }
      const json = await response.json();
      console.log(json);
      if (!response.ok) {
        return errorAlert(json.message);
      }
      setRes(json);
    } catch (error) {
      console.log(error);
    }
  };

  useEffect(() => {
    getData();
  }, []);

  return {
    res,
    getData,
  };
};
