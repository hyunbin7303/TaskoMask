﻿using AutoMapper;
using TaskoMask.BuildingBlocks.Contracts.Dtos.Organizations;
using TaskoMask.BuildingBlocks.Contracts.Dtos.Projects;
using TaskoMask.BuildingBlocks.Contracts.Protos;

namespace TaskoMask.ApiGateways.UserPanel.Aggregator.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<GetOrganizationsByOwnerIdGrpcResponse, OrganizationBasicInfoDto>();

            CreateMap<GetProjectsByOrganizationIdGrpcRequest, ProjectBasicInfoDto>();

            CreateMap<GetProjectByIdGrpcResponse, ProjectBasicInfoDto>();

            CreateMap<GetProjectByIdGrpcResponse, ProjectOutputDto>();
        }
    }
}
