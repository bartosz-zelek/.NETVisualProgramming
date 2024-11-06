using ZelekWieclaw.VisualProgrammingProject.Core;
using ZelekWieclaw.VisualProgrammingProject.Interfaces;

namespace ZelekWieclaw.VisualProgrammingProject.DAOMock
{
    internal class BeerProduct : IBeerProduct
    {
        public int Id { get; set; }
        public int ProducerId { get; set; }
        public required string Name { get; set; }
        public BeerType Type { get; set; }
        public float Abv { get; set; }
        public int Ibu { get; set; }
    }
}
