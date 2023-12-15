import { useId } from "react";
import { Field, ErrorMessage } from "formik";
import IconError from "../../../icons/iconError";

interface Props {
  title: string;
  name: string;
  placeholder?: string;
  type?: string;
}

const FormInput = ({ title, placeholder, name, type }: Props) => {
  const id = useId();

  return (
    <div className="flex flex-col gap-1">
      <label
        className="text-sm ml-2 font-semibold text-neutral-700"
        htmlFor={id}
      >
        {title}
      </label>
      <div className="relative">
        <Field
          id={id}
          className="w-full px-2 py-2 pr-10 text-sm border outline-none border-slate-300 text-neutral-700 focus:outline-none focus:ring-2 focus:ring-slate-300 transition-all duration-300 bg-white"
          type={type || "text"}
          name={name}
          placeholder={placeholder || `Ingrese ${title.toLocaleLowerCase()}`}
        />
        <ErrorMessage name={name}>
          {(msg) => (
            <div className="absolute right-0 top-0 p-2 h-full aspect-square z-10 animate-[appear_.5s]">
              <div className="relative">
                <IconError />
                <small className="hidden peer-hover:block absolute max-w-xs w-max bg-slate-100 right-2/4 shadow-lg border border-slate-300 text-xs px-2 py-2 rounded-lg rounded-tr-none text-rose-800 animate-[appear_.5s]">
                  {msg}
                </small>
              </div>
            </div>
          )}
        </ErrorMessage>
      </div>
    </div>
  );
};

export default FormInput;
