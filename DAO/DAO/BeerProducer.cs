using ZelekWieclaw.VisualProgrammingProject.Interfaces;

namespace ZelekWieclaw.VisualProgrammingProject.DAOMock
{
    public class BeerProducer : IBeerProducer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FoundationYear { get; set; }
        public string Country { get; set; }
    }
}