import { Counter } from "./components/Counter";
import { FetchData } from "./components/FetchData";
import { Home } from "./components/Home";
import { Yoba } from "./components/Yoba";
import Test from "./components/test";

const AppRoutes = [
  {
    index: true,
    element: <Home />
  },
  {
    path: '/counter',
    element: <Counter />
  },
  {
    path: '/fetch-data',
    element: <FetchData />
  },
  {
    path: '/Test',
    element: <Test />
  },
    {
        path: '/Yoba',
        element: <Yoba />
    }
];

export default AppRoutes;
