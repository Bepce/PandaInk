import {Link} from 'react-router-dom';
import { useAuth } from '../auth/AuthContext';
import './Header.css'

export const Header = () => {
  const { isLoggedIn } = useAuth();
  const { setToken } = useAuth();

  function getUserRole(): string | null {
    const token = localStorage.getItem("token");
    if (!token) return null;

    try {
      const payload = JSON.parse(atob(token.split(".")[1]));
      return payload.role || null; 
    } catch {
      return null;
    }
  }

  const role = getUserRole();

   return(
        <div className="header">
            <nav className="navbar">
                <ul className="sidebar">
                    <li>
                       <Link to="/">PandaInk</Link> 
                    </li>
                    <li>
                      <Link to="/series">Browse</Link>
                    </li>
                    <li>
                        {role === "Admin" && (
                            <Link to="/admin/add-series">Admin</Link>
                        )}
                    </li>
                    {isLoggedIn ? (
                        <>
                            <li><Link to="/library">Library</Link></li>
                            <li><Link to="/" onClick={() => setToken(null)}>Logout</Link></li>
                       </>
                    ) : (
                        <>
                            <li><Link to="/login">Login</Link></li>
                            <li><Link to="/register">Register</Link></li>
                       </>
                    )}
                </ul>
            </nav>
        </div>
    );
};