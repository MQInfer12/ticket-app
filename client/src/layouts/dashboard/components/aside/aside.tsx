import RolComponent from "../../../../global/guard/rolComponent";
import { Roles } from "../../../../global/interfaces/types/roles";
import IconEmpresa from "../../../../icons/iconEmpresa";
import IconEvent from "../../../../icons/iconEvent";
import IconExpenses from "../../../../icons/iconExpenses";
import IconHome from "../../../../icons/iconHome";
import IconIncomes from "../../../../icons/iconIncomes";
import IconPersonas from "../../../../icons/iconPersonas";
import IconCaja from "../../../../icons/iconCaja";
import { useUser } from "../../../../store/user";
import Head from "./head";
import IconLink from "./iconLink";
import Profile from "./profile";
import Section from "./section";
import IconCuenta from "../../../../icons/iconCuenta";

interface Props {
  open: boolean;
  setOpen: React.Dispatch<React.SetStateAction<boolean>>;
}

const Aside = ({ open, setOpen }: Props) => {
  const { user } = useUser();

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
            <Section title="INICIO">
              <IconLink
                icon={<IconHome />}
                label="Inicio"
                to="/dashboard/inicio"
              />
            </Section>
            <RolComponent roles={[Roles.superadmin, Roles.adminEmpresa]}>
              <Section title="ADMINISTRACIÓN">
                <RolComponent roles={[Roles.superadmin]}>
                  <IconLink
                    icon={<IconEmpresa />}
                    label="Empresas"
                    to="/dashboard/empresas"
                  />
                </RolComponent>
                <RolComponent roles={[Roles.adminEmpresa]}>
                  <IconLink
                    icon={<IconEmpresa />}
                    label="Empresa"
                    to={`/dashboard/empresas/${user?.companyId}`}
                  />
                </RolComponent>
                <IconLink
                  icon={<IconPersonas />}
                  label="Personas"
                  to="/dashboard/personas"
                />
              </Section>
            </RolComponent>
            <RolComponent roles={[Roles.superadmin, Roles.adminEmpresa]}>
              <Section title="ENTRADAS">
                <IconLink
                  icon={<IconEvent />}
                  label="Eventos"
                  to="/dashboard/eventos"
                />
              </Section>
            </RolComponent>
            <Section title="MOVIMIENTOS">
              <RolComponent roles={[Roles.superadmin, Roles.adminEmpresa]}>
                <IconLink
                  icon={<IconCaja />}
                  label="Cajas"
                  to="/dashboard/cajas"
                />
              </RolComponent>
              <IconLink
                icon={<IconCuenta />}
                label="Cuentas"
                to="/dashboard/cuentas"
              />
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
