namespace Saibadata.ApiTools
{
    public class ResourceParametersBase
    {
        const int maxPageSize = 20;
        private int _pageSize = 10;
        public string SearchQuery { get; set; }  
        public int PageNumber { get; set; } = 1;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > maxPageSize) ? maxPageSize : value;
        }
        public virtual string OrderBy { get; set; }
        public string Fields { get; set; }
    }
}
