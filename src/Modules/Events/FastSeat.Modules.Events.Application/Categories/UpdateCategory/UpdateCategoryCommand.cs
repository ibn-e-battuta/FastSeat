using FastSeat.Common.Application.Messaging;

namespace FastSeat.Modules.Events.Application.Categories.UpdateCategory;

public sealed record UpdateCategoryCommand(Guid CategoryId, string Name) : ICommand;
