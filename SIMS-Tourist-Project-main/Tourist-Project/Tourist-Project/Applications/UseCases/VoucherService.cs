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
        public List<Voucher> GetAllForUser(int userId)
        {
            var vouchers = new List<Voucher>();
            foreach (Voucher voucher in GetAll())
            {
                if (voucher.UserId == userId && voucher.LastValidDate >= DateTime.Today)
                {
                    vouchers.Add(voucher);
                }
            }
            return vouchers;
        }

        public void DeleteInvalidVouchers()
        {
            foreach(Voucher voucher in GetAll())
            {
                if(voucher.LastValidDate < DateTime.Today)
                {
                    Delete(voucher.Id);
                }
            }
        }
    }
}
