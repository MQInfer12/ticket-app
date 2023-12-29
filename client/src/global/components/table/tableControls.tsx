import { DownloadTableExcel } from "react-export-table-to-excel";
import ControlButton from "./controlButton";
import { TableView } from "./tableContainer";
import IconSearch from "../../../icons/iconSearch";
import IconX from "../../../icons/iconX";
import IconAdd from "../../../icons/iconAdd";
import IconReload from "../../../icons/iconReload";
import IconPdf from "../../../icons/iconPdf";
import IconXLSX from "../../../icons/iconXLSX";
import IconList from "../../../icons/iconList";

interface Props {
  filter: [string, React.Dispatch<React.SetStateAction<string>>];
  reload?: () => void;
  add?: () => void;
  view: [TableView, React.Dispatch<React.SetStateAction<TableView>>];
  loading: boolean;
  tableCurrentRef: HTMLTableElement | null;
  reports: boolean;
}

const TableControls = ({
  filter,
  reload,
  add,
  view,
  loading,
  tableCurrentRef,
  reports,
}: Props) => {
  const [filterValue, setFilter] = filter;
  const [viewValue, setView] = view;

  return (
    <div className="w-full flex pb-4">
      <div className="w-full relative">
        <div className="absolute left-0 top-2/4 -translate-y-2/4 w-10 h-10 p-3 pointer-events-none">
          <IconSearch />
        </div>
        <input
          disabled={viewValue !== "table"}
          className="w-full px-4 pl-10 outline-none h-10 text-sm border border-solid border-slate-300 text-neutral-700 placeholder:text-neutral-400 disabled:bg-slate-200 transition-all duration-300"
          type="text"
          placeholder="Buscar..."
          value={filterValue}
          onChange={(e) => setFilter(e.target.value)}
        />
      </div>
      {filterValue && (
        <ControlButton
          disabled={viewValue !== "table"}
          title="Eliminar búsqueda"
          onClick={() => setFilter("")}
          icon={<IconX />}
        />
      )}
      {!!add && (
        <ControlButton
          disabled={viewValue !== "table"}
          title="Añadir dato"
          onClick={add}
          icon={<IconAdd />}
        />
      )}
      {!!reload && (
        <ControlButton
          disabled={viewValue !== "table"}
          title="Recargar datos"
          onClick={reload}
          icon={<IconReload />}
        />
      )}
      {reports && (
        <>
          <ControlButton
            disabled={loading}
            title={viewValue === "PDF" ? "Ver tabla" : "Ver PDF"}
            onClick={() => setView((old) => (old === "PDF" ? "table" : "PDF"))}
            icon={viewValue === "PDF" ? <IconList /> : <IconPdf />}
          />
          <DownloadTableExcel
            filename="tabla"
            sheet="tabla"
            currentTableRef={tableCurrentRef}
          >
            <ControlButton
              disabled={loading}
              title="Exportar Excel"
              icon={<IconXLSX />}
            />
          </DownloadTableExcel>
        </>
      )}
    </div>
  );
};

export default TableControls;
