namespace Shop.RealEstateTest;

using Shop.Core.Dto;
using Shop.Core.ServiceInterface;

public class UnitTest1: TestBase
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


}