﻿using ShopTARgv21.Core.Domain;
using ShopTARgv21.Core.Dto;


namespace ShopTARgv21.Core.ServiceInterface
{
    public interface ISpaceshipServices : IApplicationServices
    {
        Task<Spaceship> Add(SpaceshipDto dto);

        Task<Spaceship> GetAsync(Guid id);

        Task<Spaceship> Update(SpaceshipDto dto);
    }
}
