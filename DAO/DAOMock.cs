using ZelekWieclaw.VisualProgrammingProject.Core;
using ZelekWieclaw.VisualProgrammingProject.DAOMock;
using ZelekWieclaw.VisualProgrammingProject.Interfaces;


public class DAOMock : IDataAccessObject
{
    private DAOMock()
    {
    }

    private static DAOMock _instance;

    public static DAOMock Instance => _instance ??= new DAOMock();


    // 4.1.1 Retrieving data
    public IEnumerable<IBeerProducer> GetAllBeerProducers() => _beerProducers;
    public IEnumerable<IBeerProduct> GetAllBeerProducts() => _beerProducts;

    // 4.1.2 Adding Products/Producers
    public void AddBeerProducer(IBeerProducer producer) => _beerProducers.Add(producer);
    public void AddBeerProduct(IBeerProduct product) => _beerProducts.Add(product);

    // 4.1.3 Modifying data
    public void UpdateBeerProducer(IBeerProducer producer)
    {
        var existingProducer = _beerProducers.FirstOrDefault(p => p.Id == producer.Id);

        if (existingProducer == null)
        {
            return;
        }

        existingProducer.Name = producer.Name;
        existingProducer.FoundationYear = producer.FoundationYear;
        existingProducer.Country = producer.Country;
    }

    public void UpdateBeerProduct(IBeerProduct product)
    {
        var existingProduct = _beerProducts.FirstOrDefault(p => p.Id == product.Id);
        if (existingProduct == null)
        {
            return;
        }

        existingProduct.ProducerId = product.ProducerId;
        existingProduct.Name = product.Name;
        existingProduct.Type = product.Type;
        existingProduct.Abv = product.Abv;
        existingProduct.Ibu = product.Ibu;
    }

    // 4.1.4 Deleting records
    public void DeleteBeerProducer(int producerId) => _beerProducers.RemoveAll(p => p.Id == producerId);
    public void DeleteBeerProduct(int productId) => _beerProducts.RemoveAll(p => p.Id == productId);

    // 4.1.5 Searching/filtering data
    public IEnumerable<IBeerProducer> SearchBeerProducers(string searchTerm) =>
        _beerProducers.Where(p => p.Name.Contains(searchTerm));

    public IEnumerable<IBeerProduct> SearchBeerProducts(string searchTerm) =>
        _beerProducts.Where(p => p.Name.Contains(searchTerm));

    public IEnumerable<IBeerProduct> FilterBeerProductsByType(BeerType type) =>
        _beerProducts.Where(p => p.Type == type);

    public IBeerProducer GetBeerProducerById(int id) => _beerProducers.FirstOrDefault(p => p.Id == id);


    private List<IBeerProducer> _beerProducers = new List<IBeerProducer>
    {
        new BeerProducer { Id = 1, Name = "Pinta", FoundationYear = "2011", Country = "Poland" },
        new BeerProducer { Id = 2, Name = "Artezan", FoundationYear = "2011", Country = "Poland" },
        new BeerProducer { Id = 3, Name = "BrewDog", FoundationYear = "2007", Country = "UK" },
        new BeerProducer { Id = 4, Name = "Heineken", FoundationYear = "1864", Country = "Netherlands" },
        new BeerProducer { Id = 5, Name = "Founders Brewing Co.", FoundationYear = "1997", Country = "USA" },
    };

    private List<IBeerProduct> _beerProducts = new List<IBeerProduct>
    {
        new BeerProduct { Id = 1, ProducerId = 1, Name = "Atak Chmielu", Type = BeerType.Porter, Abv = 6.1f, Ibu = 58 },
        new BeerProduct
            { Id = 2, ProducerId = 1, Name = "Imperator Bałtycki", Type = BeerType.Malt, Abv = 9.1f, Ibu = 40 },
        new BeerProduct { Id = 3, ProducerId = 1, Name = "Hazy Disco", Type = BeerType.Ale, Abv = 6.5f, Ibu = 30 },
        new BeerProduct
            { Id = 4, ProducerId = 2, Name = "Pacific Pale Ale", Type = BeerType.Pilsner, Abv = 5.2f, Ibu = 45 },
        new BeerProduct { Id = 5, ProducerId = 2, Name = "Mera Porter", Type = BeerType.Stout, Abv = 6.0f, Ibu = 50 },
        new BeerProduct { Id = 6, ProducerId = 3, Name = "Punk Porter", Type = BeerType.Porter, Abv = 5.6f, Ibu = 40 },
        new BeerProduct { Id = 7, ProducerId = 3, Name = "Elvis Juice", Type = BeerType.Porter, Abv = 6.5f, Ibu = 40 },
        new BeerProduct { Id = 8, ProducerId = 3, Name = "Hazy Jane", Type = BeerType.Stout, Abv = 5.0f, Ibu = 30 },
        new BeerProduct
            { Id = 9, ProducerId = 4, Name = "Heineken Lager", Type = BeerType.Lager, Abv = 5.0f, Ibu = 19 },
        new BeerProduct { Id = 10, ProducerId = 4, Name = "Heineken 0.0", Type = BeerType.Malt, Abv = 0.0f, Ibu = 0 },
        new BeerProduct
            { Id = 11, ProducerId = 5, Name = "All Day Porter", Type = BeerType.Stout, Abv = 4.7f, Ibu = 42 },
        new BeerProduct
        {
            Id = 12, ProducerId = 5, Name = "Kentucky Breakfast Stout", Type = BeerType.Stout, Abv = 12.0f, Ibu = 70
        },
    };
}