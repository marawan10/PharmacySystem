import { Outlet, Link, useNavigate } from 'react-router-dom';
import Logo from '../Logo/Logo.png';
import './AdminLayout.css';

export default function AdminLayout() {
    const navigate = useNavigate();

    const handleLogout = () => {
        localStorage.removeItem('token');
        localStorage.removeItem('user');
        navigate('/login');
    };

    return (
        <div className="admin-container">
            {/* Sidebar */}
            <aside className="admin-sidebar glass-panel">
                <div className="sidebar-header">
                    <img src={Logo} alt="PharmaFlow" className="sidebar-logo" />
                    <span className="sidebar-brand">PharmaFlow</span>
                </div>

                <nav className="sidebar-nav">
                    <Link to="/admin" className="nav-item active">
                        <span className="icon">📊</span> Dashboard
                    </Link>
                    <Link to="/admin/inventory" className="nav-item">
                        <span className="icon">📦</span> Inventory
                    </Link>
                    <Link to="/admin/sales" className="nav-item">
                        <span className="icon">💰</span> Sales
                    </Link>
                    <Link to="/admin/users" className="nav-item">
                        <span className="icon">👥</span> Users
                    </Link>
                    <Link to="/admin/reports" className="nav-item">
                        <span className="icon">📈</span> Reports
                    </Link>
                </nav>

                <div className="sidebar-footer">
                    <button onClick={handleLogout} className="logout-btn">
                        <span className="icon">🚪</span> Logout
                    </button>
                </div>
            </aside>

            {/* Main Content */}
            <main className="admin-main">
                <header className="admin-header glass-panel">
                    <h2 className="page-title">Dashboard</h2>
                    <div className="admin-profile">
                        <div className="avatar">A</div>
                        <span className="role">Admin</span>
                    </div>
                </header>

                <div className="admin-content-area">
                    <Outlet />
                </div>
            </main>
        </div>
    );
}
