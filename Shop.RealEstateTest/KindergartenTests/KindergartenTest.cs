namespace Shop.RealEstateTest.KindergartenTests;

using Shop.Core.Dto;
using Shop.Core.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class KindergartenTest: TestBase
{
    [Fact]
    public async Task Should_CreateAsync_WhenEmpty()
    {
        KindergartenDto dto = new();

        var result = await Svc<IKindergartenService>().CreateAsync(dto);
        Assert.NotNull(result);
    }

    [Fact]
    public async Task Should_CreateAsync_WhenRegular()
    {
        var dto = CreateMock();
        var s = Svc<IKindergartenService>();

        var result = await s.CreateAsync(dto);

        Assert.NotNull(result);
    }

    [Fact]
    public async Task ShouldNot_GetById_WhenDifferentId()
    {
        var dto = CreateMock();
        var s = Svc<IKindergartenService>();

        var k1 = await s.CreateAsync(dto);
        var k2 = await s.CreateAsync(dto);

        var found1 = await s.GetKindergartenAsync(k1.Id);
        var found2 = await s.GetKindergartenAsync(k2.Id);

        Assert.Equal(k1, found1);
        Assert.Equal(k2, found2);
    }

    [Fact]
    public async Task Should_ReturnNull_WhenNullId()
    {
        var s = Svc<IKindergartenService>();
        var dto = CreateMock();

        await s.CreateAsync(dto);

        var found = await s.GetKindergartenAsync(null);

        Assert.Null(found);
    }

    [Fact]
    public async Task Should_DeleteKindergarten_WhenValidId()
    {
        var dto = CreateMock();
        var s = Svc<IKindergartenService>();

        var created = await s.CreateAsync(dto);
        var id = created.Id;

        await s.DeleteAsync(id);
        var found = await s.GetKindergartenAsync(id);

        Assert.Null(found);
    }

    [Fact]
    public async Task ShouldNot_DeleteKindergarten_WhenNillGuid()
    {
        var dto = CreateMock();
        var s = Svc<IKindergartenService>();
        var nilGuid = Guid.Parse("00000000-0000-0000-0000-000000000000");

        var created = await s.CreateAsync(dto);
        var deleted = await s.DeleteAsync(nilGuid);

        Assert.Null(deleted);
    }

    private static KindergartenDto CreateMock()
    {
        return new()
        {
            GroupName = "Test",
            ChildrenCount = 10,
            KindergartenName = "A kindergarten",
            Teacher = "Beidou, Captain of the Crux fleet"
        };
    }
}
