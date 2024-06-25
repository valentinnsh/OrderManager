using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Records;

public class OrderRecord
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public long Id { get; set; }
    [Column("external_id")]
    public Guid ExternalId { get; set; }
    // In gramms
    [Column("weight")]
    public long Weight { get; set; }
    [Column("collection_date")]
    public DateTime CollectionDate { get; set; }
    [Column("shipper_town")]
    public string ShipperTown { get; set; }
    [Column("consignee_town")]
    public string ConsigneeTown { get; set; }
    [Column("shipper_address")]
    public string ShipperAddress { get; set; }
    [Column("consignee_address")]
    public string ConsigneeAddress { get; set; }
}