using FastSeat.Common.Application.Messaging;

namespace FastSeat.Modules.Events.Application.Categories.GetCategory;

public sealed record GetCategoryQuery(Guid CategoryId) : IQuery<CategoryResponse>;
