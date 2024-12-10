using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Text.Json;
using System.Windows.Input;
using ZelekWieclaw.VisualProgrammingProject.BL;
using ZelekWieclaw.VisualProgrammingProject.Core;
using ZelekWieclaw.VisualProgrammingProject.DAOMock;
using ZelekWieclaw.VisualProgrammingProject.Interfaces;

namespace ZelekWieclaw.VisualProgrammingProject.ViewModels
{
    public class BeerProductViewModel : ObservableObject, IQueryAttributable
    {
        private IBeerProduct _product;
        private CatalogService _catalogService;
        public IList<BeerType> BeerTypes { get; } = Enum.GetValues(typeof(BeerType)).Cast<BeerType>().ToList();
        public IList<String> BeerTypeNames { get; } = Enum.GetNames(typeof(BeerType)).ToList();
        public ICommand SaveCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }

        public BeerProductViewModel()
        {
            _product = new BeerProduct();
            _catalogService = new CatalogService();
            SaveCommand = new AsyncRelayCommand(Save);
            DeleteCommand = new AsyncRelayCommand(Delete);
        }

        public BeerProductViewModel(IBeerProduct product)
        {
            _product = product;
            _catalogService = new CatalogService();
            SaveCommand = new AsyncRelayCommand(Save);
            DeleteCommand = new AsyncRelayCommand(Delete);
        }

        private async Task Save()
        {
            string productJson = JsonSerializer.Serialize(_product);
            string encodedProduct = Uri.EscapeDataString(productJson);
            await Shell.Current.GoToAsync($"..?saved={encodedProduct}");
        }

        private async Task Delete()
        {
            _catalogService.DeleteBeerProduct(_product.Id);
            await Shell.Current.GoToAsync($"..?deleted={_product.Id}");
        }

        private void RefreshProperties()
        {
            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(Type));
            OnPropertyChanged(nameof(Abv));
            OnPropertyChanged(nameof(Ibu));
        }

        public void Reload()
        {
            _product = _catalogService.GetBeerProductById(Id);
            RefreshProperties();
        }

        void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("productId", out var productId) && int.TryParse(productId.ToString(), out int id))
            {
                _product = _catalogService.GetBeerProductById(id);
                RefreshProperties();
            }
        }

        public int Id
        {
            get => _product.Id;
        }

        public string Name
        {
            get => _product.Name;
            set
            {
                _product.Name = value;
                OnPropertyChanged();
            }
        }

        public BeerType Type
        {
            get => _product.Type;
            set
            {
                _product.Type = value;
                OnPropertyChanged();
            }
        }

        public float Abv
        {
            get => _product.Abv;
            set
            {
                _product.Abv = value;
                OnPropertyChanged();
            }
        }

        public int Ibu
        {
            get => _product.Ibu;
            set
            {
                _product.Ibu = value;
                OnPropertyChanged();
            }
        }

        public int ProducerId
        {
            get => _product.ProducerId;
        }
    }
}