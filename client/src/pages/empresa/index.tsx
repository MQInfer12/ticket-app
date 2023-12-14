import TableContainer from "../../global/components/table/tableContainer.tsx";
import PageContainer from "../../global/components/pageContainer.tsx";
import { useGet } from "../../hooks/useGet.tsx";
import { Empresa } from "../../global/interfaces/api/empresa.ts";
import Modal from "../../global/components/modal.tsx";
import { useModal } from "../../hooks/useModal.tsx";
import Formulario from "./components/formulario.tsx";

const Index = () => {
  const { res, getData, pushData, filterData } = useGet<Empresa[]>("Empresa");
  const { state, item, openModal, closeModal } = useModal<Empresa>("Formulario de empresa");

  const columns = [
    {
      header: "#",
      accessorFn: (_: any, i: number) => i + 1,
    },
    {
      header: "Nombre",
      accessorKey: "nombre",
    },
    {
      header: "Dirección",
      accessorKey: "direccion",
      cell: (info: any) => info.getValue() || "N/A",
    },
    {
      header: "Estado",
      accessorKey: "estado",
      cell: (info: any) => (info.getValue() ? "Activo" : "Desactivo"),
    },
  ];

  return (
    <PageContainer title="Empresas">
      <TableContainer
        columns={columns}
        data={res?.data}
        reload={getData}
        add={() => openModal()}
        onClickRow={(row) => openModal(row)}
      />
      <Modal state={state}>
        <Formulario 
          empresa={item} 
          onSuccess={data => {
            pushData([data]);
            closeModal();
          }}
          onDelete={empresa => {
            filterData(value => value.id !== empresa.id)
            closeModal();
          }}
        />
      </Modal>
    </PageContainer>
  );
};

export default Index;
