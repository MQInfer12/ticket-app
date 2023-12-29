import {
  ColumnDef,
  flexRender,
  getCoreRowModel,
  getFilteredRowModel,
  getSortedRowModel,
  useReactTable,
} from "@tanstack/react-table";
import TablePDF from "./pdf/tablePDF";
import FlatList from "flatlist-react/lib";
import { TableView } from "./tableContainer";
import { forwardRef } from "react";
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
  view: TableView;
}

const TanstackTable = forwardRef(
  (
    {
      data,
      columns,
      filter,
      setFilter,
      sorting,
      setSorting,
      onClickRow,
      view,
    }: Props,
    tableRef: React.ForwardedRef<HTMLTableElement>
  ) => {
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
    const tdStyle = `px-2 py-2 border border-solid border-slate-300 text-sm text-neutral-800 ${
      onClickRow ? "cursor-pointer" : ""
    }`;

    if (view === "PDF") {
      return <TablePDF table={table} />;
    }
    return (
      <div className="overflow-auto w-full">
        <table
          ref={tableRef}
          className="w-full border-separate border-spacing-0"
        >
          <thead className="sticky top-0">
            {table.getHeaderGroups().map((group) => (
              <tr key={group.id}>
                {/* <th className={thStyle} /> */}
                {group.headers.map((header) => (
                  <th
                    className={thStyle}
                    style={{
                      backgroundColor: "#e2e8f0",
                    }}
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
            <FlatList
              list={table.getRowModel().rows}
              renderOnScroll
              renderItem={(row) => (
                <tr
                  className={`hover:bg-slate-200 transition-all duration-300`}
                  key={row.id}
                >
                  {row.getVisibleCells().map((cell) => {
                    const header = cell.column.columnDef.header;
                    if (header === "Acciones")
                      return (
                        <td
                          className={tdStyle + " cursor-default"}
                          key={cell.id}
                        >
                          {flexRender(
                            cell.column.columnDef.cell,
                            cell.getContext()
                          )}
                        </td>
                      );
                    return (
                      <td
                        className={tdStyle}
                        key={cell.id}
                        onClick={() => handleClickRow(row.original)}
                      >
                        {flexRender(
                          cell.column.columnDef.cell,
                          cell.getContext()
                        )}
                      </td>
                    );
                  })}
                </tr>
              )}
            />
          </tbody>
        </table>
      </div>
    );
  }
);

export default TanstackTable;
