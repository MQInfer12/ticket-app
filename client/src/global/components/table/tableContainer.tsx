import { useRef, useState } from "react";
import TableControls from "./tableControls";
import TanstackTable from "./tanstackTable";
import { ColumnDef } from "@tanstack/react-table";
import Loader from "../loader/loader";

interface Props {
  data: any[] | undefined;
  columns: ColumnDef<any, any>[];
  reports?: boolean;
  reload?: () => void;
  add?: () => void;
  onClickRow?: (row: any) => void;
}

export type TableView = "table" | "PDF";

const TableContainer = ({
  data,
  columns,
  reports = true,
  reload,
  add,
  onClickRow,
}: Props) => {
  const [sorting, setSorting] = useState<any[]>([]);
  const [filter, setFilter] = useState("");
  const [view, setView] = useState<TableView>("table");
  const tableRef = useRef<HTMLTableElement>(null);

  return (
    <div className="flex flex-col h-[calc(100%_-_44px)] flex-[0_0_auto]">
      <TableControls
        tableCurrentRef={tableRef.current}
        loading={!data}
        filter={[filter, setFilter]}
        reload={reload}
        add={add}
        view={[view, setView]}
        reports={reports}
      />
      {data ? (
        <TanstackTable
          ref={tableRef}
          columns={[
            /* {
        header: "#",
        accessorFn: (_: any, i: number) => i + 1,
      }, */
            ...columns,
          ]}
          data={data}
          filter={filter}
          setFilter={setFilter}
          sorting={sorting}
          setSorting={setSorting}
          onClickRow={onClickRow}
          view={view}
        />
      ) : (
        <Loader />
      )}
    </div>
  );
};

export default TableContainer;
