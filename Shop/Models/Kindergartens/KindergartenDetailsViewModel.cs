using System;
using Shop.Core.Domain;

namespace Shop.Models.Kindergartens;

public class KindergartenDetailsViewModel
{
  public Kindergarten Kindergarten { get; set; }
  public IEnumerable<FileToDb> Images { get; set; } = [];
}