import PageContainer from "../../global/components/pageContainer";
import TableContainer from "../../global/components/table/tableContainer";

const Evento = () => {
  return (
    <PageContainer backRoute="/dashboard/eventos" title="Evento">
      <div className="flex flex-col h-full gap-5">
        <div className="flex flex-col items-center gap-2">
          <div className="flex flex-col items-center">
            <h3 className="font-bold text-lg text-neutral-800">
              Concierto de Romeo Santos
            </h3>
            <p className="text-xs text-neutral-500">{`16-12-2023 (6000 entradas)`}</p>
          </div>
        </div>
        <div className="h-[calc(100%_-_64px)]">
          <TableContainer data={[]} columns={[]} />
        </div>
      </div>
    </PageContainer>
  );
};

export default Evento;
