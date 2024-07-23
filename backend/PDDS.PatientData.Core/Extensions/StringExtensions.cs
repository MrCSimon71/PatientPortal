using PluralizeService.Core;

namespace PDDS.PatientData.Core.Extensions
{
    public static class StringExtensions
    {
        public static string Pluralize(this string @this)
        {
            return PluralizationProvider.Pluralize(@this);
        }

        public static string Singularize(this string @this)
        {
            return PluralizationProvider.Singularize(@this);
        }
    }
}
