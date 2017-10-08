using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Timer.Business.Abstractions;
using Timer.Data.Abstractions;

namespace Timer.WebApi
{
    public class AutoMapperProfileConfiguration : Profile
    {
        public AutoMapperProfileConfiguration() : this("TimerProfile")
        {
        }

        protected AutoMapperProfileConfiguration(string profileName) : base(profileName)
        {
            CreateMap<Timer.WebApi.Models.TaskModel, InterruptTaskCommand>();
            CreateMap<Timer.WebApi.Models.TaskModel, StartTaskCommand>();
            CreateMap<Timer.WebApi.Models.TaskModel, StopTaskCommand>();
            CreateMap<TimerEvent, Timer.WebApi.Models.TaskModel> ();
        }
    }
    
}
