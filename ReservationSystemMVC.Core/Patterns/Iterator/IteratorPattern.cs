using ReservationSystemMVC.Core.Domain.Abstractions;
using System.Collections.Generic;

namespace ReservationSystemMVC.Core.Patterns.Iterator
{
    public interface IIterator<T>
    {
        bool HasNext();
        T Next();
    }

    public interface IAggregate<T>
    {
        IIterator<T> CreateIterator();
    }

    public class ResourceIterator : IIterator<IBookableResource>
    {
        private readonly List<IBookableResource> _resources;
        private int _position = 0;

        public ResourceIterator(List<IBookableResource> resources)
        {
            _resources = resources;
        }

        public bool HasNext()
        {
            return _position < _resources.Count;
        }

        public IBookableResource Next()
        {
            return _resources[_position++];
        }
    }

    public class ResourceCollection : IAggregate<IBookableResource>
    {
        private readonly List<IBookableResource> _resources = new List<IBookableResource>();

        public void AddData(IBookableResource resource)
        {
            _resources.Add(resource);
        }

        public IIterator<IBookableResource> CreateIterator()
        {
            return new ResourceIterator(_resources);
        }
    }
}
