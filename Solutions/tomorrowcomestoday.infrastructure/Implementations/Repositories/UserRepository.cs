namespace TomorrowComesToday.Infrastructure.Implementations.Repositories
{
    using SharpArch.NHibernate;

    using TomorrowComesToday.Domain.Entities;
    using TomorrowComesToday.Infrastructure.Interfaces.Repositories;

    /// <summary>
    /// The user repository.
    /// </summary>
    public class CardRepository : LinqRepository<Card>, ICardRepository
    {

    }
}
