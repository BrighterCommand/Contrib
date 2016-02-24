﻿using System;
using System.Collections.Generic;
using System.Linq;
using paramore.commandprocessor;

namespace Paramore.Rewind.Core
{
    public class ConfigurationHelper
    {
        public IEnumerable<Type> FindCommands()
        {
            return typeof (Command)
                .Assembly
                .GetExportedTypes()
                .Where(expT => expT.BaseType == typeof (Command))
                .ToList();
        }

        public IEnumerable<Type> FindCommandHandlers()
        {
            return typeof (IHandleRequests<>)
                .Assembly
                .GetExportedTypes()
                .Where(
                    expT =>
                        expT.GetInterfaces().Any(
                            exptInterface =>
                                exptInterface.IsGenericType &&
                                exptInterface.GetGenericTypeDefinition() == typeof (IHandleRequests<>)))
                .ToList();
        }
    }
}