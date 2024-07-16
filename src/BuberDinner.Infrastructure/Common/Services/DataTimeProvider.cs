using BuberDinner.Application.Common.Services;

namespace BuberDinner.Infrastructure.Common.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}