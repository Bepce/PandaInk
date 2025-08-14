import {Link} from 'react-router-dom';
import './Header.css'
function Header() {
    return(
        <div className="header">
            <nav className="navbar">
                <ul className="sidebar">
                    <li>
                       <Link to="/">PandaInk</Link> 
                    </li>
                    <li>
                        <Link to='/series'>Series</Link>
                    </li>
                    <li>
                        <Link to='/login'>Log in</Link>
                    </li>
                    <li>
                        <Link to='/register'>Register</Link>
                    </li>
                </ul>
            </nav>
        </div>
    )
}

export default Header;