using System;

namespace Shop.Core.Domain;

public class Kindergarten
{
    public Guid Id { get; set; }
    public string GroupName { get; set; }
    public int ChildrenCount { get; set; }
    public string KindergartenName { get; set; }
    public string Teacher { get; set; }
    
    public DateTime CreationDate { get; set; }
    public DateTime LastUpdateDate { get; set; }
}
