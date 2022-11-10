﻿using Core.Entities;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> getByIdAsync(int id);
        Task<IReadOnlyList<T>> listAllAsync();
    }
}
