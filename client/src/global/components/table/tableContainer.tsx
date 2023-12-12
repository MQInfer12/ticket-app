import { useState } from "react";
import TableControls from "./tableControls";
import TanstackTable from "./tanstackTable";
import {
  ColumnDef,
  getCoreRowModel,
  getFilteredRowModel,
  getSortedRowModel,
  useReactTable,
} from "@tanstack/react-table";

interface Props {
  data: any[];
  columns: ColumnDef<any, any>[];
}

const TableContainer = ({ data, columns }: Props) => {
  const [sorting, setSorting] = useState([]);
  const [filtering, setFiltering] = useState("");

  const table = useReactTable({
    data,
    columns,
    getCoreRowModel: getCoreRowModel(),
    getSortedRowModel: getSortedRowModel(),
    getFilteredRowModel: getFilteredRowModel(),
    state: { sorting, globalFilter: filtering },
    //@ts-ignore
    onSortingChange: setSorting,
    onGlobalFilterChange: setFiltering,
  });

  return (
    <div className="flex flex-col">
      <TableControls filter={[filtering, setFiltering]} />
      <TanstackTable table={table} />
    </div>
  );
};

export default TableContainer;
