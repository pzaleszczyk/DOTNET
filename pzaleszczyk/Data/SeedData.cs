using pzaleszczyk.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace pzaleszczyk.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ApplicationDbContext>>()))
            {
                // Look for any movies.
                if (context.Manga.Any())
                {
                    return;   // DB has been seeded
                }

                context.Manga.AddRange(
                    new Manga
                    {
                        Title = "When Harry Met Sally",
                        ReleaseDate = DateTime.Parse("1989-2-12"),
                        Genre = "Romantic Comedy",
                        Rating = 6.55M,
                        AuthorId = 2,
                        Price = 7.99M
                    },

                    new Manga
                    {
                        Title = "Ghostbusters ",
                        ReleaseDate = DateTime.Parse("1984-3-13"),
                        Genre = "Comedy",
                        Rating = 8.55M,
                        AuthorId = 2,
                        Price = 8.99M
                    },

                    new Manga
                    {
                        Title = "Ghostbusters 2",
                        ReleaseDate = DateTime.Parse("1986-2-23"),
                        Genre = "Comedy",
                        Rating = 9.55M,
                        AuthorId = 2,
                        Price = 9.99M
                    },

                    new Manga
                    {
                        Title = "Rio Bravo",
                        ReleaseDate = DateTime.Parse("1959-4-15"),
                        Genre = "Western",
                        Rating = 7.55M,
                        AuthorId = 2,
                        Price = 3.99M
                    }
                );
                context.SaveChanges();
            }
        }
    }
}