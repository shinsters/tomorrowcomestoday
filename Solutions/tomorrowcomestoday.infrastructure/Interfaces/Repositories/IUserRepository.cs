namespace TomorrowComesToday.Infrastructure.Interfaces.Repositories
{

    using SharpArch.Domain.PersistenceSupport;

    using TomorrowComesToday.Domain;

    /// <summary>
    /// The UserRepository.
    /// </summary>
    public interface IUserRepository : IRepository<User>
    {


        ///// <summary>
        ///// Get user from identifier
        ///// </summary>
        ///// <param name="userFormId">Form Id from h</param>
        ///// <returns></returns>
        //User GetFromFormId(string userFormId);
    }
}
