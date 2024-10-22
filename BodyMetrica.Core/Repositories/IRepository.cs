using BodyMetrica.Domain.Common.Models;

namespace BodyMetrica.Domain.Common.Repositories;

public interface IRepository<T> where T : IAggregateRoot
{

}