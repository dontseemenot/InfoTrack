namespace SettlementService.Models
{
    public class GuidGenerator : IGuidGenerator
    {
        public Guid GenerateGuid()
        {
            return Guid.NewGuid();
        }
    }
}
