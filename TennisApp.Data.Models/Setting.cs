namespace TennisApp.Data.Models
{
    using TennisApp.Data.Common.Models;
    public class Setting : BaseDeletableEntity<int>
    {
        public string Name { get; set; }

        public string Value { get; set; }
    }
}
