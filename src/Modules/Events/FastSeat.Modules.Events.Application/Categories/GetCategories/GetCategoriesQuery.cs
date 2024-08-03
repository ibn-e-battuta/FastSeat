using FastSeat.Common.Application.Messaging;
using FastSeat.Modules.Events.Application.Categories.GetCategory;

namespace FastSeat.Modules.Events.Application.Categories.GetCategories;

public sealed record GetCategoriesQuery : IQuery<IReadOnlyCollection<CategoryResponse>>;
