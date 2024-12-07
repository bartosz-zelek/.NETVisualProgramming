using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Text.Json;
using System.Windows.Input;
using ZelekWieclaw.VisualProgrammingProject.BL;
using ZelekWieclaw.VisualProgrammingProject.DAOMock;
using ZelekWieclaw.VisualProgrammingProject.Interfaces;

namespace ZelekWieclaw.VisualProgrammingProject.ViewModels
{
    public class BeerProducerViewModel : ObservableObject, IQueryAttributable
    {
        private IBeerProducer _producer;
        private readonly CatalogService _catalogService;

        public BeerProducerViewModel()
        {
            _producer = new BeerProducer();
            _catalogService = new CatalogService();
            SaveCommand = new AsyncRelayCommand(Save);
            DeleteCommand = new AsyncRelayCommand(Delete);
            NavigateToBeersCommand = new AsyncRelayCommand(NavigateToBeers);
        }

        public BeerProducerViewModel(IBeerProducer producer)
        {
            _producer = producer;
            _catalogService = new CatalogService();
            SaveCommand = new AsyncRelayCommand(Save);
            DeleteCommand = new AsyncRelayCommand(Delete);
        }


        public ICommand SaveCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand NavigateToBeersCommand { get; private set; }

        private async Task Save()
        {
            string producerJson = JsonSerializer.Serialize(_producer);
            string encodedProducer = Uri.EscapeDataString(producerJson);
            await Shell.Current.GoToAsync($"..?saved={encodedProducer}");
        }

        private async Task Delete()
        {
            _catalogService.DeleteBeerProducer(_producer.Id);
            await Shell.Current.GoToAsync($"..?deleted={_producer.Id}");
        }

        private async Task NavigateToBeers()
        {
            await Shell.Current.GoToAsync($"..?producerId={_producer.Id}");
            // await Shell.Current.GoToAsync($"beers?producerId={_producer.Id}");
        }

        private void RefreshProperties()
        {
            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(FoundationYear));
            OnPropertyChanged(nameof(Country));
        }

        public void Reload()
        {
            _producer = _catalogService.GetProducerById(_producer.Id);
            RefreshProperties();
        }


        void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("load", out object? value) && int.TryParse(value.ToString(), out int id))
            {
                _producer = _catalogService.GetProducerById(id);
                RefreshProperties();
            }
        }

        public int Id
        {
            get => _producer.Id;
        }

        public string Name
        {
            get => _producer.Name;
            set
            {
                _producer.Name = value;
                OnPropertyChanged();
            }
        }

        public string FoundationYear
        {
            get => _producer.FoundationYear;
            set
            {
                _producer.FoundationYear = value;
                OnPropertyChanged();
            }
        }

        public string Country
        {
            get => _producer.Country;
            set
            {
                _producer.Country = value;
                OnPropertyChanged();
            }
        }
    }
}