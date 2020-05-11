using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace Service.Interface
{
    public interface ITrackingService
    {
        Produto GetTracking(string codigo);
    }
}
