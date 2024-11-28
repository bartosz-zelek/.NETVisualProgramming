using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Windows.Input;
using ZelekWieclaw.VisualProgrammingProject.BL;
using ZelekWieclaw.VisualProgrammingProject.DAOMock;
using ZelekWieclaw.VisualProgrammingProject.Interfaces;

namespace ZelekWieclaw.VisualProgrammingProject.ViewModels
{
    public class BeerProducerViewModel : ObservableValidator, IQueryAttributable
    {
        private IBeerProducer _producer;
        private readonly CatalogService _catalogService;

        public BeerProducerViewModel()
        {
            _producer = new BeerProducer();
            _catalogService = new CatalogService();
            SaveCommand = new AsyncRelayCommand(Save);
            DeleteCommand = new AsyncRelayCommand(Delete);
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

        private async Task Save()
        {
            // Serialize the producer object to JSON
            string producerJson = JsonSerializer.Serialize(_producer);

            // Encode the serialized string
            string encodedProducer = Uri.EscapeDataString(producerJson);

            await Shell.Current.GoToAsync($"..?saved={encodedProducer}");
        }

        private async Task Delete()
        {
            _catalogService.DeleteBeerProducer(_producer.Id);
            await Shell.Current.GoToAsync($"..?deleted={_producer.Id}");
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

        public String NameError => GetErrors(nameof(Name))?.FirstOrDefault()?.ErrorMessage;

        public int Id
        {
            get => _producer.Id;
        }

        [Required(ErrorMessage = "Nazwa jest wymagana")]
        public string Name
        {
            get => _producer.Name;
            set => SetProperty(_producer.Name, value, _producer, (p, v) => p.Name = v, true);
        }

        [Required(ErrorMessage = "Rok założenia jest wymagany")]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "Rok założenia musi być 4-cyfrowym rokiem")]
        public string FoundationYear
        {
            get => _producer.FoundationYear;
            set => SetProperty(_producer.FoundationYear, value, _producer, (p, v) => p.FoundationYear = v, true);
        }

        [Required(ErrorMessage = "Kraj pochodzenia jest wymagany")]
        public string Country
        {
            get => _producer.Country;
            set => SetProperty(_producer.Country, value, _producer, (p, v) => p.Country = v, true);
        }
    }
}