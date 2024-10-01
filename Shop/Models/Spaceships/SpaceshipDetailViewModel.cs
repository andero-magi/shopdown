using Shop.Core.Domain;

namespace Shop.Models.Spaceships
{
    public class SpaceshipDetailViewModel
    {
        public Spaceship Ship;
        public List<ImageViewModel> Images { get; set; }
    }
}
