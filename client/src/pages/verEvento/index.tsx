import { useNavigate, useParams } from "react-router-dom";
import PageContainer from "../../global/components/pageContainer";
import { Entrada } from "../../global/interfaces/api/entrada";
import { useGet } from "../../hooks/useGet";
import Loader from "../../global/components/loader/loader";
import { useState } from "react";
import { useUser } from "../../store/user";
import Button from "../../global/components/buttons/button";
import EntradaCart from "./components/entradaCart";
import { Carrito } from "./interfaces/carrito";
import ExternalInput from "../../global/components/form/externalInput";
import { useRequest } from "../../hooks/useRequest";

const VerEvento = () => {
  const navigate = useNavigate();
  const { id } = useParams();
  const { res } = useGet<Entrada[]>(`TipoEntrada/GetTipoEntradaByEvento/${id}`);
  const { user } = useUser();
  const [carrito, setCarrito] = useState<Carrito>({
    idUsuario: user?.userId,
    items: [],
  });
  const { sendRequest } = useRequest();
  const [loading, setLoading] = useState(false);

  const handleAddToCart = (cantidad: number, idEntrada: string) => {
    const exists = carrito.items.find((item) => item.idEntrada === idEntrada);
    setCarrito((old) => ({
      ...old,
      items: !exists
        ? [
            ...old.items,
            {
              cantidad,
              idEntrada,
              ci: new Array(cantidad).fill(""),
            },
          ]
        : old.items.map((item) => {
            if (item.idEntrada === idEntrada) {
              return {
                ...exists,
                cantidad: exists.cantidad + cantidad,
                ci: [...exists.ci, ...new Array(cantidad).fill("")],
              };
            }
            return item;
          }),
    }));
  };

  const changeCi = (
    e: React.ChangeEvent<HTMLInputElement>,
    entrada: Entrada,
    index: number
  ) => {
    setCarrito((old) => ({
      ...old,
      items: old.items.map((item) => {
        if (item.idEntrada === entrada.id) {
          return {
            ...item,
            ci: item.ci.map((c, i) => {
              if (i === index) {
                return e.target.value;
              }
              return c;
            }),
          };
        }
        return item;
      }),
    }));
  };

  const handleRemove = (idEntrada: string) => {
    setCarrito((old) => ({
      ...old,
      items: old.items.filter((old) => old.idEntrada !== idEntrada),
    }));
  };

  const handleSend = async () => {
    setLoading(true);
    const res = await sendRequest("Transaccion/ComprarTicket", carrito);
    if(res) {
      //@ts-ignore
      navigate(`/dashboard/inicio/gracias/${res?.data.id}`);
    }
    setLoading(false);
  }

  return (
    <PageContainer backRoute="/dashboard/inicio" title="Ver evento">
      {!res ? (
        <Loader />
      ) : (
        <div className="h-full flex overflow-auto">
          <div>
            {res.data.map((entrada) => (
              <EntradaCart
                key={entrada.id}
                handleSend={handleAddToCart}
                entrada={entrada}
              />
            ))}
          </div>
          <div>
            <p>Carrito</p>
            {carrito.items.map((item) => {
              const entrada = res.data.find(
                (entrada) => entrada.id === item.idEntrada
              );
              if (!entrada) return null;
              return (
                <div className="flex flex-col" key={item.idEntrada}>
                  <p>{entrada?.nombre}</p>
                  <small>Total {entrada?.costo * item.cantidad} Bs.</small>
                  {new Array(item.cantidad).fill("").map((_, i) => (
                    <div className="flex" key={i}>
                      <ExternalInput
                        value={item.ci[i]}
                        onChange={(e) => changeCi(e, entrada, i)}
                        title="CI"
                      />
                    </div>
                  ))}
                  <Button onClick={() => handleRemove(entrada?.id)}>
                    Eliminar todo
                  </Button>
                </div>
              );
            })}
          </div>
          {carrito.items.length > 0 && (
            <div>
              <Button disabled={loading} onClick={handleSend}>{loading ? "Cargando..." : "Comprar"}</Button>
            </div>
          )}
        </div>
      )}
    </PageContainer>
  );
};

export default VerEvento;
