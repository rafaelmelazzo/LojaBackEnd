namespace LojaApi.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LojaApi.Models.LojaApiContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(LojaApi.Models.LojaApiContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Produtos.AddOrUpdate(
                x => x.Id,
                new Models.Produto { Id = 1, Nome = "Tomato1 Soup", Categoria = "Groceries", Preco = 1 },
                new Models.Produto { Id = 2, Nome = "Yo-yo", Categoria = "Toys", Preco = 3.75M },
                new Models.Produto { Id = 3, Nome = "Hammer", Categoria = "Hardware", Preco = 16.99M }
            );
        }
    }
}
