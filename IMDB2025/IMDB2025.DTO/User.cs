namespace IMDB2025.DTO
{
    public class User
    {
        public int UserId { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public DateTime RowInsertTime { get; set; }
        public DateTime RowUpdateTime { get; set; }

        public List<Privilege> Privileges { get; set; }

        public override string ToString()
        {
            return $"UserId: {UserId}, Login: {Login}, Email: {Email}, RowInsertTime: {RowInsertTime}, RowUpdateTime: {RowUpdateTime}, Privileges: [{string.Join(", ", Privileges)}]";
        }
    }
}
