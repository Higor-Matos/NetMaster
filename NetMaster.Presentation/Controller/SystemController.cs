﻿using Microsoft.AspNetCore.Mvc;
using NetMaster.Domain.Models.DataModels;
using NetMaster.Domain.Models.Results;
using NetMaster.Services;
using NetMaster.Services.Implementation.System;

namespace NetMaster.Presentation.Controllers
{
    [ApiController]
    [Route("system")]
    public class SystemController : BaseController
    {
        private readonly SystemService _systemService;
        private readonly SystemCommandService _systemCommandService;

        public SystemController(SystemService systemService)
        {
            _systemService = systemService;
        }

        [HttpPost("shutdownPc")]
        public async Task<IActionResult> ShutdownPc([FromBody] IpRequestDataModels request)
        {
            ServiceResultModel<string> result = await _systemCommandService.ShutdownPcComand(request.Ip);
            return ToActionResult(result);
        }

        [HttpPost("restartPc")]
        public async Task<IActionResult> RestartPc([FromBody] IpRequestDataModels request)
        {
            ServiceResultModel<string> result = await _systemCommandService.RestartPcComand(request.Ip);
            return ToActionResult(result);
        }


        [HttpGet("getInfo/{infoType}/{computerName}")]
        public async Task<IActionResult> GetInfo(string infoType, string computerName)
        {
            switch (infoType.ToLower())
            {
                case "users":
                    ServiceResultModel<UsersInfoDataModel> usersResult = await _systemService.GetUsers(computerName);
                    return ToActionResult(usersResult);

                case "chocolatey":
                    ServiceResultModel<ChocolateyInfoDataModel> chocolateyResult = await _systemService.VerifyChocolateyComand(computerName);
                    return ToActionResult(chocolateyResult);

                case "osversion":
                    ServiceResultModel<OSVersionInfoDataModel> osVersionResult = await _systemService.GetOsVersion(computerName);
                    return ToActionResult(osVersionResult);

                case "installedprograms":
                    ServiceResultModel<InstalledProgramsResponseModel> installedProgramsResult = await _systemService.GetInstalledPrograms(computerName);
                    return ToActionResult(installedProgramsResult);

                default:
                    return BadRequest($"Invalid infoType: {infoType}. Valid options are: users, shutdown, restart, chocolatey, osversion, installedprograms.");
            }
        }


    }
}