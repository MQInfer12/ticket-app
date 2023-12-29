import IconAdd from "../../../icons/iconAdd";
import IconReload from "../../../icons/iconReload";
import IconSearch from "../../../icons/iconSearch";
import IconX from "../../../icons/iconX";
import IconPdf from "../../../icons/iconPdf";
import ControlButton from "./controlButton";

interface Props {
  filter: [string, React.Dispatch<React.SetStateAction<string>>];
  reload?: () => void;
  add?: () => void;
  viewPDF: [boolean, React.Dispatch<React.SetStateAction<boolean>>];
  loading: boolean;
}

const TableControls = ({ filter, reload, add, viewPDF, loading }: Props) => {
  const [filterValue, setFilter] = filter;
  const [viewPDFValue, setViewPDF] = viewPDF;

  return (
    <div className="w-full flex pb-4">
      <div className="w-full relative">
        <div className="absolute left-0 top-2/4 -translate-y-2/4 w-10 h-10 p-3 pointer-events-none">
          <IconSearch />
        </div>
        <input
          disabled={viewPDFValue}
          className="w-full px-4 pl-10 outline-none h-10 text-sm border border-solid border-slate-300 text-neutral-700 placeholder:text-neutral-400 disabled:bg-slate-200 transition-all duration-300"
          type="text"
          placeholder="Buscar..."
          value={filterValue}
          onChange={(e) => setFilter(e.target.value)}
        />
      </div>
      {filterValue && (
        <ControlButton
          disabled={viewPDFValue}
          title="Eliminar búsqueda"
          onClick={() => setFilter("")}
          icon={<IconX />}
        />
      )}
      {!!add && (
        <ControlButton
          disabled={viewPDFValue}
          title="Añadir dato"
          onClick={add}
          icon={<IconAdd />}
        />
      )}
      {!!reload && (
        <ControlButton
          disabled={viewPDFValue}
          title="Recargar datos"
          onClick={reload}
          icon={<IconReload />}
        />
      )}
      <ControlButton
        disabled={loading}
        title="Exportar PDF"
        onClick={() => setViewPDF((old) => !old)}
        icon={<IconPdf />}
      />
    </div>
  );
};

export default TableControls;
