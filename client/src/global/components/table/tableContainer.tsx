import { useState } from "react";
import TableControls from "./tableControls";
import TanstackTable from "./tanstackTable";
import { ColumnDef } from "@tanstack/react-table";
import Loader from "../loader/loader";

interface Props {
  data: any[] | undefined;
  columns: ColumnDef<any, any>[];
  reload?: () => void;
  add?: () => void;
  onClickRow?: (row: any) => void;
}

const TableContainer = ({ data, columns, reload, add, onClickRow }: Props) => {
  const [sorting, setSorting] = useState<any[]>([]);
  const [filter, setFilter] = useState("");

  return (
    <div className="flex flex-col h-[calc(100%_-_56px)] flex-[0_0_auto]">
      <TableControls filter={[filter, setFilter]} reload={reload} add={add} />
      {data ? (
        <TanstackTable
          columns={columns}
          data={data}
          filter={filter}
          setFilter={setFilter}
          sorting={sorting}
          setSorting={setSorting}
          onClickRow={onClickRow}
        />
      ) : (
        <Loader />
      )}
    </div>
  );
};

export default TableContainer;
