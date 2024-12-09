using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ZelekWieclaw.VisualProgrammingProject.BL;
using ZelekWieclaw.VisualProgrammingProject.Interfaces;

namespace ZelekWieclaw.VisualProgrammingProject.ViewModels
{
    public class BeerProductsViewModel : ObservableObject, IQueryAttributable
    {
        private IBeerProducer _producer;
        private CatalogService _catalogService;
        public ObservableCollection<BeerProductViewModel> BeerProducts { get; }


        public BeerProductsViewModel()
        {
            _catalogService = new CatalogService();
            BeerProducts = new ObservableCollection<BeerProductViewModel>();
            SelectProductCommand = new AsyncRelayCommand<BeerProductViewModel>(SelectProductAsync);
        }

        public ICommand SelectProductCommand { get; }

        private async Task SelectProductAsync(BeerProductViewModel product)
        {
            if (product == null)
            {
                return;
            }

            await Shell.Current.GoToAsync($"BeerProductPage?productId={product.Id}");
        }


        void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query) // Change to public
        {
            if (query.TryGetValue("producerId", out var producerId) && int.TryParse(producerId.ToString(), out int id))
            {
                _producer = _catalogService.GetProducerById(id);
                foreach (var product in _catalogService.GetBeerProducts(_producer))
                {
                    BeerProducts.Add(new BeerProductViewModel(product));
                }
            }
        }
    }
}