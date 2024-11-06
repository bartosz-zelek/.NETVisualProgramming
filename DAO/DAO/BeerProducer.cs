using ZelekWieclaw.VisualProgrammingProject.Interfaces;

namespace ZelekWieclaw.VisualProgrammingProject.DAOMock
{
    internal class BeerProducer : IBeerProducer
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string FoundationYear { get; set; }
        public required string Country { get; set; }
    }
}
