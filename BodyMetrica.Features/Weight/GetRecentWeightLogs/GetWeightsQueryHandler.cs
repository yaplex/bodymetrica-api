using AutoMapper;
using BodyMetrica.Features.Common;
using BodyMetrica.Features.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace BodyMetrica.Features.Weight.GetRecentWeightLogs;

public class GetWeightsQueryHandler(
    IUserService userService,
    IMapper mapper, BodyMetricaDbContext dbContext, ILogger logger
) : IRequestHandler<GetWeightLogsQuery, IEnumerable<RecentWeightLogDto>>
{
    public async Task<IEnumerable<RecentWeightLogDto>> Handle(GetWeightLogsQuery request,
        CancellationToken cancellationToken)
    {
        var user = await userService.GetLoggedInUser();
        var weightLogRecords = await dbContext.WeightLogs
            .Where(x => x.OwnerId == user.Id)
            .OrderByDescending(x => x.RecordDate)
            .Take(request.RecordsCount)
            .ToListAsync(CancellationToken.None);

        return mapper.Map<IEnumerable<RecentWeightLogDto>>(weightLogRecords);
    }
}