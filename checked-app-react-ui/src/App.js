import { Route, Routes } from 'react-router-dom'
import { Home } from './pages/Home'
import { ItemLists } from './pages/ItemLists'
import { UserHome } from './pages/UserHome'
import { NotFound } from './pages/NotFound'
import {ItemsByPages} from './pages/ItemsByPages'
// import { Items } from './pages/Items'
import Navbar from './Components/NavBar/Navbar'
import Signup from './Components/UserAuthForm/Signup'
import Login from './Components/UserAuthForm/Login'
import { AuthProvider } from './Contexts/AuthContext.js'
import PrivateRoute from './Components/PrivateRoute.js'

function App() {
  return (
    <AuthProvider>
      <Navbar />
      <Routes>
        <Route path='/' element={<Home />} />
        <Route path='/user-home' element={<UserHome />} />

        <Route path='/itemlists'>
          <Route index element={<ItemLists />} />
        </Route>

        <Route
          path='/items'
          element={
            <PrivateRoute>
              <ItemsByPages />
            </PrivateRoute>
          }
        />
        <Route path='*' element={<NotFound />} />
        <Route path='/Register' element={<Signup />} />
        <Route path='/Login' element={<Login />} />
      </Routes>
    </AuthProvider>
  )
}

export default App
