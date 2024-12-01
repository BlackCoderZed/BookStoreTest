namespace BookStoreWebAPI.Models.Common
{
    public class FilterBase
    {
        public int Start { get; set; }

        public int Length { get; set; }

        public string SearchValue { get; set; }

        public string SortColumn { get; set; }

        public string SortOrder { get; set; }
    }
}
