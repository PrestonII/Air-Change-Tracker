using System;

namespace HIVE.Domain.Entities
{
    public class Element : IElement
    {
        public string Id { get; set; }

        public Element()
        {
            Id = new Guid().ToString();
        }
    }
}