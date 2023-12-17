import IconEmpresa from "../../../../icons/iconEmpresa";
import IconEvent from "../../../../icons/iconEvent";
import IconExpenses from "../../../../icons/iconExpenses";
import IconIncomes from "../../../../icons/iconIncomes";
import IconPersonas from "../../../../icons/iconPersonas";
import Head from "./head";
import IconLink from "./iconLink";
import Profile from "./profile";
import Section from "./section";

interface Props {
  open: boolean;
  setOpen: React.Dispatch<React.SetStateAction<boolean>>;
}

const Aside = ({ open, setOpen }: Props) => {
  let asideStyle =
    "z-40 h-screen flex flex-col w-80 bg-slate-200 border-r border-solid border-slate-300 px-4 pt-6 pb-4 fixed lg:translate-x-0 lg:relative transition-all duration-300 ";
  asideStyle += open ? "translate-x-0" : "-translate-x-full";
  return (
    <>
      <div
        onClick={() => setOpen(false)}
        className={`fixed top-0 left-0 w-screen h-screen z-30 bg-black transition-opacity duration-300 lg:opacity-0 lg:pointer-events-none ${
          open ? "opacity-20" : "opacity-0 pointer-events-none"
        }`}
      />
      <aside className={asideStyle} style={{ gridArea: "aside" }}>
        <Head setOpen={setOpen} />
        <div className="flex flex-col justify-between flex-1">
          <div className="flex-1 overflow-auto">
            <Section title="ADMINISTRACIÓN">
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
            <Section title="ENTRADAS">
              <IconLink
                icon={<IconEvent />}
                label="Eventos"
                to="/dashboard/eventos"
              />
            </Section>
            <Section title="MOVIMIENTOS">
              <IconLink
                icon={<IconIncomes />}
                label="Ingresos"
                to="/dashboard/ingresos"
              />
              <IconLink
                icon={<IconExpenses />}
                label="Egresos"
                to="/dashboard/egresos"
              />
            </Section>
          </div>
          <Profile />
        </div>
      </aside>
    </>
  );
};

export default Aside;
