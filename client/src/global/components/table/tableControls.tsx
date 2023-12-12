import IconAdd from "../../../icons/iconAdd";
import IconReload from "../../../icons/iconReload";
import IconSearch from "../../../icons/iconSearch";
import IconX from "../../../icons/iconX";
import ControlButton from "./controlButton";

interface Props {
  filter: [string, React.Dispatch<React.SetStateAction<string>>];
  reload?: () => void;
  add?: () => void;
}

const TableControls = ({ filter, reload, add }: Props) => {
  const [filterValue, setFilter] = filter;
  return (
    <div className="w-full flex pb-4">
      <div className="w-full relative">
        <div className="absolute left-0 top-2/4 -translate-y-2/4 w-10 h-10 p-3 pointer-events-none">
          <IconSearch />
        </div>
        <input
          className="w-full px-4 pl-10 outline-none h-10 text-sm border border-solid border-slate-300 text-neutral-700 placeholder:text-neutral-400"
          type="text"
          placeholder="Buscar..."
          value={filterValue}
          onChange={(e) => setFilter(e.target.value)}
        />
      </div>
      {filterValue && (
        <ControlButton onClick={() => setFilter("")} icon={<IconX />} />
      )}
      {
        !!add &&
        <ControlButton onClick={add} icon={<IconAdd />} />
      }
      {
        !!reload &&
        <ControlButton onClick={reload} icon={<IconReload />} />
      }
    </div>
  );
};

export default TableControls;
