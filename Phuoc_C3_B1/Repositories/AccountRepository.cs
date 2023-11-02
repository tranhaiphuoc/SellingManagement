using System.Linq;
using Phuoc_C3_B1.Models;
using System.Collections.Generic;


namespace Phuoc_C3_B1.Repositories
{
    public class AccountRepository : IRepositoryBase<Account>
    {
        private static readonly List<Account> _accounts = new List<Account>();

        public void Add(Account item)
        {
            _accounts.Add(item);
        }

        public Account GetById(int id)
        {
            return _accounts.FirstOrDefault(a => a.Id == id);
        }

        public List<Account> GetAll()
        {
            return _accounts;
        }

        public Account GetByLogIn(string username, string password)
        {
            return _accounts.FirstOrDefault(a => a.Username == username && a.Password == password);
        }
    }
}
