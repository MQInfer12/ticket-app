import { useParams } from "react-router-dom";
import PageContainer from "../../global/components/pageContainer";
import { useGet } from "../../hooks/useGet";
import { EmpresaRes } from "../../global/interfaces/api/empresa";
import Loader from "../../global/components/loader/loader";
import { useState } from "react";
import Tabs from "../../global/components/tabs/tabs";
import Contactos from "../persona/components/contactos";
import Personas from "./components/personas";
import { useUser } from "../../store/user";
import { Roles } from "../../global/interfaces/types/roles";

type Page = "Personas" | "Contactos";

const Empresa = () => {
  const { id } = useParams();
  const [page, setPage] = useState<Page>("Personas");
  const pages: Page[] = ["Personas", "Contactos"];
  const { res } = useGet<EmpresaRes>(`Empresa/${id}`);
  const { user } = useUser();

  return (
    <PageContainer backRoute={user?.roleName === Roles.superadmin ? "/dashboard/empresas" : undefined} title="Empresa">
      {!res ? (
        <Loader />
      ) : (
        <div className="flex flex-col h-full gap-5">
          <div className="flex flex-col items-center gap-2">
            <div className="flex flex-col items-center">
              <h3 className="font-bold text-lg text-neutral-800">
                {res.data.empresa.nombre}
              </h3>
              <p className="text-xs text-neutral-500">{res.data.empresa.direccion}</p>
            </div>
            <Tabs page={page} pages={pages} setPage={setPage} />
          </div>
          <div className="h-[calc(100%_-_109px)]">
            {page === "Personas" && <Personas roles={res.data.roles} personas={res.data.personas} />}
            {page === "Contactos" && <Contactos canEdit />}
          </div>
        </div>
      )}
    </PageContainer>
  );
};

export default Empresa;
