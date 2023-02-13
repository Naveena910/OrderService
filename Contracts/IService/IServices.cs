using Entities.Dtos.ResponseDto;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.IService
{
    public interface IServices
    {
        public ErrorDto ModelState(ModelStateDictionary ModelState);
        /// <summary>
        ///checks user 
        /// </summary>
        public Guid Usercheck();
    }
}
