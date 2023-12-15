import { ModalState } from "../../hooks/useModal";
import IconX from "../../icons/iconX";
import CircleButton from "./buttons/circleButton";

interface Props {
  children: React.ReactNode;
  state: ModalState;
}

const Modal = ({ children, state }: Props) => {
  if (!state.open) return null;
  return (
    <div className="w-screen h-screen fixed inset-0 p-5 flex items-center justify-center isolate">
      <div
        onClick={state.closeModal}
        className="w-full h-full bg-black opacity-20 fixed top-0 left-0 -z-10"
      />
      <div className="w-[500px] max-w-[100%] bg-slate-200 rounded-lg">
        <header className="w-full flex justify-between items-center pt-2 px-4">
          <b className="text-sm text-neutral-800">{state.title}</b>
          <CircleButton onClick={state.closeModal} icon={<IconX />} />
        </header>
        <div className="max-h-96 overflow-auto py-4 px-4">{children}</div>
      </div>
    </div>
  );
};

export default Modal;
