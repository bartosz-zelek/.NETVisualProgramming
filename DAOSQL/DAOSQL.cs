using Microsoft.EntityFrameworkCore;
using ZelekWieclaw.VisualProgrammingProject.Interfaces;

namespace ZelekWieclaw.VisualProgrammingProject.DAOSQL
{
    public class DAOSQL : DbContext, IDataAccessObject
    {
        public string DbPath { get; }

        private DAOSQL()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "rakrobiswoje.db");
        }

        private static DAOSQL _instance;

        public static DAOSQL Instance => _instance ??= new DAOSQL();

        public DbSet<BeerProducer> BeerProducers { get; set; }

        public DbSet<BeerProduct> BeerProducts { get; set; }

        // 4.1.1 Retrieving data
        public IEnumerable<IBeerProducer> GetAllBeerProducers() => BeerProducers.ToList();

        public IEnumerable<IBeerProduct> GetAllBeerProducts() => BeerProducts.ToList();

        // 4.1.2 Adding Products/Producers
        public void AddBeerProducer(IBeerProducer producer)
        {
            var beerProducer = new BeerProducer
            {
                Id = producer.Id,
                Name = producer.Name,
                FoundationYear = producer.FoundationYear,
                Country = producer.Country
            };

            BeerProducers.Add(beerProducer);
            SaveChanges();
        }


        public void AddBeerProduct(IBeerProduct product)
        {
            var beerProduct = new BeerProduct
            {
                Id = product.Id,
                ProducerId = product.ProducerId,
                Name = product.Name,
                Type = product.Type,
                Abv = product.Abv,
                Ibu = product.Ibu
            };

            BeerProducts.Add(beerProduct);
            SaveChanges();
        }

        // 4.1.3 Modifying data
        public void UpdateBeerProducer(IBeerProducer producer)
        {
            var existingProducer = BeerProducers.Find(producer.Id);
            if (existingProducer != null)
            {
                existingProducer.Name = producer.Name;
                existingProducer.FoundationYear = producer.FoundationYear;
                existingProducer.Country = producer.Country;
                SaveChanges();
            }
        }

        public void UpdateBeerProduct(IBeerProduct product)
        {
            var existingProduct = BeerProducts.Find(product.Id);
            if (existingProduct != null)
            {
                existingProduct.ProducerId = product.ProducerId;
                existingProduct.Name = product.Name;
                existingProduct.Type = product.Type;
                existingProduct.Abv = product.Abv;
                existingProduct.Ibu = product.Ibu;
                SaveChanges();
            }
        }

        // 4.1.4 Deleting records
        public void DeleteBeerProducer(int producerId)
        {
            var producer = BeerProducers.Find(producerId);
            if (producer != null)
            {
                BeerProducers.Remove(producer);
                SaveChanges();
            }
        }

        public void DeleteBeerProduct(int productId)
        {
            var product = BeerProducts.Find(productId);
            if (product != null)
            {
                BeerProducts.Remove(product);
                SaveChanges();
            }
        }

        // 4.1.5 Searching/filtering data
        public IBeerProducer GetBeerProducerById(int id) => BeerProducers.Find(id);

        public IBeerProduct GetBeerProductById(int id) => BeerProducts.Find(id);

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite($"Data Source={DbPath}");
            }
        }
    }
}