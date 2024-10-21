namespace Shop.RealEstateTest;

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

}