import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import { Header } from "./components/layout/Header";
import { LoginPage } from "./pages/LoginPage";
import Home from "./pages/Home";
import SeriesPage from "./pages/SeriesPage";
import SeriesDetailsPage from "./pages/SeriesDetailsPage";
import ChapterPage from "./pages/ChapterPage";
import RegisterPage from "./pages/RegisterPage";
import AdminPage from "./pages/AdminPage";
import LibraryPage from "./pages/LibraryPage";
import './App.css'
import ServerErrorPage from "./pages/ServerErrorPage";
import NotFoundPage from "./pages/NotFoundPage";

function App(){
  return(
    <Router>
      <Header />
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/series" element = {<SeriesPage />}/>
        <Route path="series/:id" element = {<SeriesDetailsPage />}/>
        <Route path="/chapter/:chapterId" element = {<ChapterPage />}/>
        <Route path="/library" element = {<LibraryPage />}/>
        <Route path="/login" element={<LoginPage />} />
        <Route path="/register" element={<RegisterPage />} />
        <Route path="/admin" element={<AdminPage />} />
        <Route path="/500" element={<ServerErrorPage />} />
        <Route path="*" element={<NotFoundPage/>}/>
      </Routes>
    </Router>
  )
}

export default App;