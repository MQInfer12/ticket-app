import { ModalState } from "../../hooks/useModal";
import IconX from "../../icons/iconX";
import CircleButton from "./circleButton";

interface Props {
  children: React.ReactNode;
  state: ModalState;
}

const Modal = ({ children, state }: Props) => {
  if (!state.open) return null;
  return (
    <div className="w-screen h-screen fixed inset-0">
      <div
        onClick={state.closeModal}
        className="w-full h-full bg-black opacity-20"
      />
      <div className="fixed left-2/4 top-2/4 -translate-x-2/4 -translate-y-2/4 w-96 bg-slate-200 rounded-lg">
        <header className="w-full flex justify-between items-center py-2 px-4">
          <b className="text-sm text-neutral-800">{state.title}</b>
          <CircleButton onClick={state.closeModal} icon={<IconX />} />
        </header>
        <div className="max-h-96 overflow-auto pb-2 px-4">{children}</div>
      </div>
    </div>
  );
};

export default Modal;
