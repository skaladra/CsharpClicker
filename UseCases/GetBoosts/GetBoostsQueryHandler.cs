using AutoMapper;
using CSharpClicker.Web.Infrastructure.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CSharpClicker.Web.UseCases.GetBoosts;

public class GetBoostsQueryHandler : IRequestHandler<GetBoostsQuery, IReadOnlyCollection<BoostDto>>
{
    private IAppDbContext appDbContext;
    private IMapper mapper;

    public GetBoostsQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        this.appDbContext = appDbContext;
        this.mapper = mapper;
    }

    public async Task<IReadOnlyCollection<BoostDto>> Handle(GetBoostsQuery request, CancellationToken cancellationToken)
    {
        return await mapper
            .ProjectTo<BoostDto>(appDbContext.Boosts)
            .ToArrayAsync();
    }
}
