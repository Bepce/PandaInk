import { Link } from 'react-router-dom';
import './Home.css'

function Home() {
  return(
      <div className='home'>
        <h1>Welcome to PandaInk</h1>
        <p>
          <Link to="/login">Login</Link> or{' '}
          <Link to="/register">register</Link> and start reading now!
        </p>
      </div>
    )
}

export default Home;