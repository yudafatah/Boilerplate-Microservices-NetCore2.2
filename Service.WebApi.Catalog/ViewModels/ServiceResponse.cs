using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.WebApi.Catalog.ViewModels
{
    public class ServiceResponse<T>
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public List<T> Data { get; set; }
    }

    public class ServiceResponseSingle<T>
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
