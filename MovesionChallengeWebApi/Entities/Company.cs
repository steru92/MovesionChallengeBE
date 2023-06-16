namespace MovesionChallengeWebApi.Entities
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int Revenue { get; set; }

        public Company(int id, string name, string address, string phone, int revenue)
        {
            /// Id management implemented in this wrong way for simplicity
            Id = id;
            Name = name;
            Address = address;
            Phone = phone;
            Revenue = revenue;
        }
    }
}