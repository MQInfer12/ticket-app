import {
  ColumnDef,
  flexRender,
  getCoreRowModel,
  getFilteredRowModel,
  getSortedRowModel,
  useReactTable,
} from "@tanstack/react-table";
import IconArrowUp from "../../../icons/iconArrowUp";
import IconArrowDown from "../../../icons/iconArrowDown";

interface Props {
  filter: string;
  setFilter: React.Dispatch<React.SetStateAction<string>>;
  sorting: any[];
  setSorting: React.Dispatch<React.SetStateAction<any[]>>;
  data: any[];
  columns: ColumnDef<any, any>[];
  onClickRow?: (row: any) => void;
}

const TanstackTable = ({
  data,
  columns,
  filter,
  setFilter,
  sorting,
  setSorting,
  onClickRow,
}: Props) => {
  const table = useReactTable({
    data,
    columns,
    getCoreRowModel: getCoreRowModel(),
    getSortedRowModel: getSortedRowModel(),
    getFilteredRowModel: getFilteredRowModel(),
    state: { sorting, globalFilter: filter },
    //@ts-ignore
    onSortingChange: setSorting,
    onGlobalFilterChange: setFilter,
  });

  const handleClickRow = (row: any) => {
    if (onClickRow) {
      onClickRow(row);
    }
  };

  const thStyle =
    "px-2 py-2 bg-slate-200 border border-solid border-slate-300 text-sm font-medium text-neutral-800 text-start select-none";
  const tdStyle =
    "px-2 py-2 border border-solid border-slate-300 text-sm text-neutral-800";
  return (
    <div className="overflow-auto">
      <table className="w-full border-separate border-spacing-0">
        <thead className="sticky top-0">
          {table.getHeaderGroups().map((group) => (
            <tr key={group.id}>
              {/* <th className={thStyle} /> */}
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
              className={`hover:bg-slate-200 transition-all duration-300 ${onClickRow ? "cursor-pointer" : ""}`}
              key={row.id}
              onClick={() => handleClickRow(row.original)}
            >
              {/* <td className={tdStyle + " text-center"}>
                <input type="checkbox" />
              </td> */}
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
