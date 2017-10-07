using System;
using MJNsoft.Base.Model.Abstractions;

namespace Timer.Data
{
    public class CommandEntity : IIdentifyable
    {
        public Guid Id { get; set; }
        public string Command { get; set; }
    }
}
