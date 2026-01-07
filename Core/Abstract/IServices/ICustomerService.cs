using Core.Concretes.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Abstract.IServices
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerListItemDTO>> GetAllAsync(ClaimsPrincipal user);
        Task<CustomerDetailDTO> GetAsync(int id);
    
    }

}