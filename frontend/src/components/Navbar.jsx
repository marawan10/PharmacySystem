import { Link } from 'react-router-dom';
import Logo from '../Logo/Logo.png';
import './Navbar.css';

export default function Navbar() {
    return (
        <nav className="navbar glass-panel">
            <div className="container navbar-content">
                <div className="navbar-brand-wrapper">
                    <img src={Logo} alt="PharmaFlow Logo" className="brand-logo" />
                    <Link to="/" className="navbar-brand">PharmaFlow</Link>
                </div>
                <div className="navbar-actions">
                    <Link to="/login" className="btn-primary">Client Portal</Link>
                </div>
            </div>
        </nav>
    );
}
