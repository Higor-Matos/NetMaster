﻿// NetMaster.Services/Implementation/System/UsersInfoService.cs
using NetMaster.Domain.Models;
using NetMaster.Domain.Models.DataModels;
using NetMaster.Domain.Models.Results;
using NetMaster.Repository.Implementation.System;
using NetMaster.Repository.Interfaces.BaseCommand;
using NetMaster.Repository.Interfaces.System;
using NetMaster.Services.Implementations.BaseCommands;
using NetMaster.Services.Interfaces.BaseCommands;
using NetMaster.Services.Interfaces.System;

namespace NetMaster.Services.Implementation.System
{
    public class UsersInfoService : BaseService, IUsersInfoService
    {
        private readonly IBaseRepository<UsersInfoDataModel> _repository;
        private readonly LocalUsersRepository _localRepository;

        public UsersInfoService(
            IBaseRepository<UsersInfoDataModel> repository,
            LocalUsersRepository localRepository,
            ICommandRunner commandRunner,
            IResultConverter resultConverter
        ) : base(commandRunner, resultConverter)
        {
            _repository = repository;
            _localRepository = localRepository;
        }

        public async Task SaveLocalSystemInfoAsync(string ip)
        {
            RepositoryResultModel<UsersInfoDataModel> localInfoResult = await _localRepository.ExecCommand(new RepositoryPowerShellParamModel(ip));
            if (localInfoResult.SuccessResult != null)
            {
                await _repository.InsertAsync(localInfoResult.SuccessResult.Result);
            }
        }

        public async Task<ServiceResultModel<UsersInfoDataModel>> GetSystemInfo(string ip)
        {
            UsersInfoDataModel info = await _repository.GetByComputerNameAsync(ip);
            return CreateServiceResult(info, "Não foi possível obter informações dos usuários locais.", ip);
        }
    }
}