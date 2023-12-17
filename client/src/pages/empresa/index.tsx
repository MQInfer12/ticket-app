import { useParams } from "react-router-dom";
import PageContainer from "../../global/components/pageContainer";
import TableContainer from "../../global/components/table/tableContainer";
import { useGet } from "../../hooks/useGet";
import { Empresa as EmpresaType } from "../../global/interfaces/api/empresa";
import Loader from "../../global/components/loader/loader";
import { useState } from "react";
import Tabs from "../../global/components/tabs/tabs";
import Contactos from "../persona/components/contactos";

type Page = "Contactos";

const Empresa = () => {
  const { id } = useParams();
  const [page, setPage] = useState<Page>("Contactos");
  const pages: Page[] = ["Contactos"];
  const { res } = useGet<EmpresaType>(`Empresa/${id}`);

  return (
    <PageContainer backRoute="/dashboard/empresas" title="Empresa">
      {!res ? (
        <Loader />
      ) : (
        <div className="flex flex-col h-full gap-5">
          <div className="flex flex-col items-center gap-2">
            <div className="flex flex-col items-center">
              <h3 className="font-bold text-lg text-neutral-800">
                {res.data.nombre}
              </h3>
              <p className="text-xs text-neutral-500">{res.data.direccion}</p>
            </div>
            <Tabs page={page} pages={pages} setPage={setPage} />
          </div>
          <div className="h-[calc(100%_-_109px)]">
            {page === "Contactos" && <Contactos />}
          </div>
        </div>
      )}
    </PageContainer>
  );
};

export default Empresa;
