using ISPAN.Izakaya.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace Izakayamvc.ViewModels.Exts
{
    public static class CarStatusExts
    {
        public static IEnumerable<CartStatusDto> ContainsStatus(this IEnumerable<CartStatusDto> carStatuses, string status)
        {
            return string.IsNullOrEmpty(status)
                ? carStatuses
                : carStatuses.Where(c => c.Status.Contains(status));
        }
    }
}