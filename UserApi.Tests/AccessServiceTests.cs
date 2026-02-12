using System;
using UserApi;
using Xunit;

namespace UserApi.Tests;

public class AccessServiceTests
{
   [Fact]
    public async Task CanUnlock_Should_Return_True_When_Key_Active()
    {
        // fake data
        var userId = Guid.NewGuid();
        var locatinId = Guid.NewGuid();

        var fakeRepo = new FakeRepo(new[]
        {
            new AccessKey
            {
                UserId = userId,
                LocationId = locatinId,
                IsActive = true
            }
        });

        var service = new AccessService(fakeRepo);

        var result = await service.CanUnlock(userId, locatinId);

        Assert.True(result);
    }
    
}
