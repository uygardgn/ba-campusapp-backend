﻿namespace BACampusApp.Core.DataAccess.Interfaces;

public interface IDeleteableRepository<TEntity> : IRepository
{
    bool Delete(TEntity entity);
}
