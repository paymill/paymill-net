using System.Collections.Generic;
using System.Threading.Tasks;
using PaymillWrapper.Net;

namespace PaymillWrapper.Service
{
    public interface IReadService<T>
    {
        T Get(string id);
        IReadOnlyCollection<T> List(Filter filter);
        IReadOnlyCollection<T> List();
    }

    public interface ICreateService<T>
    {
        T Create(string id, string encodeParams);
    }

    public interface IDeleteService
    {
        bool Remove(string id);
    }

    public interface IUpdateService<T>
    {
        T Update(T obj);
    }

    public interface ICRService<T> : ICreateService<T>, IReadService<T> { }

    public interface ICRDService<T> : ICRService<T>, IDeleteService { }

    public interface ICRUDService<T> : ICRDService<T>, IUpdateService<T> { }
}