using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public class CarNotFoundException : NotFoundException
    {
        public CarNotFoundException(Guid carId) : base($"Car with id: {carId} doesn't exist")
        {}
    }
}
