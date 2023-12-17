interface Props<T> {
  pages: string[];
  page: T;
  setPage: React.Dispatch<React.SetStateAction<T>>;
}

const Tabs = <T,>({ page, pages, setPage }: Props<T>) => {
  return (
    <div>
      {pages.map((p, index) => (
        <button
          key={index}
          className={`px-5 py-2 text-sm border-b border-slate-300 text-neutral-600 transition-all duration-300 hover:opacity-80 ${
            page === p ? "text-emerald-500 border-emerald-500" : ""
          }`}
          onClick={() => setPage(p as T)}
        >
          {p}
        </button>
      ))}
    </div>
  );
};

export default Tabs;
