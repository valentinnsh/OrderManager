import React, { useState, useEffect } from 'react';
import axios from 'axios';

function OrderDetailDialog({ isOpen, onClose, orderId }) {
  const [order, setOrder] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    if (isOpen && orderId) {
      axios.get(`http://localhost:8080/api/orders/${orderId}`)
        .then(response => {
          setOrder(response.data);
          setLoading(false);
        })
        .catch(error => {
          setError(error);
          setLoading(false);
        });
    }
  }, [isOpen, orderId]);

  if (!isOpen) return null;
  if (loading) return <div className="dialog">Loading...</div>;
  if (error) return <div className="dialog">Error: {error.message}</div>;

  return (
    <div className="dialog">
      <h2>Order Details</h2>
      <div>
        <strong>External ID:</strong> {order.externalId}
      </div>
      <div>
        <strong>Weight:</strong> {order.weight}
      </div>
      <div>
        <strong>Collection Date:</strong> {order.collectionDate}
      </div>
      <div>
        <strong>Shipper Town:</strong> {order.shipperTown}
      </div>
      <div>
        <strong>Shipper Address:</strong> {order.shipperAddress}
      </div>
      <div>
        <strong>Consignee Town:</strong> {order.consigneeTown}
      </div>
      <div>
        <strong>Consignee Address:</strong> {order.consigneeAddress}
      </div>
      <button onClick={onClose}>Close</button>
    </div>
  );
}

export default OrderDetailDialog;
