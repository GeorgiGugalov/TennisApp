namespace TennisApp.Models
{
    using TennisApp.Data.Common;

    public class Setting : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public string Value { get; set; }
    }
}
