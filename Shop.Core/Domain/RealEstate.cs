namespace Shop.Core.Domain;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class RealEstate
{
    public Guid Id { get; set; }
    public double Size { get; set; }
    public int RoomNumber { get; set; }
    public string BuildingType { get; set; }

    public DateTime CreationTime { get; set; }
    public DateTime ModifiedTime { get; set; }
}
