using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIVE.Domain.Entities;

namespace Hive.Domain.Services.Factories
{
    public abstract class BaseFactory<T> : IFactory<T>
    {
        public abstract T Create();

        public abstract T Create(object obj);
    }
}
