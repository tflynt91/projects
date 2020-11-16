namespace DvdLibrary.Migrations
{
    using DvdLibrary.Models.Queries;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DvdLibrary.Models.Repos.DvdRepositoryEF>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DvdLibrary.Models.Repos.DvdRepositoryEF context)
        {
            context.DvdItems.AddOrUpdate(
                m => m.DvdId,
                new DvdItem
                {
                    Title = "Star Wars",
                    Director = "George Lucas",
                    RatingType = "PG",
                    ReleaseYear = "1977"
                }
                );

            context.SaveChanges();

            
        }
    }
}
