using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record PhotoDTO(Guid Id, string Name, string Picture, string PictureFormat);
}
