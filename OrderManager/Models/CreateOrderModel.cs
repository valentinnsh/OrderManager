namespace OrderManager.Models;

public class CreateOrderModel
{
    public long Weight { get; set; }
    public DateTime CollectionDate { get; set; }
    public string ShipperTown { get; set; }
    public string ConsigneeTown { get; set; }
    public string ShipperAddress { get; set; }
    public string ConsigneeAddress { get; set; }
}