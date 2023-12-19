import { useParams } from "react-router-dom";
import PageContainer from "../../global/components/pageContainer";
import { useGet } from "../../hooks/useGet";
import { PersonaPage } from "../../global/interfaces/api/rolUsuario";
import Loader from "../../global/components/loader/loader.tsx";
import ProfilePic from "../../global/components/profilePic.tsx";
import Rols from "./components/rols.tsx";
import Tabs from "../../global/components/tabs/tabs.tsx";
import { useState } from "react";
import Contactos from "./components/contactos.tsx";

type Page = "Roles" | "Contactos";

const Persona = () => {
  const { id } = useParams();
  const [page, setPage] = useState<Page>("Roles");
  const pages: Page[] = ["Roles", "Contactos"];
  const { res: personaPage } = useGet<PersonaPage>(
    `Persona/GetPersonaPage/${id}`
  );

  return (
    <PageContainer backRoute="/dashboard/personas" title="Persona">
      {!personaPage ? (
        <Loader />
      ) : (
        <div className="flex flex-col h-full gap-5">
          <div className="flex flex-col items-center gap-2">
            <ProfilePic
              userId={personaPage.data.userData.idUsuario}
              size="w-20"
            />
            <div className="flex flex-col items-center">
              <h3 className="font-bold text-lg text-neutral-800">
                {personaPage?.data.userData.fullName}
              </h3>
              <p className="text-xs text-neutral-500">{`@${personaPage?.data.userData.usuario} (CI: ${personaPage?.data.userData.ci})`}</p>
            </div>
            <Tabs page={page} pages={pages} setPage={setPage} />
          </div>
          <div className="h-[calc(100%_-_197px)]">
            {page === "Roles" ? (
              <Rols
                idUser={personaPage.data.userData.idUsuario}
                empresas={personaPage.data.empresas}
                roles={personaPage.data.rols}
              />
            ) : (
              page === "Contactos" && <Contactos />
            )}
          </div>
        </div>
      )}
    </PageContainer>
  );
};

export default Persona;
