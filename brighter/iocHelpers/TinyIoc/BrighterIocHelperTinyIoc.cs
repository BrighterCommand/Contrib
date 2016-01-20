#region Licence
/* The MIT License (MIT)
Copyright © 2015 Ian Cooper <ian_hammond_cooper@yahoo.co.uk>

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

using paramore.brighter.commandprocessor;
using TinyIoC;

namespace paramore.contrib.brighter.iocHelpers.TinyIoc
{
    public class BrighterIocHelperTinyIoc
    {
        private readonly TinyIocMessageMapperFactory _mappers;
        private readonly TinyIocHandlerFactory _handlers;

        public BrighterIocHelperTinyIoc(TinyIoCContainer iocContainer)
        {
            _mappers = new TinyIocMessageMapperFactory(iocContainer);
            _handlers = new TinyIocHandlerFactory(iocContainer);
        }

        public void Register<TRequest, THandler, TMapper>()
            where TRequest : class, IRequest
            where TMapper : class, IAmAMessageMapper<TRequest>
            where THandler : class, IHandleRequests<TRequest>
        {
            _mappers.Register<TRequest, TMapper>();
            _handlers.Register<TRequest, THandler>();
        }

        public IAmAMessageMapperRegistry Mappers
        {
            get { return _mappers; }
        }

        public HandlerConfiguration Handlers
        {
            get { return new HandlerConfiguration(_handlers, _handlers); }
        }
    }
}