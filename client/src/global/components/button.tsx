type Props = {
  children: string
}

const Button = ({ children }: Props) => {
  return (
    <button
      className="px-4 py-2 border border-solid border-gray-600 rounded-lg"
    >{children}</button>
  )
}

export default Button