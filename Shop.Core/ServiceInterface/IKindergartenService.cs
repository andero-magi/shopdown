using System;
using Shop.Core.Domain;
using Shop.Core.Dto;

namespace Shop.Core.ServiceInterface;

public interface IKindergartenService
{
    Task<Kindergarten?> GetKindergartenAsync(Guid? guid);

    Task<Kindergarten> UpdateAsync(KindergartenDto dto);

    Task<Kindergarten> CreateAsync(KindergartenDto dto);

    Task<Kindergarten?> DeleteAsync(Guid id);
}
