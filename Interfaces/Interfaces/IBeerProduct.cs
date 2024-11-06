using ZelekWieclaw.VisualProgrammingProject.Core;

namespace ZelekWieclaw.VisualProgrammingProject.Interfaces
{
    public interface IBeerProduct
    {
        public int Id { get; set; }
        public int ProducerId { get; set; }
        public string Name { get; set; }
        public BeerType Type { get; set; }
        public float Abv { get; set; }
        public int Ibu { get; set; }
    }
}
