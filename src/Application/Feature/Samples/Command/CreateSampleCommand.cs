using Application.Interface.DataAccess;
using Domain.Entity;
using MediatR;

namespace Application.Feature.Samples.Command;

public record CreateSampleCommand(string Name, string Description) : IRequest<string>;

public class CreateSampleCommandHandler(
    IWriteUnitOfWork unitOfWork
) : IRequestHandler<CreateSampleCommand, string>
{
    public async Task<string> Handle(CreateSampleCommand request, CancellationToken cancellationToken)
    {
        var repo = unitOfWork.GetRepository<Sample>();

        repo.Insert(new Sample {
            Name = "Sample",
            Description = "Sample Description"
        });

        await repo.ExecuteAsync();

        return string.Empty;
    }
}