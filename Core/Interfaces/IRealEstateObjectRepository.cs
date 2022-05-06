using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IRealEstateObjectRepository
    {
        public void ListAllRealEstateObjects();
        public void GetRealEstateObject();
        public void CreateRealEstateObject();
        public void UpdateRealEstateObject();
        public void DeleteRealEstateObject();
    }
}
