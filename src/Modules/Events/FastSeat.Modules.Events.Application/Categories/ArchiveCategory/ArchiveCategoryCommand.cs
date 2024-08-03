using FastSeat.Common.Application.Messaging;

namespace FastSeat.Modules.Events.Application.Categories.ArchiveCategory;

public sealed record ArchiveCategoryCommand(Guid CategoryId) : ICommand;
