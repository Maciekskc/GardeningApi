﻿using Gardening.Core.Entities;
using LanguageExt;

namespace Gardening.Core.Interfaces
{
    public interface IPlantSpecieRepository
    {
        Task<IEnumerable<PlantSpecie>> GetAllPlantSpeciesAsync();
        Task<Option<PlantSpecie>> GetPlantSpecieByIdAsync(int id);
        Task<Option<PlantSpecie>> GetPlantSpecieByNameAsync(string name);
        Task<Option<PlantSpecie>> CreatePlantSpecieAsync(PlantSpecie plantSpecie);
        Task<Option<PlantSpecie>> UpdatePlantSpecieAsync(PlantSpecie plantSpecie);
        Task<Option<int>> DeletePlantSpecieAsync(PlantSpecie plantSpacie);
    }
}
