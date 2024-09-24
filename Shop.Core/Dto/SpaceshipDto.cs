using Shop.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Core.Dto
{
    public class SpaceshipDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Typename { get; set; }

        public string SpaceshipModel { get; set; }

        public DateTime BuildDate { get; set; }

        public int Crew { get; set; }

        public int EnginePower { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }


        public SpaceshipDto() {}

        public SpaceshipDto(Spaceship ship)
        {
            Id = ship.Id;
            Name = ship.Name;
            Typename = ship.Typename;
            SpaceshipModel = ship.SpaceshipModel;
            BuildDate = ship.BuildDate;
            Crew = ship.Crew;
            EnginePower = ship.EnginePower;
            CreatedAt = ship.CreatedAt;
            LastUpdatedAt = ship.LastUpdatedAt;
        }
    }
}
