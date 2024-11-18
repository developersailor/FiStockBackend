namespace FiStockBackend.DTO;

public class LotDTO
{
    public int LotId { get; set; }
    public string LotNumber { get; set; }
    public DateTime ExpiryDate { get; set; }
    public int ProductId { get; set; }
}
