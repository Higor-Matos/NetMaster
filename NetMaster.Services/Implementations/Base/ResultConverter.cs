﻿// NetMaster.Services/Implementations/BaseCommands/CResultConverter.cs
using NetMaster.Domain.Models.Results.Repository;
using NetMaster.Services.Interfaces.Base;

namespace NetMaster.Services.Implementations.BaseCommands
{
    public class ResultConverter : IResultConverter
    {
        public RepositoryResultModel<object> Convert(RepositoryResultModel<string> result)
        {
            return result.SuccessResult != null
                ? new RepositoryResultModel<object>(data: result.SuccessResult.Result, success: true, error: result.ErrorResult)
                : new RepositoryResultModel<object>(success: false, error: result.ErrorResult);
        }
    }
}
