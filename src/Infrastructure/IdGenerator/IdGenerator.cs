using Application.Interface.Identity;
using Domain.Constants;
using NanoidDotNet;

namespace Infrastructure.IdGenerator;

public class IdGenerator : IIdGenerator
{
    public string GenerateSessionId()
    {
        return Nanoid.Generate(size: GlobalConstants.Session.IdLength);
    }
}
