import './App.css'

import {createBrowserRouter, Navigate, RouterProvider} from "react-router-dom";
import LoginPage from "./pages/login/LoginPage.jsx";
import {useSelector} from "react-redux";
import HomePage from "./pages/homepage/HomePage.jsx";
import ManagerPage from "./pages/manager/ManagerPage.jsx";
import StaffPage from "./pages/staff/StaffPage.jsx";

function App() {
    const user = useSelector((store) => store.user);
    if (!user){
        return <Navigate to={"/"}/>
    }
    const router = createBrowserRouter([
        {
            path: "/",
            element: (
               <HomePage/>
            )
        },
        {
            path: "/login",
            element: (
                <LoginPage/>
            )
        },
        {
            path: "/manager",
            element: (
                <ManagerPage/>
            )
        },
        {
            path: "/staff",
            element: (
                <StaffPage/>
            )
        }
    ])

  return (
   <RouterProvider router={router}/>
  )
}

export default App
