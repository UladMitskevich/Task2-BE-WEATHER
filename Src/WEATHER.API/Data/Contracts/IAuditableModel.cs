namespace WEATHER.API.Data.Contracts
{
    /// <summary>
    /// Represents an auditable data model.
    /// </summary>
    public interface IAuditableModel
    {
        //public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        //public string CreatedBy { get; set; }
        //public string ModifiedBy { get; set; }
        //public bool IsDeleted { get; set; }
    }
}
