using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public class DealerShopNotFoundException : NotFoundException
    {
        public DealerShopNotFoundException(Guid id) 
            : base($"Dealer Shop with id: {id} doesn't exist in this database")
        {
        }

    }
}
