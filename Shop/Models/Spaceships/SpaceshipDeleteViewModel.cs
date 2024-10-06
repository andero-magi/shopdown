using Shop.Core.Domain;

namespace Shop.Models.Spaceships
{
    public class SpaceshipDeleteViewModel
    {
        public Spaceship Ship { get; set; }
        public List<ImageViewModel> Images { get; set; }
    }
}
