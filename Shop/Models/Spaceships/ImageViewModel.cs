using System;

namespace Shop.Models.Spaceships;

public class ImageViewModel
{
        public Guid ImageId { get; set; }
        public string FilePath { get; set; }
        public Guid SpaceshipId { get; set; }
}