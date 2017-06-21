﻿// ***********************************************************************
// Assembly         : paramore.brighter.restms.core
// Author           : ian
// Created          : 10-21-2014
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
using Paramore.Brighter;
using paramore.brighter.restms.core.Model;

namespace paramore.brighter.restms.core.Ports.Commands
{
    /// <summary>
    /// Class AddJoinToFeedCommand.
    /// </summary>
    public class AddJoinToFeedCommand : Command
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Command" /> class.
        /// </summary>
        /// <param name="pipeIdentity">The pipe identity.</param>
        /// <param name="feedAddress">The feed address.</param>
        /// <param name="addressPattern">The address pattern.</param>
        public AddJoinToFeedCommand(Pipe pipe, string feedAddress, string addressPattern) : base(Guid.NewGuid())
        {
            FeedAddress = feedAddress;
            AddressPattern = addressPattern;
            Pipe = pipe;
        }

        /// <summary>
        /// Gets the feed address.
        /// </summary>
        /// <value>The feed address.</value>
        public string FeedAddress { get; private set; }
        /// <summary>
        /// Gets the address pattern.
        /// </summary>
        /// <value>The address pattern.</value>
        public string AddressPattern { get; private set; }
        /// <summary>
        /// Gets the pipe identity.
        /// </summary>
        /// <value>The pipe identity.</value>
        public Pipe Pipe { get; private set; }
    }
}
