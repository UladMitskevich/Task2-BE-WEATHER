namespace WEATHER.API.Dto
{
    /// <summary>
    /// Represents an auditable data for transfer object.
    /// </summary>
    public interface IAuditableDto
    {
        //omittedd for straignhtforwardness
        //public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        //public string CreatedBy { get; set; }

        //public string ModifiedBy { get; set; }
        //public bool IsDeleted { get; set; }
    }
}
