

using ChartAccountDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartAccountBusiness.Interfaces
{
    public interface IChartAccountBusiness
    {
        Task<ChartAccountDomain.ChartAccount> x();
        //Task<ChartAccount.Domain.> Test(int temperature);
    }
}
