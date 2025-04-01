namespace TaskManager.Core.DomainObjects
{
    /// <summary>
    /// Represents the pagination settings for limiting the amount of registers returned by gets.
    /// </summary>
    public class PaginationSettings
    {
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
