using CommunityToolkit.Mvvm.ComponentModel;
using ZelekWieclaw.VisualProgrammingProject.BL;
using ZelekWieclaw.VisualProgrammingProject.Core;
using ZelekWieclaw.VisualProgrammingProject.Interfaces;

namespace ZelekWieclaw.VisualProgrammingProject.ViewModels
{
    public class BeerProductViewModel : ObservableObject, IQueryAttributable
    {
        private IBeerProduct _product;
        private CatalogService _catalogService;

        public BeerProductViewModel(IBeerProduct product)
        {
            _product = product;
            _catalogService = new CatalogService();
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

        void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query) // Change to public
        {
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