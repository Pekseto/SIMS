using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist_Project.Domain.Models;
using Tourist_Project.Domain.RepositoryInterfaces;
using Tourist_Project.Serializer;

namespace Tourist_Project.Repositories
{
    public class VoucherRepository : IVoucherRepository
    {
        private const string FilePath = "../../../Data/vouchers.csv";
        private readonly Serializer<Voucher> serializer;
        private List<Voucher> vouchers;

        public VoucherRepository()
        {
            serializer = new Serializer<Voucher>();
            vouchers = serializer.FromCSV(FilePath);
        }

        public void Delete(int id)
        {
            vouchers = GetAll();
            Voucher voucher = vouchers.Find(r => r.Id == id);
            vouchers.Remove(voucher);
            serializer.ToCSV(FilePath, vouchers);
        }

        public List<Voucher> GetAll()
        {
            return serializer.FromCSV(FilePath);
        }

        public Voucher GetById(int id)
        {
            vouchers = GetAll();
            return vouchers.Find(r => r.Id == id);
        }

        public int NextId()
        {
            vouchers = GetAll();
            if (vouchers.Count < 1)
            {
                return 1;
            }
            return vouchers.Max(r => r.Id) + 1;
        }

        public void Save(Voucher voucher)
        {
            voucher.Id = NextId();
            vouchers = GetAll();
            vouchers.Add(voucher);
            serializer.ToCSV(FilePath, vouchers);
        }

        public void Update(Voucher voucher)
        {
            vouchers = GetAll();
            var old = vouchers.Find(r => r.Id == voucher.Id);
            int index = vouchers.IndexOf(old);
            vouchers.Remove(old);
            vouchers.Insert(index, voucher);
            serializer.ToCSV(FilePath, vouchers);
        }
    }
}
