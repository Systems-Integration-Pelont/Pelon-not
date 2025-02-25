namespace Broker.DTO;

public class AtmStatus
{
    public string AtmId { get; set; }
    public string Location { get; set; }
    public string Status { get; set; }
    public DateTime LastChecked { get; set; }
}