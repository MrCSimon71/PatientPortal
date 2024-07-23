using Microsoft.AspNetCore.Mvc.Authorization;

namespace PDDS.PatientData.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AllowAnonymous : Attribute, IAllowAnonymousFilter
    {
    }
}
