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
    public class TourVoucherRepository : ITourVoucherRepository
    {
        private const string filePath = "../../../Data/tourVouchers.csv";
        private readonly Serializer<TourVoucher> serializer = new();
        private List<TourVoucher> vouchers;
        public TourVoucherRepository() 
        {
            vouchers = serializer.FromCSV(filePath);
        }

        public int NextId()
        {
            vouchers = serializer.FromCSV(filePath);
            if (vouchers.Count < 1)
            {
                return 1;
            }
            return vouchers.Max(v => v.Id) + 1;
        }

        public TourVoucher Save(TourVoucher voucher)
        {
            voucher.Id = NextId();
            vouchers = serializer.FromCSV(filePath);
            vouchers.Add(voucher);
            serializer.ToCSV(filePath, vouchers);
            return voucher;
        }
    }
}
