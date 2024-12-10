using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows.Input;
using ZelekWieclaw.VisualProgrammingProject.BL;
using ZelekWieclaw.VisualProgrammingProject.DAOMock;
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
            NewCommand = new AsyncRelayCommand(NewAsync);
            PerformSearchCommand = new AsyncRelayCommand<string>(PerformSearchTask);
        }

        public ICommand SelectProductCommand { get; }
        public ICommand NewCommand { get; }

        public ICommand PerformSearchCommand { get; }

        private async Task SelectProductAsync(BeerProductViewModel product)
        {
            if (product == null)
            {
                return;
            }

            await Shell.Current.GoToAsync($"BeerProductPage?productId={product.Id}");
        }

        private async Task NewAsync()
        {
            await Shell.Current.GoToAsync("BeerProductPage");
        }

        private async Task PerformSearchTask(string query)
        {
            BeerProducts.Clear();
            var products = _catalogService.GetBeerProducts(_producer)
                .Where(p => p.Name.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0);
            foreach (var product in products)
            {
                BeerProducts.Add(new BeerProductViewModel(product));
            }
        }


        void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("producerId", out var producerId) && int.TryParse(producerId.ToString(), out int id))
            {
                _producer = _catalogService.GetProducerById(id);
                BeerProducts.Clear();
                foreach (var product in _catalogService.GetBeerProducts(_producer))
                {
                    BeerProducts.Add(new BeerProductViewModel(product));
                }
            }
            else if (query.TryGetValue("saved", out var _product_state))
            {
                string productJson = Uri.UnescapeDataString(_product_state.ToString());
                var product_state = JsonSerializer.Deserialize<BeerProduct>(productJson);
                var product = BeerProducts.FirstOrDefault(p => p.Id == product_state.Id);
                if (product != null)
                {
                    product.Reload();
                    _catalogService.UpdateBeerProduct(product_state);
                }
                else
                {
                    _catalogService.AddBeerProduct(product_state);
                    BeerProducts.Insert(0, new BeerProductViewModel(product_state));
                }
            }
            else if (query.TryGetValue("deleted", out var deletedId))
            {
                var product = BeerProducts.FirstOrDefault(p => p.Id == int.Parse((string)deletedId));
                if (product != null)
                {
                    _catalogService.DeleteBeerProducer(product.Id);
                    BeerProducts.Remove(product);
                }
            }
        }
    }
}