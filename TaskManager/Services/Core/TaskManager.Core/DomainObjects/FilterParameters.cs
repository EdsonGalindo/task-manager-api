namespace TaskManager.Core.DomainObjects
{
    public abstract class FilterParameters
    {
        public FilterParameters(PaginationSettings paginationSettings)
        {
            MaxPageSize = paginationSettings.MaxPageSize;
            DefaultPageSize = paginationSettings.DefaultPageSize;
            DefaultPageNumber = paginationSettings.DefaultPageNumber;
        }

        /// <summary>
        /// The maximum page size allowed.
        /// </summary>
        public int MaxPageSize { get; set; } = 20;

        /// <summary>
        /// The default page size.
        /// </summary>
        public int DefaultPageSize { get; set; } = 10;

        /// <summary>
        /// The default page number.
        /// </summary>
        public int DefaultPageNumber { get; set; } = 1;

    }
}
