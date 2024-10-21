using System;
using Shop.Core.Domain;

namespace Shop.Core.Dto;

public class KindergartenDto: DbFilesHolder
{
    public Guid Id { get; set; }
    public string GroupName { get; set; }
    public int ChildrenCount { get; set; }
    public string KindergartenName { get; set; }
    public string Teacher { get; set; }
    
    public DateTime CreationDate { get; set; }
    public DateTime LastUpdateDate { get; set; }

    public KindergartenDto()
    {

    }

    public KindergartenDto(Kindergarten k)
    {
        Id = k.Id;
        GroupName = k.GroupName;
        ChildrenCount = k.ChildrenCount;
        KindergartenName = k.KindergartenName;
        Teacher = k.Teacher;
        CreationDate = k.CreationDate;
        LastUpdateDate = k.LastUpdateDate;
    }

    public void TransferTo(Kindergarten target)
    {
        target.Id = Id;
        target.GroupName = GroupName;
        target.ChildrenCount = ChildrenCount;
        target.KindergartenName = KindergartenName;
        target.Teacher = Teacher;
        target.CreationDate = CreationDate;
        target.LastUpdateDate = LastUpdateDate;
    }
}
