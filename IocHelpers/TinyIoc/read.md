# TinyIoc helpers
== 
- Build and reference to provide tinyIoc compatible HandlerFactory and MessageMapper.

## Usage
======
- Instantiate BrighterIocHelperTinyIoc
- Call Register<TRequest, THandler, TMapper> to register a Command/Event, Handler and Mapper in one call.
- Use .Mappers / .Handlers to pass to Dispatcher builder...