namespace ZelekWieclaw.VisualProgrammingProject.Interfaces
{
    public interface IBeerProducer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FoundationYear { get; set; }
        public string Country { get; set; }
    }
}