﻿using ContactsApp.Core.Customs.Exceptions;
using ContactsApp.Core.Wrappers;
using ContactsApp.ReportService.DTOs;
using ContactsApp.ReportService.Entities;
using ContactsApp.ReportService.Extensions;
using ContactsApp.ReportService.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace ContactsApp.ReportService.Controller
{
    [ApiController]
    [Route("reports")]
    public class ReportController : ControllerBase
    {
        private readonly IReportUnitOfWork _unitOfWork;

        public ReportController(IReportUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<BaseResponse> Get()
        {
            List<Report> reports =await _unitOfWork.ReportRepository.GetAllAsync();

            List<ReportDTO> reportDTOs = reports.Select(x => x.AsReportDTO()).ToList();

            return new BaseResponse()
            {
                Message = "Raporlar getirildi",
                Data = reportDTOs
            };
        }

        [HttpGet("{id}")]
        public async Task<BaseResponse> Get(Guid id)
        {
            if (id == Guid.Empty) return new InvalidModelException("Id").HandleException();

            Report report = await _unitOfWork.ReportRepository.GetAsync(id);
            
            if (report.Equals(default(Report))) return new NotFoundException("Aradığınız rapor").HandleException();

            ReportDetailDTO reportDetailDTO = report.AsReportDetailDTO();
            
            return new BaseResponse()
            {
                Data = reportDetailDTO,
                Message = "İstediğiniz rapor getirildi"
            };
        }

        [HttpGet("create")]
        public async Task<BaseResponse> Create()
        {
            Report report = new Report()
            {
                Id = new Guid(),
                CreatedOn = DateTime.Now,
                Status = ReportStatus.Pending
            };

            await _unitOfWork.ReportRepository.CreateAsync(report);

            return new BaseResponse()
            {
                Data = report,
                Message = $"{report.CreatedOn} tarihli rapor oluşturma talebiniz işleme alınmıştır."
            };
        }
    }

}