using AutoMapper;
using BodyMetrica.Features.Common;
using BodyMetrica.Features.Persistence;
using BodyMetrica.Features.Weight.GetRecentWeightLogs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BodyMetrica.Features.BloodPressure.GetRecentBloodPressureLogs;

public class GetRecentBloodPressureLogsQueryHandler(IUserService userService, BodyMetricaDbContext dbContext, IMapper mapper): IRequestHandler<GetRecentBloodPressureLogsQuery, IEnumerable<RecentBloodPressureLogDto>>
{
    public async Task<IEnumerable<RecentBloodPressureLogDto>> Handle(GetRecentBloodPressureLogsQuery request, CancellationToken cancellationToken)
    {
        var user = await userService.GetLoggedInUser();
        var records = await dbContext.BloodPressureLogs
            .Where(x => x.OwnerId == user.Id)
        .OrderByDescending(x => x.RecordDate)
            .Take(request.RecordsCount)
            .ToListAsync(CancellationToken.None);

        return mapper.Map<IEnumerable<RecentBloodPressureLogDto>>(records);

    }
}