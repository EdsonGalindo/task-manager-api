namespace TaskManager.Core.DomainObjects
{
    public abstract class FilterParameters
    {
        public FilterParameters(PaginationSettings paginationSettings)
        {
            MaxPageSize = paginationSettings.MaxPageSize;
            PageNumber = paginationSettings.DefaultPageNumber;
            _pageSize = paginationSettings.DefaultPageSize;
        }

        protected FilterParameters()
        {
            var paginationSettings = new PaginationSettings();
            MaxPageSize = paginationSettings.MaxPageSize;
            PageNumber = paginationSettings.DefaultPageNumber;
            _pageSize = paginationSettings.DefaultPageSize;
        }

        /// <summary>
        /// The maximum page size allowed.
        /// </summary>
        private int MaxPageSize { get; set; }

        /// <summary>
        /// The default page number.
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// The default page size or the value set by the request 
        /// if it is less or equal the maximum page size
        /// </summary>
        private int _pageSize { get; set; }

        /// <summary>
        /// The request page size.
        /// </summary>
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                if (value == 0)
                {
                    return;
                }
                _pageSize = value > MaxPageSize ? MaxPageSize : value;
            }
        }

    }
}
