﻿

using AutoMapper;

namespace SchoolManagementSystem.Application.Mapping;

public interface IMapFrom<T>
{
    void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType()).ReverseMap();

}
