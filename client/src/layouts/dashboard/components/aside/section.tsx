interface Props {
  title: string
  children: JSX.Element[] | JSX.Element
}

const Section = ({ title, children }: Props) => {
  return (
    <div className="border-t border-solid border-slate-300 py-4 flex flex-col gap-2">
      <h2 className="ml-3 text-xs text-neutral-700">{title}</h2>
      {children}
    </div>
  )
}

export default Section