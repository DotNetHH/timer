﻿using System;
using MJNsoft.Base.Model.Abstractions;

namespace Timer.Data
{
    internal class CommandEntity : IIdentifyable
    {
        public Guid Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Command { get; set; }
    }
}
