﻿using Shop.Core.Domain;
using Shop.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Core.ServiceInterface
{
    public interface IFileService
    {
        void FilesToApi(SpaceshipDto dto, Spaceship spaceship);

        Task<List<FileToApi>?> RemoveImagesFromApi(FileToApiDto[] dtos);
    }
}
