namespace Hackathon_CV_Portal.Data.Pagination
{
    public static class Pagination
    {
        public static DomainPagedResult<T> Paginate<T>(this IQueryable<T> collection, DomainPagedQueryBase query)
        {
            return collection.Paginate(query.Page, query.Results);
        }

        private static DomainPagedResult<T> Paginate<T>(this IQueryable<T> collection, int page = 1, int resultsPerPage = 10)
        {
            if (page <= 0)
            {
                page = 1;
            }
            if (resultsPerPage <= 0)
            {
                resultsPerPage = 10;
            }
            bool isEmpty = collection.Any() == false;

            if (isEmpty)
            {
                return DomainPagedResult<T>.Empty;
            }

            int totalResults = collection.Count();
            int totalPages = (int)Math.Ceiling((decimal)totalResults / resultsPerPage);
            List<T> data = collection.Limit(page, resultsPerPage).ToList();
            return DomainPagedResult<T>.Create(data, page, resultsPerPage, totalPages, totalResults);
        }

        public static IQueryable<T> Limit<T>(this IQueryable<T> collection, int page = 1, int resultsPerPage = 10)
        {
            if (page <= 0)
            {
                page = 1;
            }
            if (resultsPerPage <= 0)
            {
                resultsPerPage = 10;
            }
            int skip = (page - 1) * resultsPerPage;
            IQueryable<T> data = collection.Skip(skip).Take(resultsPerPage);
            return data;
        }
    }

    public class DomainPagedQueryBase
    {
        public int Page { get; set; }
        public int Results { get; set; }
        public DomainPagedQueryBase(int page, int result)
        {
            Page = page;
            Results = result;
        }
    }

    public class DomainPagedResult<T> : DomainPagedResultBase
    {
        public List<T> Items { get; set; }
        public bool IsEmpty => Items == null || !Items.Any();
        protected DomainPagedResult()
        {
            Items = new List<T>(0);
        }
        protected DomainPagedResult(List<T> items, int currentPage, int resultsPerPage, int totalPages, long totalResults)
            : base(currentPage, resultsPerPage, totalPages, totalResults)
        {
            Items = items;
        }
        public static DomainPagedResult<T> Create(List<T> items, int currentPage, int resultsPerPage, int totalPages, long totalResults)
        {
            return new DomainPagedResult<T>(items, currentPage, resultsPerPage, totalPages, totalResults);
        }

        public static DomainPagedResult<T> Empty => new DomainPagedResult<T>();
    }

    public abstract class DomainPagedResultBase
    {
        public int CurrentPage { get; set; }
        public int ResultsPerPage { get; set; }
        public int TotalPages { get; set; }
        public long TotalResults { get; set; }
        protected DomainPagedResultBase()
        {
        }
        protected DomainPagedResultBase(int currentPage, int resultsPerPage, int totalPages, long totalResults)
        {
            CurrentPage = currentPage > totalPages ? totalPages : currentPage;
            ResultsPerPage = resultsPerPage;
            TotalPages = totalPages;
            TotalResults = totalResults;
        }
    }
}