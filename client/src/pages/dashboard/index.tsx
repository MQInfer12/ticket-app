import Sidebar from "./components/sidebar"
import IconProfile from "../../icons/iconProfile"

type Props = {}

const Index = (props: Props) => {
  return (
    <section className="w-screen h-screen flex">
        
        <div className="h-full w-[25%]">
            <Sidebar/>
        </div>

        <div className="h-full flex-1 flex-col">
          <div className="h-20 flex justify-between items-center border border-solid border-stone-500">
            <h1 className="ml-2">Dashboard</h1>
            <div className="aspect-square h-16 mr-2">
              <IconProfile/>
            </div>
          </div>
          <div className="h-[calc(100dvh_-_80px)]">
            
          </div>
        </div>
    
    </section>
  )
}

export default Index