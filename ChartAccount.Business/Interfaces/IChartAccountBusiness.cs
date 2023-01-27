

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
        OperationResult Insert(ChartAccount entity, bool autoSave = true);
        OperationResult Update(ChartAccount entity, bool autoSave = true);
        OperationResult Delete(int id, bool autoSave = true);
        ChartAccount GetById(int id);
        string GetNextCode(string parentCode);
    }
}
