﻿using System.Linq;

namespace BookingHelper
{
    public interface IUnitOfWork
    {
        IQueryable<T> Query<T>();

    }
}