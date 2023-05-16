﻿using NetMaster.Domain.Models;
using NetMaster.Domain.Models.Results;
using NetMaster.Repository.Interfaces.Software;
using NetMaster.Repository.Local.Powershell;

namespace NetMaster.Repository.Local.Software
{
    public class InstallVlcRepository : BasePowershellRepository, IInstallVlcRepository
    {
        private static readonly string command = "choco install vlc -y --force";

        public async Task<RepositoryResultModel<string>> ExecCommand(RepositoryPowerShellParamModel param)
        {
            return await ExecCommand(param, command, jsonOutput => jsonOutput);
        }
    }
}