using Domain.Enums;

namespace Domain.Helpers;

public static class DateFilterHelper
{
    public const int DefaultMaxDays = 365;
    public const int ShortRangeMaxDays = 30;
    public const int MediumRangeMaxDays = 90;

    public static (DateTimeOffset startDate, DateTimeOffset endDate) GetDateRange(DateFilterType dateFilterType, DateTimeOffset? customStartDate = null, DateTimeOffset? customEndDate = null)
    {
        var now = DateTimeOffset.UtcNow;
        var startOfDay = new DateTimeOffset(now.Date, now.Offset);
        var endOfDay = startOfDay.AddDays(1).AddTicks(-1); // Get end of current UTC day: 23:59:59.9999999;

        return dateFilterType switch
        {
            DateFilterType.Last7Days => (startOfDay.AddDays(-7), endOfDay),
            DateFilterType.Last14Days => (startOfDay.AddDays(-14), endOfDay),
            DateFilterType.Last30Days => (startOfDay.AddDays(-30), endOfDay),
            DateFilterType.Last90Days => (startOfDay.AddDays(-90), endOfDay),
            DateFilterType.LastYear => (startOfDay.AddYears(-1), endOfDay),
            DateFilterType.CustomRange => (
                customStartDate ?? startOfDay.AddDays(-30),
                customEndDate ?? endOfDay
            ),
            _ => (startOfDay.AddDays(-30), endOfDay)
        };
    }

    public static bool IsValidCustomDateRange(DateTimeOffset? startDate, DateTimeOffset? endDate, int? maxDaysLimit = null)
    {
        if (!startDate.HasValue || !endDate.HasValue)
            return false;

        if (startDate.Value > endDate.Value)
            return false;

        if (maxDaysLimit.HasValue)
        {
            var daysDifference = (endDate.Value - startDate.Value).TotalDays;
            if (daysDifference > maxDaysLimit.Value)
                return false;
        }

        return true;
    }
}