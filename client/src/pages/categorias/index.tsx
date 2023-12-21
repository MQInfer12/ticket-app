import { useNavigate } from "react-router-dom";
import Button from "../../global/components/buttons/button";
import Form from "../../global/components/form/form";
import FormInput from "../../global/components/form/formInput";
import FormSelect from "../../global/components/form/formSelect";
import Modal from "../../global/components/modal";
import PageContainer from "../../global/components/pageContainer";
import TableContainer from "../../global/components/table/tableContainer";
import { Categoria } from "../../global/interfaces/api/categoria";
import { Empresa } from "../../global/interfaces/api/empresa";
import { Roles } from "../../global/interfaces/types/roles";
import { useGet } from "../../hooks/useGet";
import { useModal } from "../../hooks/useModal";
import { useUser } from "../../store/user";
import { CategoriaForm, categoriaSchema } from "./validations/categorias";

const Categorias = () => {
  const { user } = useUser();
  const navigate = useNavigate();
  const { res, getData, pushData, modifyData, filterData } =
    useGet<Categoria[]>("Categoria");
  const { res: dataEmpresa } = useGet<Empresa[]>("Empresa");
  const { item, openModal, closeModal, state } = useModal<Categoria>(
    "Formulario categoria"
  );

  const columns = [
    {
      header: "Nombre",
      accessorKey: "nombre",
    },
    {
      header: "Acciones",
      cell: (cell: any) => {
        const item: Categoria = cell.row.original;
        return (
          <div className="flex justify-center">
            <Button
              onClick={() => navigate(`/dashboard/categorias/${item.id}`)}
            >
              Ver
            </Button>
          </div>
        );
      },
    },
  ];
  if (user?.roleName === Roles.superadmin)
    columns.unshift({ header: "Empresa", accessorKey: "nombreEmpresa" });

  return (
    <PageContainer title="Categorias">
      <TableContainer
        columns={columns}
        data={res?.data}
        add={() => openModal()}
        onClickRow={(row) => openModal(row)}
        reload={getData}
      ></TableContainer>
      <Modal state={state}>
        <Form<Categoria | null, CategoriaForm>
          item={item}
          validationSchema={categoriaSchema}
          initialValues={{
            idEmpresa:
              user?.roleName == Roles.superadmin
                ? item
                  ? item?.idEmpresa
                  : ""
                : user?.companyId,
            nombreCategoria: item?.nombre || "",
          }}
          post={{
            route: "Categoria",
            onBody: (value) => value,
            onSuccess: (data) => {
              pushData(data);
              closeModal();
            },
          }}
          put={{
            route: `Categoria/${item?.id}`,
            onBody: (value) => value,
            onSuccess: (data) => {
              modifyData(data, (value) => value.id === data.id);
              closeModal();
            },
          }}
          del={{
            route: `Categoria/${item?.id}`,
            onSuccess: (data) => {
              filterData((value) => value.id !== data.id);
              closeModal();
            },
          }}
        >
          <FormInput title="Nombre" name="nombreCategoria" />
          {user?.roleName === Roles.superadmin && (
            <FormSelect title="Empresa" name="idEmpresa">
              <option value="">Elija una empresa</option>
              {dataEmpresa?.data.map((empresa) => (
                <option value={empresa.id} key={empresa.id}>
                  {empresa.nombre}
                </option>
              ))}
            </FormSelect>
          )}
        </Form>
      </Modal>
    </PageContainer>
  );
};

export default Categorias;
