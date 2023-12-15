import { Formik, Form as FormikForm } from "formik";
import Button from "../buttons/button";
import { confirmAlert, successAlert } from "../../utils/alerts";
import { useRequest } from "../../../hooks/useRequest";

interface Props<T, U> {
  children: React.ReactNode;
  initialValues: any;
  validationSchema: any;
  item: T;
  put: SendRequest<T, U>;
  post: SendRequest<T, U>;
  del: DeleteRequest<T>;
}

interface SendRequest<T, U> {
  route: string;
  onBody: (value: U) => Record<string, any>;
  onSuccess: (data: NonNullable<T>) => void;
}

interface DeleteRequest<T> {
  route: string;
  onSuccess: (data: NonNullable<T>) => void;
}

const Form = <T, U>({
  children,
  initialValues,
  validationSchema,
  item,
  put,
  post,
  del,
}: Props<T, U>) => {
  const { sendRequest } = useRequest();

  const handleSend = async (value: U) => {
    const res = await sendRequest<NonNullable<T>>(
      item ? put.route : post.route,
      item ? put.onBody(value) : post.onBody(value),
      {
        method: item ? "PUT" : "POST",
      }
    );
    if (!res) return;
    successAlert(res.message);
    if (item) {
      put.onSuccess(res.data);
    } else {
      post.onSuccess(res.data);
    }
  };

  const handleDelete = async () => {
    if (!item) return;
    const res = await sendRequest(del.route, null, {
      method: "DELETE",
    });
    if (res) {
      successAlert(res.message);
      del.onSuccess(item);
    }
  };

  return (
    <Formik
      initialValues={initialValues}
      validationSchema={validationSchema}
      onSubmit={handleSend}
    >
      <FormikForm className="flex flex-col gap-6">
        <div className="flex flex-col gap-2">{children}</div>
        <div className="self-center flex gap-4">
          <Button type="submit">Enviar</Button>
          {item && (
            <Button
              type="button"
              onClick={() => confirmAlert(handleDelete)}
              bg="bg-rose-800"
            >
              Eliminar
            </Button>
          )}
        </div>
      </FormikForm>
    </Formik>
  );
};

export default Form;
