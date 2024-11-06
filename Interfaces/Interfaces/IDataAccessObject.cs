namespace ZelekWieclaw.VisualProgrammingProject.Interfaces
{
    public interface IDataAccessObject
    {
        // public void AddBeerProduct(IBeerProduct beerProduct);
        // public void AddBeerProducer(IBeerProducer beerProducer);
        // public void RemoveBeerProduct(int id);
        // public void RemoveBeerProducer(int id);
        // public void UpdateBeerProduct(IBeerProduct beerProduct);
        // public void UpdateBeerProducer(IBeerProducer beerProducer);
        // public IBeerProduct GetBeerProduct(int id);
        // public IBeerProducer GetBeerProducer(int id);

        public IEnumerable<IBeerProducer> GetAllBeerProducers();
        public IEnumerable<IBeerProduct> GetAllBeerProducts();
    }
}
