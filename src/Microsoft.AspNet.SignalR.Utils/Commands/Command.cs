﻿using System;

namespace Microsoft.AspNet.SignalR.Utils
{
    internal abstract class Command : ICommand
    {
        public Command(Action<string> info, Action<string> success, Action<string> warning, Action<string> error)
        {
            Info = info;
            Success = success;
            Warning = warning;
            Error = error;
        }

        public abstract string DisplayName { get; }

        public abstract string Help { get; }

        public abstract string[] Names { get; }

        public abstract void Execute(string[] args);

        protected Action<string> Info { get; private set; }

        protected Action<string> Success { get; private set; }

        protected Action<string> Warning { get; private set; }

        protected Action<string> Error { get; private set; }
    }
}
