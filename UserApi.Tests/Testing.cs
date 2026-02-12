using System;

namespace UserApi.Tests;

public class Testing
{
    class FakeRepo : IAccessKeyRepository
{
    private readonly IEnumerable<AccessKey> _keys;

    public FakeRepo(IEnumerable<AccessKey> keys)
    {
        _keys = keys;
    }

    public Task<IEnumerable<AccessKey>> GetKeysForUser(Guid userId)
    {
        return Task.FromResult(_keys.Where(k => k.UserId == userId));
    }
}

}
