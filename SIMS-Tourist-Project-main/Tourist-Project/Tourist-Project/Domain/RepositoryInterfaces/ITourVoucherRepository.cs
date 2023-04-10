using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist_Project.Domain.Models;

namespace Tourist_Project.Domain.RepositoryInterfaces
{
    public interface ITourVoucherRepository
    {
        public TourVoucher Save(TourVoucher tourVoucher);
        public int NextId();

    }
}
