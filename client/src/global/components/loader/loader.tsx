import styles from "./loader.module.css";

interface Props {
  text?: string;
}

const Loader = ({ text = "Cargando..." }: Props) => {
  return (
    <div className="w-full h-full flex flex-col gap-2 items-center justify-center">
      <span className={styles.loader}></span>
      <small className="text-neutral-700">{text}</small>
    </div>
  )
}

export default Loader