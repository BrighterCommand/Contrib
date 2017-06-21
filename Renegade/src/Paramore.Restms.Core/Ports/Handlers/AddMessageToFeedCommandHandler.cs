﻿// ***********************************************************************
// Assembly         : paramore.brighter.restms.core
// Author           : ian
// Created          : 10-16-2014
//
// Last Modified By : ian
// Last Modified On : 10-21-2014
// ***********************************************************************
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

#region Licence
/* The MIT License (MIT)
Copyright © 2014 Ian Cooper <ian_hammond_cooper@yahoo.co.uk>

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the “Software”), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE. */

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Paramore.Brighter;
using log4net;
using paramore.brighter.restms.core.Extensions;
using paramore.brighter.restms.core.Model;
using paramore.brighter.restms.core.Ports.Commands;
using paramore.brighter.restms.core.Ports.Common;

namespace paramore.brighter.restms.core.Ports.Handlers
{
    /// <summary>
    /// Class AddMessageToFeedCommandHandler.
    /// </summary>
    public class AddMessageToFeedCommandHandler : RequestHandler<AddMessageToFeedCommand>
    {
        private readonly IAmARepository<Feed> _feedRepository;
        private readonly IAmACommandProcessor _commandProcessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestHandler{TRequest}"/> class.
        /// </summary>
        /// <param name="feedRepository"></param>
        /// <param name="logger">The logger.</param>
        public AddMessageToFeedCommandHandler(IAmARepository<Feed> feedRepository, IAmACommandProcessor commandProcessor, ILog logger)
        {
            _feedRepository = feedRepository;
            _commandProcessor = commandProcessor;
        }

        #region Overrides of RequestHandler<AddMessageToFeedCommand>

        /// <summary>
        /// Handles the specified command.
        /// </summary>
        /// <param name="addMessageToFeedCommand">The command.</param>
        /// <returns>TRequest.</returns>
        public override AddMessageToFeedCommand Handle(AddMessageToFeedCommand addMessageToFeedCommand)
        {
            IEnumerable<Pipe> pipes;
            using (var scope = new TransactionScope())
            {
                var feed = _feedRepository[new Identity(addMessageToFeedCommand.FeedName)];
                if (feed == null)
                {
                    throw new FeedDoesNotExistException();
                }

                pipes = feed.AddMessage(
                    new Model.Message(
                        new Address(addMessageToFeedCommand.Address),
                        feed.Href,
                        addMessageToFeedCommand.Headers,
                        addMessageToFeedCommand.Attachment,
                        !string.IsNullOrEmpty(addMessageToFeedCommand.ReplyTo) ? new Uri(addMessageToFeedCommand.ReplyTo) : null));

                addMessageToFeedCommand.MatchingJoins = pipes.Count();

                scope.Complete();
            }

            pipes.Each(pipe => _commandProcessor.Send(new InvalidateCacheCommand(pipe.Href)));

            return base.Handle(addMessageToFeedCommand);
        }

        #endregion
    }
}
