using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ZelekWieclaw.VisualProgrammingProject.BL;

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
            // await Shell.Current.GoToAsync(nameof(UI.ProducerPage));
        }

        private async Task SelectProducerAsync(BeerProducerViewModel producer)
        {
            if (producer == null)
            {
                return;
            }
            // await Shell.Current.GoToAsync($"{nameof(UI.ProducerPage)}?producerId={producer.Id}");
        }

        void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query) // Change to public
        {
            if (query.TryGetValue("saved", out var savedId))
            {
                var id = int.Parse((string)savedId);
                var producer = AllProducers.FirstOrDefault(p => p.Id == id);
                if (producer != null)
                {
                    producer.Reload();
                }
                else
                {
                    AllProducers.Insert(0, new BeerProducerViewModel(_catalogService.GetProducerById(id)));
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