import { Table, flexRender } from "@tanstack/react-table";
import IconArrowUp from "../../../icons/iconArrowUp";
import IconArrowDown from "../../../icons/iconArrowDown";

interface Props {
  table: Table<any>;
}

const TanstackTable = ({ table }: Props) => {
  const thStyle =
    "px-2 py-2 bg-slate-200 border border-solid border-slate-300 text-sm font-medium text-neutral-800 text-start select-none";
  const tdStyle =
    "px-2 py-2 border border-solid border-slate-300 text-sm text-neutral-800";
  return (
    <div>
      <table className="w-full">
        <thead>
          {table.getHeaderGroups().map((group) => (
            <tr key={group.id}>
              <th className={thStyle} />
              {group.headers.map((header) => (
                <th
                  className={thStyle}
                  key={header.id}
                  onClick={header.column.getToggleSortingHandler()}
                >
                  <div className="flex">
                    {flexRender(
                      header.column.columnDef.header,
                      header.getContext()
                    )}
                    <div className="h-5 w-5">
                      {
                        {
                          none: <></>,
                          asc: <IconArrowUp />,
                          desc: <IconArrowDown />,
                        }[header.column.getIsSorted() || "none"]
                      }
                    </div>
                  </div>
                </th>
              ))}
            </tr>
          ))}
        </thead>
        <tbody>
          {table.getRowModel().rows.map((row) => (
            <tr
              className="hover:bg-slate-200 transition-all duration-300"
              key={row.id}
            >
              <td className={tdStyle + " text-center"}>
                <input type="checkbox" />
              </td>
              {row.getVisibleCells().map((cell) => (
                <td className={tdStyle} key={cell.id}>
                  {flexRender(cell.column.columnDef.cell, cell.getContext())}
                </td>
              ))}
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default TanstackTable;
