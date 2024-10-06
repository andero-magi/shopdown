using Shop.Core.Dto;

namespace Shop.Models.Spaceships
{
    public class SpaceshipCreateUpdateViewModel
    {
        public SpaceshipDto Dto { get; set; }
        public List<ImageViewModel> Images { get; set; }
    }
}
