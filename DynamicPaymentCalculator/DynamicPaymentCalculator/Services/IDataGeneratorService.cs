using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicPaymentCalculator.Services
{
    public interface IDataGeneratorService
    {
        System.Data.DataTable GenerateData(int count);
    }
}
