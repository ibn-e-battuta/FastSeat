using System.Data.Common;
using Dapper;
using FastSeat.Common.Application.Data;
using FastSeat.Common.Application.Messaging;
using FastSeat.Common.Domain;
using FastSeat.Modules.Events.Application.Categories.GetCategory;

namespace FastSeat.Modules.Events.Application.Categories.GetCategories;

internal sealed class GetCategoriesQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetCategoriesQuery, IReadOnlyCollection<CategoryResponse>>
{
    public async Task<Result<IReadOnlyCollection<CategoryResponse>>> Handle(
        GetCategoriesQuery request,
        CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 id AS {nameof(CategoryResponse.Id)},
                 name AS {nameof(CategoryResponse.Name)},
                 is_archived AS {nameof(CategoryResponse.IsArchived)}
             FROM events.categories
             """;

        List<CategoryResponse> categories = (await connection.QueryAsync<CategoryResponse>(sql, request)).AsList();

        return categories;
    }
}
