using APICore.Common.DTO.Response;
using APICore.Data.Entities;
using AutoMapper;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Net;

namespace APICore.API.Utils
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<TaskM, TaskMResponse>();
        }
    }
}