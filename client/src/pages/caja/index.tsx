import { useParams } from "react-router-dom";
import { useState } from "react";
import { useGet } from "../../hooks/useGet";
import { Caja as CajaType } from "../../global/interfaces/api/cajas";
import PageContainer from "../../global/components/pageContainer";
import Loader from "../../global/components/loader/loader";
import Tabs from "../../global/components/tabs/tabs";
import User from "./components/user";

type Page = "Usuarios";

const Caja = () => {
  const { id } = useParams();
  const [page, setPage] = useState<Page>("Usuarios");
  const pages: Page[] = ["Usuarios"];
  const { res } = useGet<CajaType>(`Caja/ById/${id}`);

  return (
    <PageContainer backRoute="/dashboard/cajas" title="Cajas">
      {!res ? (
        <Loader />
      ) : (
        <div className="flex flex-col h-full gap-5">
          <div className="flex flex-col items-center gap-2">
            <div className="flex flex-col items-center">
              <h3 className="font-bold text-lg text-neutral-800">
                {res.data.cajaName}
              </h3>
              <p className="text-xs text-neutral-500">{res.data.companyName}</p>
            </div>
            <Tabs page={page} pages={pages} setPage={setPage} />
          </div>
          <div className="h-[calc(100%_-_109px)]">
            {page === "Usuarios" && <User idEmpresa={res.data.companyId} />}
          </div>
        </div>
      )}
    </PageContainer>
  );
};

export default Caja;
