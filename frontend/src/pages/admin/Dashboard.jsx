import './Dashboard.css';

export default function Dashboard() {
    return (
        <div className="dashboard-page">
            {/* Stats Row */}
            <div className="stats-grid">
                <div className="stat-card glass-panel">
                    <div className="stat-icon green">💰</div>
                    <div>
                        <h3>Total Sales</h3>
                        <p className="stat-value">$24,500</p>
                        <span className="stat-trend up">+12.5%</span>
                    </div>
                </div>

                <div className="stat-card glass-panel">
                    <div className="stat-icon blue">📦</div>
                    <div>
                        <h3>Low Stock</h3>
                        <p className="stat-value">14 Items</p>
                        <span className="stat-trend down">Restock Needed</span>
                    </div>
                </div>

                <div className="stat-card glass-panel">
                    <div className="stat-icon violet">👥</div>
                    <div>
                        <h3>Active Users</h3>
                        <p className="stat-value">8</p>
                        <span className="stat-trend">Online Now</span>
                    </div>
                </div>
            </div>

            {/* Main Charts / Activity Area */}
            <div className="dashboard-main-grid">
                <div className="activity-section glass-panel">
                    <h3>Recent Sales Activity</h3>
                    <div className="activity-chart">
                        {/* CSS only bar chart for simplicity */}
                        {[60, 40, 75, 50, 90, 30, 80].map((h, i) => (
                            <div key={i} className="chart-bar-wrapper">
                                <div className="chart-bar" style={{ height: `${h}%` }}></div>
                                <span className="chart-label">Day {i + 1}</span>
                            </div>
                        ))}
                    </div>
                </div>

                <div className="recent-orders glass-panel">
                    <h3>Latest Orders</h3>
                    <ul className="order-list">
                        <li className="order-item">
                            <span className="order-id">#ORD-001</span>
                            <span className="order-amount">$120.50</span>
                            <span className="status completed">Completed</span>
                        </li>
                        <li className="order-item">
                            <span className="order-id">#ORD-002</span>
                            <span className="order-amount">$45.00</span>
                            <span className="status pending">Pending</span>
                        </li>
                        <li className="order-item">
                            <span className="order-id">#ORD-003</span>
                            <span className="order-amount">$210.00</span>
                            <span className="status completed">Completed</span>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
    );
}
