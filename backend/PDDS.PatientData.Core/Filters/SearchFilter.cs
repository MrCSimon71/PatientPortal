using Microsoft.AspNetCore.Http;

namespace PDDS.PatientData.Core.Filters
{
    public class SearchFilter
    {
        private readonly int _maxPageSize = 50;

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public List<SearchCriteria> SearchCriteria { get; set; }

        public SearchFilter()
        {
            SearchCriteria = new List<SearchCriteria>();
            PageNumber = 1;
            PageSize = 50;
        }

        public SearchFilter(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize > _maxPageSize ? _maxPageSize : pageSize;
        }

        public void ApplyRules()
        {
            PageNumber = PageNumber < 1 ? 1 : PageNumber;
            PageSize = PageSize > _maxPageSize ? _maxPageSize : PageSize;
        }

        public void SetPagingAttributes(IQueryCollection queryParams)
        {
            var pagingParams = queryParams.Where(p => p.Key.StartsWith("page"));

            foreach (var param in pagingParams)
            {
                if (string.IsNullOrWhiteSpace(param.Value))
                    continue;

                switch (param.Key.ToLower())
                {
                    case "pagesize":
                        this.PageSize = Convert.ToInt16(param.Value[0]);
                        break;
                    case "pagenumber":
                        this.PageNumber = Convert.ToInt16(param.Value[0]);
                        break;
                }
            }
        }
    }
}
