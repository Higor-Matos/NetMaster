﻿// NetMaster.Services.Interfaces/IHardwareInfoService.cs
using NetMaster.Domain.Models.Results;

namespace NetMaster.Services.Interfaces
{
    public interface IHardwareInfoService<T> where T : class
    {
        Task<ServiceResultModel<T>> SaveLocalHardwareInfoAsync(string ip);
        Task<ServiceResultModel<T>> GetHardwareInfoByComputerNameAsync(string computerName);
    }
}
