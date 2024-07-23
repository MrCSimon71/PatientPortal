using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PDDS.PatientData.Core.Filters;

namespace PDDS.PatientData.Core.Services
{
    public interface IUriService
    {
        public Uri GetPageUri(SearchFilter filter, string route, List<SearchCriteria> searchParams);
    }
}
