import {Link} from 'react-router-dom';
import { useAuth } from '../auth/AuthContext';
import './Header.css'

export const Header = () => {
  const { isLoggedIn } = useAuth();
  const { setToken } = useAuth();

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