import { useState } from "react";

export interface ModalState {
  title: string;
  open: boolean;
  closeModal: () => void;
}

export const useModal = <T,>(title: string = "") => {
  const [open, setOpen] = useState(false);
  const [item, setItem] = useState<T | null>(null);

  const openModal = (item?: T) => {
    if (item) {
      setItem(item);
    } else {
      setItem(null);
    }
    setOpen(true);
  };

  const closeModal = () => {
    setOpen(false);
  };

  const state: ModalState = {
    title,
    open,
    closeModal,
  };

  return {
    state,
    open,
    item,
    openModal,
    closeModal,
  };
};
