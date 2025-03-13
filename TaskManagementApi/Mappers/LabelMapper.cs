using AutoMapper;
using TaskManagementApi.DTOs;
using TaskManagementApi.Models;

namespace TaskManagementApi.Mappers
{
    public class LabelMapper: Profile
    {
        public LabelMapper()
        {
            CreateMap<Label, LabelDto>();
            CreateMap<LabelDto, Label>();
        }
    }
}
