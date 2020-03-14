using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.DAL.Interfaces
{
    public interface IParkDao
    {
        IList<Park> GetParks();
        Park GetParkDetails(string parkCode);
        IList<Park> GetBestParks();
    }
}
