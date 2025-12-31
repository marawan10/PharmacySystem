import { useState, useEffect } from 'react';
import './Medicines.css';

export default function Medicines() {
    const [medicines, setMedicines] = useState([]);
    const [loading, setLoading] = useState(true);
    const [showModal, setShowModal] = useState(false);
    const [isEditing, setIsEditing] = useState(false);
    const [formData, setFormData] = useState({
        id: '',
        name: '',
        genericName: '',
        costPrice: '',
        sellingPrice: '',
        stockQuantity: '',
        expiryDate: '',
        isDiscontinued: false,
        categoryId: 1 // Default category
    });
    const [message, setMessage] = useState({ type: '', text: '' });

    const fetchMedicines = async () => {
        try {
            const token = localStorage.getItem('token');
            const response = await fetch(`${import.meta.env.VITE_API_URL}/api/Medicines`, {
                headers: {
                    'Authorization': `Bearer ${token}`
                }
            });
            if (response.ok) {
                const data = await response.json();
                setMedicines(data);
            }
        } catch (error) {
            console.error('Error fetching medicines:', error);
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        fetchMedicines();
    }, []);

    const handleInputChange = (e) => {
        const { name, value, type, checked } = e.target;
        setFormData({
            ...formData,
            [name]: type === 'checkbox' ? checked : value
        });
    };

    const resetForm = () => {
        setFormData({
            id: '',
            name: '',
            genericName: '',
            costPrice: '',
            sellingPrice: '',
            stockQuantity: '',
            expiryDate: '',
            isDiscontinued: false,
            categoryId: 1
        });
        setIsEditing(false);
    };

    const openCreateModal = () => {
        resetForm();
        setShowModal(true);
    };

    const handleEditClick = async (medicineId) => {
        setMessage({ type: '', text: '' });
        try {
            const token = localStorage.getItem('token');
            const response = await fetch(`${import.meta.env.VITE_API_URL}/api/Medicines/${medicineId}`, {
                headers: { 'Authorization': `Bearer ${token}` }
            });

            if (response.ok) {
                const data = await response.json();
                setFormData({
                    id: data.id,
                    name: data.name,
                    genericName: data.genericName,
                    costPrice: data.costPrice,
                    sellingPrice: data.sellingPrice,
                    stockQuantity: data.stockQuantity,
                    expiryDate: data.expiryDate.split('T')[0], // Format date for input
                    isDiscontinued: data.isDiscontinued,
                    categoryId: data.categoryId
                });
                setIsEditing(true);
                setShowModal(true);
            } else {
                setMessage({ type: 'error', text: 'Failed to fetch medicine details' });
            }
        } catch (error) {
            console.error(error);
            setMessage({ type: 'error', text: 'Error fetching medicine details' });
        }
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        setMessage({ type: '', text: '' });

        try {
            const token = localStorage.getItem('token');

            const payload = {
                name: formData.name,
                genericName: formData.genericName,
                costPrice: parseFloat(formData.costPrice),
                sellingPrice: parseFloat(formData.sellingPrice),
                stockQuantity: parseInt(formData.stockQuantity),
                expiryDate: formData.expiryDate,
                isDiscontinued: formData.isDiscontinued,
                categoryId: parseInt(formData.categoryId)
            };

            let response;
            if (isEditing) {
                // UPDATE
                response = await fetch(`${import.meta.env.VITE_API_URL}/api/Medicines/${formData.id}`, {
                    method: 'PUT',
                    headers: {
                        'Authorization': `Bearer ${token}`,
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(payload)
                });

                if (response.ok) {
                    setMessage({ type: 'success', text: 'Medicine updated successfully!' });
                    setShowModal(false);
                    fetchMedicines();
                } else {
                    const data = await response.json();
                    setMessage({ type: 'error', text: data.message || 'Failed to update medicine' });
                }
            } else {
                // CREATE
                response = await fetch(`${import.meta.env.VITE_API_URL}/api/Medicines/Add`, {
                    method: 'POST',
                    headers: {
                        'Authorization': `Bearer ${token}`,
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(payload)
                });

                if (response.ok) {
                    setMessage({ type: 'success', text: 'Medicine created successfully!' });
                    setShowModal(false);
                    fetchMedicines();
                } else {
                    const text = await response.text();
                    setMessage({ type: 'error', text: text || 'Failed to create medicine' });
                }
            }
        } catch (error) {
            console.error('Error saving medicine:', error);
            setMessage({ type: 'error', text: 'Server error occurred.' });
        }
    };

    const handleDelete = async (medicineId) => {
        if (!window.confirm('Are you sure you want to delete this medicine?')) return;

        try {
            const token = localStorage.getItem('token');
            const response = await fetch(`${import.meta.env.VITE_API_URL}/api/Medicines/${medicineId}`, {
                method: 'DELETE',
                headers: { 'Authorization': `Bearer ${token}` }
            });

            if (response.ok) {
                setMessage({ type: 'success', text: 'Medicine deleted successfully' });
                fetchMedicines();
            } else {
                const data = await response.json();
                setMessage({ type: 'error', text: data.message || 'Failed to delete medicine' });
            }
        } catch (error) {
            console.error(error);
            setMessage({ type: 'error', text: 'Error deleting medicine' });
        }
    };

    // Calculate low stock count
    const lowStockCount = medicines.filter(m => m.stockQuantity <= 10 && m.stockQuantity > 0).length;
    const outOfStockCount = medicines.filter(m => m.stockQuantity === 0).length;

    return (
        <div className="medicines-page">
            <div className="medicines-header">
                <h3>📦 Inventory Management</h3>
                <button className="btn-primary" onClick={openCreateModal}>
                    + Add Medicine
                </button>
            </div>

            {/* Stats Cards */}
            <div className="stats-grid-medicines">
                <div className="stat-card glass-panel">
                    <div className="stat-icon">💊</div>
                    <div className="stat-info">
                        <p className="stat-label">Total Medicines</p>
                        <h3>{medicines.length}</h3>
                    </div>
                </div>
                <div className="stat-card glass-panel warning">
                    <div className="stat-icon">⚠️</div>
                    <div className="stat-info">
                        <p className="stat-label">Low Stock</p>
                        <h3>{lowStockCount}</h3>
                    </div>
                </div>
                <div className="stat-card glass-panel danger">
                    <div className="stat-icon">🚫</div>
                    <div className="stat-info">
                        <p className="stat-label">Out of Stock</p>
                        <h3>{outOfStockCount}</h3>
                    </div>
                </div>
            </div>

            {message.text && (
                <div className={`alert ${message.type}`} style={{ marginBottom: '1rem' }}>
                    {message.text}
                </div>
            )}

            <div className="glass-panel medicines-table-container">
                {loading ? (
                    <p className="loading-text">Loading medicines...</p>
                ) : (
                    <table className="medicines-table">
                        <thead>
                            <tr>
                                <th>Name</th>
                                <th>Generic Name</th>
                                <th>Stock</th>
                                <th>Selling Price</th>
                                <th>Expiry Date</th>
                                <th>Status</th>
                                <th>Actions</th>
                            </tr>
                        </thead>
                        <tbody>
                            {medicines.map((medicine) => (
                                <tr key={medicine.id}>
                                    <td>{medicine.name}</td>
                                    <td>{medicine.genericName}</td>
                                    <td>
                                        <span className={`stock-badge ${medicine.stockQuantity === 0 ? 'out-of-stock' :
                                            medicine.stockQuantity <= 10 ? 'low-stock' : 'in-stock'
                                            }`}>
                                            {medicine.stockQuantity}
                                        </span>
                                    </td>
                                    <td>${medicine.sellingPrice?.toFixed(2)}</td>
                                    <td>{new Date(medicine.expiryDate).toLocaleDateString()}</td>
                                    <td>
                                        {medicine.isDiscontinued ? (
                                            <span className="status-badge discontinued">Discontinued</span>
                                        ) : (
                                            <span className="status-badge active">Active</span>
                                        )}
                                    </td>
                                    <td>
                                        <button className="btn-icon" onClick={() => handleEditClick(medicine.id)}>✏️</button>
                                        <button className="btn-icon delete-btn" onClick={() => handleDelete(medicine.id)} title="Delete Medicine">🗑️</button>
                                    </td>
                                </tr>
                            ))}
                            {medicines.length === 0 && (
                                <tr>
                                    <td colSpan="7" className="text-center">No medicines found.</td>
                                </tr>
                            )}
                        </tbody>
                    </table>
                )}
            </div>

            {/* Modal */}
            {showModal && (
                <div className="modal-overlay">
                    <div className="modal-content glass-panel">
                        <div className="modal-header">
                            <h3>{isEditing ? 'Edit Medicine' : 'Add New Medicine'}</h3>
                            <button className="close-btn" onClick={() => setShowModal(false)}>×</button>
                        </div>
                        <form onSubmit={handleSubmit}>
                            <div className="form-row">
                                <div className="form-group">
                                    <label>Medicine Name *</label>
                                    <input name="name" value={formData.name} onChange={handleInputChange} required />
                                </div>
                                <div className="form-group">
                                    <label>Generic Name *</label>
                                    <input name="genericName" value={formData.genericName} onChange={handleInputChange} required />
                                </div>
                            </div>
                            <div className="form-row">
                                <div className="form-group">
                                    <label>Cost Price *</label>
                                    <input type="number" step="0.01" name="costPrice" value={formData.costPrice} onChange={handleInputChange} required />
                                </div>
                                <div className="form-group">
                                    <label>Selling Price *</label>
                                    <input type="number" step="0.01" name="sellingPrice" value={formData.sellingPrice} onChange={handleInputChange} required />
                                </div>
                            </div>
                            <div className="form-row">
                                <div className="form-group">
                                    <label>Stock Quantity *</label>
                                    <input type="number" name="stockQuantity" value={formData.stockQuantity} onChange={handleInputChange} required />
                                </div>
                                <div className="form-group">
                                    <label>Expiry Date *</label>
                                    <input type="date" name="expiryDate" value={formData.expiryDate} onChange={handleInputChange} required />
                                </div>
                            </div>
                            <div className="form-group checkbox-group">
                                <label>
                                    <input type="checkbox" name="isDiscontinued" checked={formData.isDiscontinued} onChange={handleInputChange} />
                                    Mark as Discontinued
                                </label>
                            </div>

                            <div className="modal-actions">
                                <button type="button" className="btn-secondary" onClick={() => setShowModal(false)}>Cancel</button>
                                <button type="submit" className="btn-primary">{isEditing ? 'Update' : 'Create'}</button>
                            </div>
                        </form>
                    </div>
                </div>
            )}
        </div>
    );
}
