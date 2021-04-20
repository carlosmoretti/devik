using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Service.Interface
{
    public interface ITrackingService
    {
        Task<Produto> GetTracking(string codigo);
    }
}
