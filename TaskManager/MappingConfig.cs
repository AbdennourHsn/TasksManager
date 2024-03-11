using System;
using AutoMapper;
using TaskManager.DTOs;
using TaskManager.Entities;

namespace TaskManager
{
	public class MappingConfig
	{
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<TaskDto, TaskEntity>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}

