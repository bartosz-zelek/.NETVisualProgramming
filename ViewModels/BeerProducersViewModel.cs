using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows.Input;
using ZelekWieclaw.VisualProgrammingProject.BL;
using ZelekWieclaw.VisualProgrammingProject.DAO;


namespace ZelekWieclaw.VisualProgrammingProject.ViewModels
{
    public class BeerProducersViewModel : IQueryAttributable
    {
        public ObservableCollection<BeerProducerViewModel> AllProducers { get; }
        public ICommand NewCommand { get; }
        public ICommand SelectProducerCommand { get; }
        public ICommand PerformSearchCommand { get; }

        private CatalogService _catalogService;

        public BeerProducersViewModel()
        {
            _catalogService = new CatalogService();
            AllProducers = new ObservableCollection<BeerProducerViewModel>(
                _catalogService.GetAllBeerProducers().Select(p => new BeerProducerViewModel(p)));
            NewCommand = new AsyncRelayCommand(NewProducerAsync);
            SelectProducerCommand = new AsyncRelayCommand<BeerProducerViewModel>(SelectProducerAsync);
            PerformSearchCommand = new AsyncRelayCommand<string>(PerformSearchTask);
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

        private async Task PerformSearchTask(string query)
        {
            AllProducers.Clear();
            var producers = _catalogService.GetAllBeerProducers()
                .Where(p => p.Name.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0);
            foreach (var producer in producers)
            {
                AllProducers.Add(new BeerProducerViewModel(producer));
            }
        }

        void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
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

                query.Remove("saved");
            }
            else if (query.TryGetValue("deleted", out var deletedId))
            {
                var producer = AllProducers.FirstOrDefault(p => p.Id == int.Parse((string)deletedId));
                if (producer != null)
                {
                    _catalogService.DeleteBeerProducer(producer.Id);
                    AllProducers.Remove(producer);
                }

                query.Remove("deleted");
            }
        }
    }
}