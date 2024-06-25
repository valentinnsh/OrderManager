import React, { useState, useEffect } from 'react';
import axios from 'axios';
import CreateOrderDialog from './CreateOrderDialog';
import OrderDetailsDialog from './OrderDetailsDialog';

function TableData() {
  const [orderData, setOrderData] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [isCreateDialogOpen, setIsCreateDialogOpen] = useState(false);
  const [isDetailDialogOpen, setIsDetailDialogOpen] = useState(false);
  const [selectedOrderId, setSelectedOrderId] = useState(null);

  useEffect(() => {
    fetchData();
  }, []);

  const fetchData = () => {
    axios.get('http://localhost:8080/api/orders')
      .then(response => {
        setOrderData(response.data);
        setLoading(false);
      })
      .catch(error => {
        setError(error);
        setLoading(false);
      });
  };

  const handleOrderCreated = (newOrder) => {
    setOrderData([...orderData, newOrder]);
  };

  const handleRowClick = (orderId) => {
    setSelectedOrderId(orderId);
    setIsDetailDialogOpen(true);
  };

  if (loading) {
    return <div>Loading...</div>;
  }

  if (error) {
    return <div>Error: {error.message}</div>;
  }

  const tableRows = orderData.map((info) => (
    <tr key={info.externalId} onClick={() => handleRowClick(info.externalId)}>
      <td>{info.externalId}</td>
      <td>{info.weight}</td>
      <td>{info.collectionDate}</td>
      <td>{info.shipperTown}</td>
      <td>{info.shipperAddress}</td>
      <td>{info.consigneeTown}</td>
      <td>{info.consigneeAddress}</td>
    </tr>
  ));

  return (
    <div>
        <div style={{ paddingBottom: '15px' }}>
            <button className='button' onClick={() => setIsCreateDialogOpen(true)}>Create New Order</button>
        </div>

        <table className="table table-stripped">
        <thead>
          <tr>
            <th>Id</th>
            <th>Weight</th>
            <th>Date</th>
            <th>Shipper Town</th>
            <th>Shipper Address</th>
            <th>Consignee Town</th>
            <th>Consignee Address</th>
          </tr>
        </thead>
        <tbody>{tableRows}</tbody>
      </table>
      <CreateOrderDialog
        isOpen={isCreateDialogOpen}
        onClose={() => setIsCreateDialogOpen(false)}
        onOrderCreated={handleOrderCreated}
      />
      <OrderDetailsDialog
        isOpen={isDetailDialogOpen}
        onClose={() => setIsDetailDialogOpen(false)}
        orderId={selectedOrderId}
      />
    </div>
  );
}

export default TableData;
