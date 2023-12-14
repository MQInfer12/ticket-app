import IconEmpresa from "../../../../icons/iconEmpresa";
import IconPersonas from "../../../../icons/iconPersonas";
import Head from "./head";
import IconLink from "./iconLink";
import Profile from "./profile";
import Section from "./section";

const Aside = () => {
  return (
    <aside
      className="flex flex-col w-80 bg-slate-200 border-r border-solid border-slate-300 px-4 pt-6 pb-4 overflow-auto relative"
      style={{ gridArea: "aside" }}
    >
      <Head />
      <div className="flex flex-col justify-between flex-1">
        <div className="flex-1 overflow-auto">
          <Section title="MAIN">
            <IconLink
              icon={<IconEmpresa />}
              label="Empresas"
              to="/dashboard/empresas"
            />
            <IconLink
              icon={<IconPersonas />}
              label="Personas"
              to="/dashboard/personas"
            />
          </Section>
        </div>
        <Profile />
      </div>
    </aside>
  );
};

export default Aside;
