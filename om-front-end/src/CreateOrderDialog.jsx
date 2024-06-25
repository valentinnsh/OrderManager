import React, { useState } from 'react';
import axios from 'axios';

function CreateOrderDialog({ isOpen, onClose, onOrderCreated }) {
  const [formData, setFormData] = useState({
    externalId: '',
    weight: '',
    collectionDate: '',
    shipperTown: '',
    shipperAddress: '',
    consigneeTown: '',
    consigneeAddress: ''
  });

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData({ ...formData, [name]: value });
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    axios.post('http://localhost:8080/api/orders', formData)
      .then(response => {
        onOrderCreated(response.data);
        onClose();
      })
      .catch(error => {
        console.error("There was an error creating the order!", error);
      });
  };

  if (!isOpen) return null;

  return (
    <div className="dialog">
      <h2>Create New Order</h2>
      <form onSubmit={handleSubmit}>
        <div className='row'>
          <label>Weight (in grams):</label>
          <input type="number" name="weight" value={formData.weight} onChange={handleChange} required />
        </div>
        <div  className='row'>
          <label>Collection Date:</label>
          <input type="date" name="collectionDate" value={formData.collectionDate} onChange={handleChange} required />
        </div>
        <div className='row'>
          <label>Shipper Town:</label>
          <input type="text" name="shipperTown" value={formData.shipperTown} onChange={handleChange} required />
        </div>
        <div className='row'>
          <label>Shipper Address:</label>
          <input type="text" name="shipperAddress" value={formData.shipperAddress} onChange={handleChange} required />
        </div>
        <div className='row'>
          <label>Consignee Town:</label>
          <input type="text" name="consigneeTown" value={formData.consigneeTown} onChange={handleChange} required />
        </div>
        <div className='row'>
          <label>Consignee Address:</label>
          <input type="text" name="consigneeAddress" value={formData.consigneeAddress} onChange={handleChange} required />
        </div>
        <button type="submit">Create Order</button>
        <button type="button" onClick={onClose}>Cancel</button>
      </form>
    </div>
  );
}

export default CreateOrderDialog;