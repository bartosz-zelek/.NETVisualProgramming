using ZelekWieclaw.VisualProgrammingProject.Core;
using ZelekWieclaw.VisualProgrammingProject.Interfaces;

namespace ZelekWieclaw.VisualProgrammingProject.DAOMock
{
    public class DAOMock : IDataAccessObject
    {
        public IEnumerable<IBeerProducer> GetAllBeerProducers()
        {
            return new List<IBeerProducer>
            {
                new BeerProducer { Id = 1, Name = "Pinta", FoundationYear = "2011", Country = "Poland" },
                new BeerProducer { Id = 2, Name = "Artezan", FoundationYear = "2011", Country = "Poland" },
                new BeerProducer { Id = 3, Name = "BrewDog", FoundationYear = "2007", Country = "UK" },
                new BeerProducer { Id = 4, Name = "Heineken", FoundationYear = "1864", Country = "Netherlands" },
                new BeerProducer { Id = 5, Name = "Founders Brewing Co.", FoundationYear = "1997", Country = "USA" },
            };
        }

        public IEnumerable<IBeerProduct> GetAllBeerProducts()
        {
            return new List<IBeerProduct>
            {
                // Pinta Products
                new BeerProduct { Id = 1, ProducerId = 1, Name = "Atak Chmielu", Type = BeerType.Porter, Abv = 6.1f, Ibu = 58 },
                new BeerProduct { Id = 2, ProducerId = 1, Name = "Imperator Bałtycki", Type = BeerType.Malt, Abv = 9.1f, Ibu = 40 },
                new BeerProduct { Id = 3, ProducerId = 1, Name = "Hazy Disco", Type = BeerType.Ale, Abv = 6.5f, Ibu = 30 },

                // Artezan Products
                new BeerProduct { Id = 4, ProducerId = 2, Name = "Pacific Pale Ale", Type = BeerType.Pilsner, Abv = 5.2f, Ibu = 45 },
                new BeerProduct { Id = 5, ProducerId = 2, Name = "Mera Porter", Type = BeerType.Stout, Abv = 6.0f, Ibu = 50 },

                // BrewDog Products
                new BeerProduct { Id = 6, ProducerId = 3, Name = "Punk Porter", Type = BeerType.Porter, Abv = 5.6f, Ibu = 40 },
                new BeerProduct { Id = 7, ProducerId = 3, Name = "Elvis Juice", Type = BeerType.Porter, Abv = 6.5f, Ibu = 40 },
                new BeerProduct { Id = 8, ProducerId = 3, Name = "Hazy Jane", Type = BeerType.Stout, Abv = 5.0f, Ibu = 30 },

                // Heineken Products
                new BeerProduct { Id = 9, ProducerId = 4, Name = "Heineken Lager", Type = BeerType.Lager, Abv = 5.0f, Ibu = 19 },
                new BeerProduct { Id = 10, ProducerId = 4, Name = "Heineken 0.0", Type = BeerType.Malt, Abv = 0.0f, Ibu = 0 },

                // Founders Brewing Co. Products
                new BeerProduct { Id = 11, ProducerId = 5, Name = "All Day Porter", Type = BeerType.Stout, Abv = 4.7f, Ibu = 42 },
                new BeerProduct { Id = 12, ProducerId = 5, Name = "KBS (Kentucky Breakfast Stout)", Type = BeerType.Stout, Abv = 12.0f, Ibu = 70 },
            };
        }
    }
}
