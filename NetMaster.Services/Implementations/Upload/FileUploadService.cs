﻿using Microsoft.AspNetCore.Http;
using NetMaster.Domain.Models.Results;
using NetMaster.Domain.Models.Results.Repository;
using NetMaster.Domain.Models.Results.Service;
using NetMaster.Repository.Interfaces.Uploud;
using NetMaster.Services.Implementations.BaseCommands;
using NetMaster.Services.Interfaces.Base;
using NetMaster.Services.Interfaces.Upload;

namespace NetMaster.Services.Implementations.Upload
{
    public class FileUploadService : BaseService, IFileUploadService
    {
        private readonly IUploadFileRepository _uploadFileRepository;

        public FileUploadService(ICommandRunner commandRunner, IResultConverter resultConverter, IUploadFileRepository uploadFileRepository)
            : base(commandRunner, resultConverter)
        {
            _uploadFileRepository = uploadFileRepository ?? throw new ArgumentNullException(nameof(uploadFileRepository));
        }

        public ServiceResultModel<object> ValidateFile(IFormFile file)
        {
            return file == null || file.Length == 0
                ? new ServiceResultModel<object>(
                    error: new ErrorServiceResult("File not provided or empty.", DateTime.Now, Environment.MachineName)
                )
                : new ServiceResultModel<object>(
                    success: new SuccessServiceResult<object>(new { Message = "File is valid" }, DateTime.Now, Environment.MachineName)
                );
        }


        public ServiceResultModel<UploadResult> UploadFile(IFormFile file)
        {
            ServiceResultModel<object> validationResult = ValidateFile(file);
            if (!validationResult.IsSuccess)
            {
                return new ServiceResultModel<UploadResult>(error: validationResult.ErrorResult);
            }

            string destinationFolder = "Uploads";
            byte[] fileData = ReadFileData(file);

            RepositoryResultModel<UploadResult> resultRep = _uploadFileRepository.UploadFile(file.FileName, fileData, destinationFolder);
            return resultRep.Success && resultRep.Data != null
                ? new ServiceResultModel<UploadResult>(
                    success: new SuccessServiceResult<UploadResult>(resultRep.Data, DateTime.Now, Environment.MachineName)
                )
                : new ServiceResultModel<UploadResult>(
                    error: new ErrorServiceResult(resultRep.ErrorResult?.Message ?? string.Empty, DateTime.Now, Environment.MachineName)
                );
        }

        private byte[] ReadFileData(IFormFile file)
        {
            using MemoryStream memoryStream = new();
            file.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }
    }
}
