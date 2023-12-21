import { useNavigate } from "react-router-dom";
import Button from "../../global/components/buttons/button";
import Loader from "../../global/components/loader/loader";
import PageContainer from "../../global/components/pageContainer";
import { Evento } from "../../global/interfaces/api/evento";
import { useGet } from "../../hooks/useGet";

const Inicio = () => {
  const navigate = useNavigate();
  const { res } = useGet<Evento[]>("EventType");

  return (
    <PageContainer title="Inicio">
      {!res ? (
        <Loader />
      ) : (
        res.data.map((evento) => (
          <div key={evento.id}>
            <p>{evento.typeEventName}</p>
            <Button onClick={() => navigate(`/dashboard/inicio/verEvento/${evento.id}`)}>Ver entradas</Button>
          </div>
        ))
      )}
    </PageContainer>
  );
};

export default Inicio;
