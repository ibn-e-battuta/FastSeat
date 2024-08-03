using FastSeat.Common.Application.Messaging;
using FastSeat.Common.Domain;
using FastSeat.Modules.Events.Application.Abstractions.Data;
using FastSeat.Modules.Events.Domain.Categories;

namespace FastSeat.Modules.Events.Application.Categories.CreateCategory;

internal sealed class CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<CreateCategoryCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = Category.Create(request.Name);

        categoryRepository.Insert(category);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return category.Id;
    }
}
