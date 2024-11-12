using ZelekWieclaw.VisualProgrammingProject.Core;

namespace ZelekWieclaw.VisualProgrammingProject.Interfaces
{
    public interface IDataAccessObject
    {
        // 4.1.1 Retrieving data
        public IEnumerable<IBeerProducer> GetAllBeerProducers();
        public IEnumerable<IBeerProduct> GetAllBeerProducts();

        // 4.1.2 Adding Products/Producers
        public void AddBeerProducer(IBeerProducer producer);
        public void AddBeerProduct(IBeerProduct product);

        // 4.1.3 Modifying data
        public void UpdateBeerProducer(IBeerProducer producer);
        public void UpdateBeerProduct(IBeerProduct product);

        // 4.1.4 Deleting records
        public void DeleteBeerProducer(int producerId);
        public void DeleteBeerProduct(int productId);

        // 4.1.5 Searching/filtering data
        public IEnumerable<IBeerProducer> SearchBeerProducers(string searchTerm);
        public IEnumerable<IBeerProduct> SearchBeerProducts(string searchTerm);
        public IEnumerable<IBeerProduct> FilterBeerProductsByType(BeerType type);
        public IBeerProducer GetBeerProducerById(int id);
    }
}