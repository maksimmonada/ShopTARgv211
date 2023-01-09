﻿using Microsoft.EntityFrameworkCore;
using ShopTARgv21.Core.Domain;
using ShopTARgv21.Core.Dto;
using ShopTARgv21.Core.ServiceInterface;
using ShopTARgv21.Data;


namespace ShopTARgv21.ApplicationServices.Services
{
    public class SpaceShipServices : ISpaceshipServices
    {
        private readonly ShopDbContext _context;

        public SpaceShipServices
            (
                ShopDbContext context
            )
        {
            _context = context;
        }


        public async Task<Spaceship> Create(SpaceshipDto dto)
        {
            Spaceship spaceship = new Spaceship();
            FileToDatabase file = new FileToDatabase();

            spaceship.Id = Guid.NewGuid();
            spaceship.Name = dto.Name;
            spaceship.ModelType = dto.ModelType;
            spaceship.SpaceshipBuider = dto.SpaceshipBuider;
            spaceship.PlaceOfBuild = dto.PlaceOfBuild;
            spaceship.EnginePower = dto.EnginePower;
            spaceship.LiftUpToSpaceByTonn = dto.LiftUpToSpaceByTonn;
            spaceship.Crew = dto.Crew;
            spaceship.Passengers = dto.Passengers;
            spaceship.LaunchDate = dto.LaunchDate;
            spaceship.BuildOfDate = dto.BuildOfDate;
            spaceship.CreatedAt = dto.CreatedAt;
            spaceship.ModifiedAt = dto.ModifiedAt;

            if(dto.Files != null)
            {
                file.ImageData = UploadFile(dto, spaceship);
            }
            await _context.Spaceship.AddAsync(spaceship);
            await _context.SaveChangesAsync();

            return spaceship;

        }

        public async Task<Spaceship>GetAsync(Guid id)
        {
            var result = await _context.Spaceship
                .FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }

        public async Task<Spaceship> Update(SpaceshipDto dto)
        {
           
            var spaceship = new Spaceship()
            {
                Id = dto.Id,
                Name = dto.Name,
                ModelType = dto.ModelType,
                SpaceshipBuider = dto.SpaceshipBuider,
                PlaceOfBuild = dto.PlaceOfBuild,
                EnginePower = dto.EnginePower,
                LiftUpToSpaceByTonn = dto.LiftUpToSpaceByTonn,
                Crew = dto.Crew,
                Passengers = dto.Passengers,
                LaunchDate = dto.LaunchDate,
                BuildOfDate = dto.BuildOfDate,
                CreatedAt = dto.CreatedAt,
                ModifiedAt = dto.ModifiedAt
            };

            _context.Spaceship.Update(spaceship);
            await _context.SaveChangesAsync();
            return spaceship;

        }

        public async Task<Spaceship> Delete(Guid id)
        {
            var  spaceship = await _context.Spaceship
                .FirstOrDefaultAsync(x =>x.Id == id);

            _context.Spaceship.Remove(spaceship);
            await _context.SaveChangesAsync();

            return spaceship;

        }

        public byte[] UploadFile(SpaceshipDto dto, Spaceship domain)
        {
            if (dto.Files !=null && dto.Files.Count > 0 )
            {
                foreach(var photo in dto.Files)
                {
                    using (var target = new MemoryStream())
                    {
                        FileToDatabase files = new FileToDatabase
                        {
                            Id = Guid.NewGuid(),
                            ImageTitle = photo.FileName,
                            SpaceshipId = domain.Id,

                        };

                        photo.CopyTo(target);
                        files.ImageData = target.ToArray();

                        _context.FileToDatabase.Add(files);
                    }
                }
            }
            return null;
        }
    }
}
