import { useGet } from "../../hooks/useGet";
import { Caja } from "../../global/interfaces/api/cajas";
import PageContainer from "../../global/components/pageContainer";
import TableContainer from "../../global/components/table/tableContainer";
import Modal from "../../global/components/modal";
import { useModal } from "../../hooks/useModal";
import { CajaForm, cajaSchema } from "./validations/cajas";
import Form from "../../global/components/form/form";
import FormSelect from "../../global/components/form/formSelect";
import FormInput from "../../global/components/form/formInput";
import { Empresa } from "../../global/interfaces/api/empresa";
import Button from "../../global/components/buttons/button";
import { useNavigate } from "react-router-dom";
import { Cajas as CajaType } from "../../global/interfaces/types/cajas";

const Cajas = () => {
  const navigate = useNavigate();
  const { res, getData, pushData, filterData, modifyData } =
    useGet<Caja[]>("Caja");
  const { res: dataEmpresa } = useGet<Empresa[]>("Empresa");
  const { state, item, openModal, closeModal } =
    useModal<Caja>("Formulario Caja");

  const columns = [
    {
      header: "Empresa",
      accessorKey: "companyName",
    },
    {
      header: "Caja",
      accessorKey: "cajaName",
    },
    {
      header: "Acciones",
      cell: (cell: any) => {
        const item: Caja = cell.row.original;
        return (
          <div className="flex justify-center">
            <Button onClick={() => navigate(`/dashboard/cajas/${item.id}`)}>
              Ver
            </Button>
          </div>
        );
      },
    },
  ];
  return (
    <PageContainer title="Cajas">
      <TableContainer
        columns={columns}
        data={res?.data}
        reload={getData}
        add={() => openModal()}
        onClickRow={(row) => openModal(row)}
      />
      <Modal state={state}>
        <Form<Caja | null, CajaForm>
          item={item}
          initialValues={{
            companyId: item?.companyId || "",
            cajaName: item?.cajaName || "",
          }}
          validationSchema={cajaSchema}
          post={{
            route: "Caja",
            onBody: (value) => value,
            onSuccess: (data) => {
              pushData(data);
              closeModal();
            },
          }}
          put={{
            route: `Caja/${item?.id}`,
            onBody: (value) => value,
            onSuccess: (data) => {
              modifyData(data, (value) => value.id === data.id);
              closeModal();
            },
          }}
          del={{
            route: `Caja/${item?.id}`,
            onSuccess: (data) => {
              filterData((value) => value.id !== data.id);
              closeModal();
            },
          }}
          showDelete={item?.cajaName !== CajaType.cajaVirtual}
        >
          <FormSelect title="Empresa" name="companyId">
            <option value="">Seleccione empresa</option>
            {dataEmpresa?.data.map((data) => (
              <option value={data.id} key={data.id}>{data.nombre}</option>
            ))}
          </FormSelect>
          <FormInput title="Nombre" name="cajaName"/>
        </Form>
      </Modal>
    </PageContainer>
  );
};

export default Cajas;
