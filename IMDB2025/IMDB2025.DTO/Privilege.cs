namespace IMDB2025.DTO
{
    public class Privilege
    {
        public int PrivilegeId { get; set; }
        public PrivilegeType Name { get; set; }
        public DateTime RowInsertTime { get; set; }

        public override string ToString()
        {
            return Name.ToString();
        }
    }
}
