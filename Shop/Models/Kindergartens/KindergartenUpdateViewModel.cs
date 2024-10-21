using System;
using Shop.Core.Domain;
using Shop.Core.Dto;

namespace Shop.Models.Kindergartens;

public class KindergartenUpdateViewModel
{
  public KindergartenDto Dto { get; set; }
  public IEnumerable<FileToDb> Images { get; set; } = [];
}
