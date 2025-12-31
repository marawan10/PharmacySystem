import { useNavigate } from 'react-router-dom';
import Navbar from '../components/Navbar';
import './Home.css';

export default function Home() {
    const navigate = useNavigate();

    return (
        <>
            <Navbar />
            <main className="home-wrapper">

                {/* Decorative Blobs */}
                <div className="blob blob-1"></div>
                <div className="blob blob-2"></div>

                {/* Hero Section */}
                <section className="container hero-section">
                    <div className="hero-content">
                        <span className="badge">New Generation Pharmacy</span>
                        <h1 className="hero-title">
                            Manage your Pharmacy with <span className="gradient-text">Flow</span>
                        </h1>
                        <p className="hero-description">
                            PharmaFlow provides an intuitive, powerful, and secure way to manage inventory, sales, and patient records. Experience the future of healthcare management.
                        </p>
                        <div className="hero-actions">
                            <button onClick={() => navigate('/login')} className="btn-primary btn-lg">
                                Get Started
                            </button>
                            <button className="btn-secondary">
                                Learn More
                            </button>
                        </div>
                    </div>

                    <div className="hero-visual glass-panel">
                        <div className="visual-content dashboard-mock">

                            {/* Mock Sidebar */}
                            <div className="mock-sidebar">
                                <div className="sidebar-item active"></div>
                                <div className="sidebar-item"></div>
                                <div className="sidebar-item"></div>
                                <div className="sidebar-item"></div>
                            </div>

                            {/* Mock Main Content */}
                            <div className="mock-main">
                                {/* Mock Header */}
                                <div className="mock-header">
                                    <div className="mock-search"></div>
                                    <div className="mock-profile"></div>
                                </div>

                                {/* Mock Stats Row */}
                                <div className="mock-stats-row">
                                    <div className="mock-stat-card">
                                        <span className="label">Revenue</span>
                                        <div className="value">$12.4k</div>
                                        <div className="trend up">+18%</div>
                                    </div>
                                    <div className="mock-stat-card">
                                        <span className="label">Invoices</span>
                                        <div className="value">843</div>
                                    </div>
                                </div>

                                {/* Mock Graph/Activity */}
                                <div className="mock-activity-card">
                                    <div className="activity-header">
                                        <span className="dot red"></span>
                                        <span className="dot yellow"></span>
                                        <span className="dot green"></span>
                                    </div>
                                    <div className="activity-content">
                                        <div className="graph-bar h-40"></div>
                                        <div className="graph-bar h-60"></div>
                                        <div className="graph-bar h-80"></div>
                                        <div className="graph-bar h-50"></div>
                                        <div className="graph-bar h-70"></div>
                                        <div className="graph-bar h-90"></div>
                                        <div className="graph-bar h-60"></div>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </section>

                {/* Features Grid */}
                <section className="container features-section">
                    <h2>Why PharmaFlow?</h2>
                    <div className="features-grid">
                        <div className="feature-card glass-panel">
                            <div className="feature-icon">📦</div>
                            <h3>Smart Inventory</h3>
                            <p>Real-time tracking of stock levels with automated alerts.</p>
                        </div>
                        <div className="feature-card glass-panel">
                            <div className="feature-icon">⚡</div>
                            <h3>Fast Billing</h3>
                            <p>Lightning fast checkout process for better customer experience.</p>
                        </div>
                        <div className="feature-card glass-panel">
                            <div className="feature-icon">🛡️</div>
                            <h3>Secure Data</h3>
                            <p>Enterprise-grade security for patient and transaction data.</p>
                        </div>
                    </div>
                </section>

            </main>
        </>
    );
}
