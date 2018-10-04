﻿namespace Hive.Domain.Services.Factories
{
    public abstract class BaseConversionFactory<T, T1> : IConversionFactory<T, T1>
    {
        public abstract T Create<T11>(T11 obj);
        public abstract T Create();
        public abstract T Create(object obj);
    }
}