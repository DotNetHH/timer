using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace Timer.WebApi
{
    public class AutoMapperProfileConfiguration : Profile
    {
        public AutoMapperProfileConfiguration() : this("TimerProfile")
        {
        }

        protected AutoMapperProfileConfiguration(string profileName) : base(profileName)
        {
            CreateMap<Timer.WebApi.Models.TaskModel, Timer.Abstractions.InterruptCommand>();
            CreateMap<Timer.WebApi.Models.TaskModel, Timer.Abstractions.StartTaskCommand>();
            CreateMap<Timer.WebApi.Models.TaskModel, Timer.Abstractions.StopTaskCommand>();
        }
    }
    
}
