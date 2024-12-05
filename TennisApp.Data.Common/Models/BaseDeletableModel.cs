namespace TennisApp.Data.Common.Models
{
    using System;
    public abstract class BaseDeletableEntity<TKey> : BaseModel<TKey>, IDeletableEntity
    {
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
