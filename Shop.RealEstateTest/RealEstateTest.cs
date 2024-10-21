namespace Shop.RealEstateTest;

using Shop.Core.Domain;
using Shop.Core.Dto;
using Shop.Core.ServiceInterface;

public class RealEstateTest: TestBase
{
    [Fact]
    public async Task ShouldNot_AddEmptyRealEstate_WhenReturnResult()
    {

        RealEstateDto dto = new();

        dto.Size = 100;
        dto.RoomNumber = 1;
        dto.BuildingType = "Apartment";
        dto.CreationTime = DateTime.Now;
        dto.ModifiedTime = DateTime.Now;

        var result = await Svc<IRealEstateService>().Create(dto);

        Assert.NotNull(result);
    }

    [Fact]
    public async Task ShouldNot_GetByIdRealEstate_WhenReturnsNotEqual()
    {
        RealEstateDto dto1 = new()
        {
            Size = 1000,
            RoomNumber = 10,
            BuildingType = "Manor",
            CreationTime = DateTime.Now,
            ModifiedTime = DateTime.Now,
        };

        var serv = Svc<IRealEstateService>();
        Guid id1 = (await serv.Create(dto1)).Id;
        Guid g = Guid.NewGuid();

        var a = await serv.GetAsync(g);
        var b = await serv.GetAsync(id1);

        Assert.Null(a);
        Assert.NotNull(b);
    }

    [Fact]
    public async Task Should_GetByIdRealEstate_WhenReturnsNotEqual()
    {

    }

    [Fact]
    public async Task Should_DeleteByIdRealEstate_WhenDeleteRealEstate()
    {
        RealEstateDto dto = MockRealEstateData();
        var svr = Svc<IRealEstateService>();

        RealEstate estate = await svr.Create(dto);
        Guid id = estate.Id;

        var removed = await svr.Delete(id);

        var found = await svr.GetAsync(id);

        Assert.Null(found);
        Assert.NotNull(removed);
    }

    [Fact]
    public async Task ShouldNot_DeleteByIdRealEstate_WhenDidNotDeleteRealEstate()
    {
        RealEstateDto dto = MockRealEstateData();

        var s = Svc<IRealEstateService>();

        var r1 = await s.Create(dto);
        var r2 = await s.Create(dto);

        var result = await s.Delete(r2.Id);

        Assert.NotEqual(result.Id, r1.Id);
        Assert.Equal(result.Id, r2.Id);
    }

    private RealEstateDto MockRealEstateData()
    {
        return new()
        {
            Size = 1000,
            RoomNumber = 10,
            BuildingType = "Apartment",
            CreationTime = DateTime.Now,
            ModifiedTime = DateTime.Now,
        };
    }
}