namespace TomorrowComesToday.Infrastructure.NHibernateMaps
{
    #region Using Directives

    using System;

    using Conventions;

    using FluentNHibernate.Automapping;
    using FluentNHibernate.Conventions;

    using TomorrowComesToday.Domain;

    using SharpArch.Domain.DomainModel;
    using SharpArch.NHibernate.FluentNHibernate;

    using TomorrowComesToday.Domain.Entities;

    #endregion

    /// <summary>
    /// Generates the automapping for the domain assembly
    /// </summary>
    public class AutoPersistenceModelGenerator : IAutoPersistenceModelGenerator
    {
        /// <summary>
        /// Generate the auto mapping for domain objects
        /// </summary>
        /// <returns></returns>
        public AutoPersistenceModel Generate()
        {
            // I'm not sure if we'll need to make one of these for every domain object?
            var mappings = AutoMap.AssemblyOf<Card>(new AutomappingConfiguration());
            mappings.IgnoreBase<Entity>();
            mappings.IgnoreBase(typeof(EntityWithTypedId<>));
            mappings.Conventions.Setup(GetConventions());
            mappings.UseOverridesFromAssemblyOf<AutoPersistenceModelGenerator>();

            return mappings;
        }

        /// <summary>
        /// Conventions we're using
        /// </summary>
        /// <returns></returns>
        private static Action<IConventionFinder> GetConventions()
        {
            return c =>
                   {
                       c.Add<PrimaryKeyConvention>();
                       c.Add<CustomForeignKeyConvention>();
                       c.Add<HasManyConvention>();
                       c.Add<TableNameConvention>();
                   };
        }
    }
}