﻿// NetMaster.Services/Implementations/Hardware/RamInfoService.cs
using NetMaster.Domain.Models.DataModels;
using NetMaster.Domain.Models.Results.Service;
using NetMaster.Repository.Interfaces.Hardware;
using NetMaster.Services.Interfaces.Base;
using NetMaster.Services.Interfaces.Hardware;

namespace NetMaster.Services.Implementations.Hardware
{
    public class RamInfoService : HardwareInfoService<RamInfoDataModel>, IRamInfoService
    {
        public RamInfoService(
            IRamRepository ramRepository,
            ILocalRamRepository localRamRepository,
            ICommandRunner commandRunner,
            IResultConverter resultConverter
        ) : base(ramRepository, localRamRepository, commandRunner, resultConverter)
        {
        }

        public Task<ServiceResultModel<RamInfoDataModel>> SaveLocalRamInfoAsync(string ip)
        {
            return SaveLocalHardwareInfoAsync(ip);
        }

        public Task<ServiceResultModel<RamInfoDataModel>> GetRamInfoByComputerNameAsync(string computerName)
        {
            return GetHardwareInfoByComputerNameAsync(computerName);
        }
    }
}
