using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist_Project.Domain.Models;
using Tourist_Project.Domain.RepositoryInterfaces;

namespace Tourist_Project.Applications.UseCases
{
    public class VoucherService
    {
        private static readonly Injector injector = new();
        private readonly IVoucherRepository repository;

        public VoucherService()
        {
            repository = injector.CreateInstance<IVoucherRepository>();
        }

        public List<Voucher> GetAll()
        {
            return repository.GetAll();
        }
        public void Save(Voucher voucher)
        {
            repository.Save(voucher);
        }
        public void Delete(int id)
        {
            repository.Delete(id);
        }
        public void Update(Voucher voucher)
        {
            repository.Update(voucher);
        }
        public Voucher GetById(int id)
        {
            return repository.GetById(id);
        }
    }
}
