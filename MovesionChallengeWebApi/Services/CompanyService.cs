using MovesionChallengeWebApi.Entities;

namespace MovesionChallengeWebApi.Services
{
    public interface ICompanyService
    {
        IEnumerable<Company> GetAll();
        Company GetById(int id);
        void Insert(Company company);
        void Update(Company company);
        void Delete(int id);
    }

    public class CompanyService : ICompanyService
    {
        /// <summary>
        /// harcoded users for simplicity
        /// </summary>
        private readonly List<Company> _companies = new()
        {
            new Company(1, "DELL LNV", "PIAZZA DEL DUOMO, 23", "531 917 51", 160000),
            new Company(2, "PAYPAL HOLDINGS INC", "PIAZZA DEL DUOMO, 25", "595 463 87", 13000),
            new Company(3, "TESLA SRL", "PIAZZA DEL DUOMO, 27", "475 604 96", 270000)
        };

        public IEnumerable<Company> GetAll()
        {
            return _companies;
        }

        public Company GetById(int id)
        {
            return _companies.Single(c => c.Id == id);
        }

        public void Delete(int id)
        {
            var target = GetById(id);
            if (target != null) {
                _companies.RemoveAt(_companies.IndexOf(target));
            }
        }

        public void Insert(Company company)
        {
            /// Id management implemented in this wrong way for simplicity
            var newId = _companies.Max(c => c.Id) + 1;
            _companies.Add(new Company(newId, company.Name, company.Address, company.Phone, company.Revenue));
        }

        public void Update(Company company)
        {
            var target = GetById(company.Id);
            var index = _companies.IndexOf(target);
            _companies[index].Name = company.Name;
            _companies[index].Address = company.Address;
            _companies[index].Phone = company.Phone;
            _companies[index].Revenue = company.Revenue;
        }
    }
}