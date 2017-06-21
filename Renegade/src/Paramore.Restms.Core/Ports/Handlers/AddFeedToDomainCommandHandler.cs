// ***********************************************************************
// Assembly         : paramore.brighter.restms.core
// Author           : ian
// Created          : 10-07-2014
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
Copyright � 2014 Ian Cooper <ian_hammond_cooper@yahoo.co.uk>

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the �Software�), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED �AS IS�, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE. */

#endregion

using System.Transactions;
using Paramore.Brighter;
using log4net;
using paramore.brighter.restms.core.Ports.Commands;
using paramore.brighter.restms.core.Ports.Common;
using paramore.brighter.restms.core.Ports.Repositories;

namespace paramore.brighter.restms.core.Ports.Handlers
{
    public class AddFeedToDomainCommandHandler : RequestHandler<AddFeedToDomainCommand>
    {
        private readonly InMemoryDomainRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestHandler{TRequest}"/> class.
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="logger">The logger.</param>
        public AddFeedToDomainCommandHandler(InMemoryDomainRepository repository, ILog logger)
        {
            _repository = repository;
        }

        /// <summary>
        /// Adds the feed to the specified domain
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns>TRequest.</returns>
        public override AddFeedToDomainCommand Handle(AddFeedToDomainCommand command)
        {
            using (var scope = new TransactionScope())
            {
                var domain = _repository[new Identity(command.DomainName)];
                if (domain == null)
                {
                    throw new DomainDoesNotExistException();
                }

                domain.AddFeed(new Identity(command.FeedName));
                scope.Complete();
            }

            return base.Handle(command);
        }
    }
}