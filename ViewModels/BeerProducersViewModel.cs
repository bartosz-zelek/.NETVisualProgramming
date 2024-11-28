using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows.Input;
using ZelekWieclaw.VisualProgrammingProject.BL;
using ZelekWieclaw.VisualProgrammingProject.DAOMock;


namespace ZelekWieclaw.VisualProgrammingProject.ViewModels
{
    public class BeerProducersViewModel : IQueryAttributable
    {
        public ObservableCollection<BeerProducerViewModel> AllProducers { get; }
        public ICommand NewCommand { get; }
        public ICommand SelectProducerCommand { get; }

        private CatalogService _catalogService;

        public BeerProducersViewModel()
        {
            _catalogService = new CatalogService();
            AllProducers = new ObservableCollection<BeerProducerViewModel>(
                _catalogService.GetAllBeerProducers().Select(p => new BeerProducerViewModel(p)));
            NewCommand = new AsyncRelayCommand(NewProducerAsync);
            SelectProducerCommand = new AsyncRelayCommand<BeerProducerViewModel>(SelectProducerAsync);
        }

        private async Task NewProducerAsync()
        {
            await Shell.Current.GoToAsync("BeerProducerPage");
        }

        private async Task SelectProducerAsync(BeerProducerViewModel producer)
        {
            if (producer == null)
            {
                return;
            }

            await Shell.Current.GoToAsync($"BeerProducerPage?load={producer.Id}");
        }

        void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query) // Change to public
        {
            if (query.TryGetValue("saved", out var _producer_state))
            {
                string producerJson = Uri.UnescapeDataString(_producer_state.ToString());
                var producer_state = JsonSerializer.Deserialize<BeerProducer>(producerJson);
                var producer = AllProducers.FirstOrDefault(p => p.Id == producer_state.Id);
                if (producer != null)
                {
                    producer.Reload();
                    _catalogService.UpdateBeerProducer(producer_state);
                }
                else
                {
                    _catalogService.AddBeerProducer(producer_state);
                    AllProducers.Insert(0, new BeerProducerViewModel(producer_state));
                }
            }
            else if (query.TryGetValue("deleted", out var deletedId))
            {
                var producer = AllProducers.FirstOrDefault(p => p.Id == int.Parse((string)deletedId));
                if (producer != null)
                {
                    // _catalogService.DeleteBeerProducer(producer.Id);
                    AllProducers.Remove(producer);
                }
            }
        }
    }
}