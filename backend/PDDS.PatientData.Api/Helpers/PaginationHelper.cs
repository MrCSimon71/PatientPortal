using PDDS.PatientData.Core.Services;
using PDDS.PatientData.Core.Filters;

namespace PDDS.PatientData.Api.Helpers
{
    public class PaginationHelper
    {
        public static PagedResponse<List<T>> CreatePagedReponse<T>(List<T> pagedData, SearchFilter validFilter, int totalRecords, 
            IUriService uriService, string route)
        {
            var response = new PagedResponse<List<T>>(pagedData);
            var totalPages = ((double)totalRecords / (double)validFilter.PageSize);
            int roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));

            var searchParams = validFilter.SearchCriteria;

            var prevPageUri = validFilter.PageNumber - 1 >= 1 && validFilter.PageNumber <= roundedTotalPages
                ? uriService.GetPageUri(new SearchFilter(validFilter.PageNumber - 1, validFilter.PageSize), route, searchParams)
                : null;

            var nextPageUri = validFilter.PageNumber >= 1 && validFilter.PageNumber < roundedTotalPages
                ? uriService.GetPageUri(new SearchFilter(validFilter.PageNumber + 1, validFilter.PageSize), route, searchParams)
                : null;

            var meta = new Meta()
            {
                Links = new Links()
                {
                    Self = uriService.GetPageUri(new SearchFilter(validFilter.PageNumber, validFilter.PageSize), route, searchParams).ToString(),
                    First = uriService.GetPageUri(new SearchFilter(1, validFilter.PageSize), route, searchParams).ToString(),
                    Prev = prevPageUri?.ToString(),
                    Next = nextPageUri?.ToString(),
                    Last = uriService.GetPageUri(new SearchFilter(roundedTotalPages, validFilter.PageSize), route, searchParams).ToString()
                },
                Count = pagedData.Count,
                CurrentPage = validFilter.PageNumber,
                TotalPages = roundedTotalPages,
                TotalRecords = totalRecords
            };

            response.Meta = meta;

            return response;
        }
    }
}
