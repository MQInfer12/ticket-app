import { useParams } from "react-router-dom";
import Button from "../../global/components/buttons/button";
import IconHeart from "../../icons/iconHeart";
import { useState } from "react";

const Gracias = () => {
  const { id } = useParams();
  const [pdfBlob, setPdfBlob] = useState<Blob | null>(null);
  
  const getPdfBlob = async () => {
    try {
      const response = await fetch(
        `${import.meta.env.VITE_BACKEND}Transaccion/GeneratePdf/${id}`
      );
      if (response.ok) {
        const datas = await response.blob();
        setPdfBlob(datas);
      }
    } catch (error) {
      console.error("Error al obtener el archivo PDF:", error);
      setPdfBlob(null);
    }
  };

  return (
    <div className="w-full h-full flex flex-col gap-4 items-center justify-center animate-[appear_1.5s]">
      {pdfBlob === null && (
        <>
          <div className="w-40 text-emerald-500">
            <IconHeart />
          </div>
          <p className="text-neutral-500">¡Gracias por su compra!</p>
          {/* <a href={`${import.meta.env.VITE_BACKEND}Transaccion/GeneratePdf/${id}`}> */}
          <Button onClick={getPdfBlob}>Ver detalles</Button>
          {/* </a> */}
        </>
      )}
      {pdfBlob != null && (
        <>
          <Button onClick={() => setPdfBlob(null)}>Cerrar</Button>
          <iframe
            src={URL.createObjectURL(pdfBlob)}
            title="PDFViewer"
            className="w-4/5 h-full -mt-4"
          />
        </>
      )}
    </div>
  );
};

export default Gracias;
