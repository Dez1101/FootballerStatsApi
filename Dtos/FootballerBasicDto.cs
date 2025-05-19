namespace FootballerStatsApi.Dtos
{
    public class FootballerBasicDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Club { get; set; }
        public string Position { get; set; }
    }
}
