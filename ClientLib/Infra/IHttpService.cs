using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ClientLib.Infra
{
    public interface IHttpService
    {
        Task<R> PostAsync<T, R>(string url, T payload, string token = "");
        Task<R> GetAsync<R>(string url, string token = "");
    }
}
