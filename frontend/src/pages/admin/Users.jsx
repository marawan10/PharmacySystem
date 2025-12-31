import { useState, useEffect } from 'react';
import './Users.css';

export default function Users() {
    const [users, setUsers] = useState([]);
    const [loading, setLoading] = useState(true);
    const [showModal, setShowModal] = useState(false);
    const [formData, setFormData] = useState({
        id: '', // Added ID for edit
        username: '',
        email: '',
        password: '',
        firstname: '',
        lastname: '',
        role: 'Admin' // Default role
    });
    const [isEditing, setIsEditing] = useState(false); // Track edit mode
    const [message, setMessage] = useState({ type: '', text: '' });

    const fetchUsers = async () => {
        try {
            const token = localStorage.getItem('token');
            const response = await fetch(`${import.meta.env.VITE_API_URL}/api/Users`, {
                headers: {
                    'Authorization': `Bearer ${token}`
                }
            });
            if (response.ok) {
                const data = await response.json();
                setUsers(data);
            }
        } catch (error) {
            console.error('Error fetching users:', error);
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        fetchUsers();
    }, []);

    const handleInputChange = (e) => {
        setFormData({ ...formData, [e.target.name]: e.target.value });
    };

    const resetForm = () => {
        setFormData({
            id: '',
            username: '',
            email: '',
            password: '',
            firstname: '',
            lastname: '',
            role: 'Admin'
        });
        setIsEditing(false);
    };

    const openCreateModal = () => {
        resetForm();
        setShowModal(true);
    };

    const handleEditClick = async (userId) => {
        setMessage({ type: '', text: '' });
        try {
            const token = localStorage.getItem('token');
            const response = await fetch(`${import.meta.env.VITE_API_URL}/api/Users/${userId}`, {
                headers: { 'Authorization': `Bearer ${token}` }
            });

            if (response.ok) {
                const data = await response.json();
                // Map API response to form state
                setFormData({
                    id: userId,
                    username: data.userName,
                    email: data.email,
                    password: '', // Password usually empty on edit, or optional
                    firstname: data.firstname,
                    lastname: data.lastname,
                    role: data.type || 'Admin'
                });
                setIsEditing(true);
                setShowModal(true);
            } else {
                setMessage({ type: 'error', text: 'Failed to fetch user details' });
            }
        } catch (error) {
            console.error(error);
            setMessage({ type: 'error', text: 'Error fetching user details' });
        }
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        setMessage({ type: '', text: '' });

        try {
            const token = localStorage.getItem('token');

            if (isEditing) {
                // EDIT MODE
                const payload = {
                    Id: formData.id,
                    firstname: formData.firstname,
                    lastname: formData.lastname,
                    email: formData.email,
                    UserName: formData.username,
                    Role: formData.role
                    // Password NOT sent in this basic edit implementation as per DTO
                };

                const response = await fetch(`${import.meta.env.VITE_API_URL}/api/Users/edit-user`, {
                    method: 'PATCH',
                    headers: {
                        'Authorization': `Bearer ${token}`,
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(payload)
                });

                if (response.ok) {
                    setMessage({ type: 'success', text: 'User updated successfully!' });
                    setShowModal(false);
                    fetchUsers();
                } else {
                    const data = await response.json();
                    setMessage({ type: 'error', text: data.message || 'Failed to update user' });
                }

            } else {
                // CREATE MODE
                const payload = {
                    UserName: formData.username,
                    email: formData.email,
                    password: formData.password,
                    firstname: formData.firstname,
                    lastname: formData.lastname,
                    Role: formData.role
                };

                const response = await fetch(`${import.meta.env.VITE_API_URL}/api/Users/create-user`, {
                    method: 'POST',
                    headers: {
                        'Authorization': `Bearer ${token}`,
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(payload)
                });

                if (response.ok) {
                    setMessage({ type: 'success', text: 'User created successfully!' });
                    setShowModal(false);
                    fetchUsers();
                } else {
                    const text = await response.text();
                    try {
                        const errors = JSON.parse(text);
                        if (Array.isArray(errors)) {
                            setMessage({ type: 'error', text: errors.map(e => e.description).join(', ') });
                        } else {
                            setMessage({ type: 'error', text: text });
                        }
                    } catch {
                        setMessage({ type: 'error', text: text || 'Failed to create user' });
                    }
                }
            }
        } catch (error) {
            console.error('Error saving user:', error);
            setMessage({ type: 'error', text: 'Server error occurred.' });
        }
    };

    const handleDelete = async (userId) => {
        if (!window.confirm('Are you sure you want to delete this user?')) return;

        try {
            const token = localStorage.getItem('token');
            const response = await fetch(`${import.meta.env.VITE_API_URL}/api/Users/delete-user?id=${userId}`, {
                method: 'DELETE',
                headers: { 'Authorization': `Bearer ${token}` }
            });

            if (response.ok) {
                setMessage({ type: 'success', text: 'User deleted successfully' });
                fetchUsers();
            } else {
                const data = await response.json();
                setMessage({ type: 'error', text: data.message || 'Failed to delete user' });
            }
        } catch (error) {
            console.error(error);
            setMessage({ type: 'error', text: 'Error deleting user' });
        }
    };

    return (
        <div className="users-page">
            <div className="users-header-actions">
                <h3>User Management</h3>
                <button className="btn-primary" onClick={openCreateModal}>
                    + Add New User
                </button>
            </div>

            {message.text && (
                <div className={`alert ${message.type}`} style={{ marginBottom: '1rem' }}>
                    {message.text}
                </div>
            )}

            <div className="glass-panel users-table-container">
                {loading ? (
                    <p className="loading-text">Loading users...</p>
                ) : (
                    <table className="users-table">
                        <thead>
                            <tr>
                                <th>Username</th>
                                <th>Email</th>
                                <th>Role</th>
                                <th>Actions</th>
                            </tr>
                        </thead>
                        <tbody>
                            {users.map((user, index) => (
                                <tr key={index}>
                                    <td>{user.userName}</td>
                                    <td>{user.email}</td>
                                    <td>
                                        <span className={`role-badge ${user.type?.toLowerCase()}`}>
                                            {user.type}
                                        </span>
                                    </td>
                                    <td>
                                        <button className="btn-icon" onClick={() => handleEditClick(user.id)}>✏️</button>
                                        <button className="btn-icon delete-btn" onClick={() => handleDelete(user.id)} title="Delete User">🗑️</button>
                                    </td>
                                </tr>
                            ))}
                            {users.length === 0 && (
                                <tr>
                                    <td colSpan="4" className="text-center">No users found.</td>
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
                            <h3>{isEditing ? 'Edit User' : 'Create New User'}</h3>
                            <button className="close-btn" onClick={() => setShowModal(false)}>×</button>
                        </div>
                        <form onSubmit={handleSubmit}>
                            <div className="form-group">
                                <label>First Name</label>
                                <input name="firstname" value={formData.firstname} onChange={handleInputChange} required />
                            </div>
                            <div className="form-group">
                                <label>Last Name</label>
                                <input name="lastname" value={formData.lastname} onChange={handleInputChange} required />
                            </div>
                            <div className="form-group">
                                <label>Username</label>
                                <input name="username" value={formData.username} onChange={handleInputChange} required />
                            </div>
                            <div className="form-group">
                                <label>Email</label>
                                <input type="email" name="email" value={formData.email} onChange={handleInputChange} required />
                            </div>
                            {!isEditing && (
                                <div className="form-group">
                                    <label>Password</label>
                                    <input type="password" name="password" value={formData.password} onChange={handleInputChange} required />
                                </div>
                            )}
                            <div className="form-group">
                                <label>Role</label>
                                <select name="role" value={formData.role} onChange={handleInputChange}>
                                    <option value="Admin">Admin</option>
                                    <option value="Pharmacist">Pharmacist</option>
                                </select>
                            </div>

                            <div className="modal-actions">
                                <button type="button" className="btn-secondary" onClick={() => setShowModal(false)}>Cancel</button>
                                <button type="submit" className="btn-primary">{isEditing ? 'Save Changes' : 'Create User'}</button>
                            </div>
                        </form>
                    </div>
                </div>
            )}
        </div>
    );
}
