using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist_Project.Domain.Models;

namespace Tourist_Project.Domain.RepositoryInterfaces
{
    public interface IVoucherRepository
    {
        public List<Voucher> GetAll();
        public void Save(Voucher voucher);
        public int NextId();
        public void Delete(int id);
        public void Update(Voucher voucher);
        public Voucher GetById(int id);
    }
}
