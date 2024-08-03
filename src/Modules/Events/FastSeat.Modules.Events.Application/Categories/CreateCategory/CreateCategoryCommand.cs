using FastSeat.Common.Application.Messaging;

namespace FastSeat.Modules.Events.Application.Categories.CreateCategory;

public sealed record CreateCategoryCommand(string Name) : ICommand<Guid>;
