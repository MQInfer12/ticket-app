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
          className={`px-5 py-2 text-sm border-b transition-all duration-300 hover:opacity-80`}
          style={{
            color: page === p ? "rgb(16 185 129)" : "rgb(82 82 82)",
            borderColor: page === p ? "rgb(16 185 129)" : "rgb(203 213 225)",
          }}
          onClick={() => setPage(p as T)}
        >
          {p}
        </button>
      ))}
    </div>
  );
};

export default Tabs;
