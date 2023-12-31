﻿namespace WebTechnologies.Application.Interfaces;
public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
