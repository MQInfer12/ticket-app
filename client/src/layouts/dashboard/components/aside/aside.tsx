import IconEmpresa, { IconEmpresaActive } from "../../../../icons/iconEmpresa"
import IconPersonas, { IconPersonasActive } from "../../../../icons/iconPersonas"
import Head from "./head"
import IconLink from "./iconLink"
import Section from "./section"

const Aside = () => {
  return (
    <aside
      className="w-80 bg-slate-200 border-r border-solid border-slate-300 px-4 pt-6"
      style={{ gridArea: "aside" }}
    >
      <Head />
      <Section title="MAIN">
        <IconLink 
          icons={[<IconEmpresa />, <IconEmpresaActive />]}
          label="Empresas"
          to="/dashboard/empresas"
        />
        <IconLink 
          icons={[<IconPersonas />, <IconPersonasActive />]}
          label="Personas"
          to="/dashboard/personas"
        />
      </Section>
    </aside>
  )
}

export default Aside