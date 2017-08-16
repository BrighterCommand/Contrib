from brightside.handler import Handler, Command
from brightside.messaging import BrightsideMessage, BrightsideMessageStore


class FakeMessageStore(BrightsideMessageStore):
    def __init__(self):
        self._message_was_added = None
        self._messages = []

    @property
    def message_was_added(self):
        return self._message_was_added

    def add(self, message: BrightsideMessage):
        self._messages.append(message)
        self._message_was_added = True

    def get_message(self, key):
        for msg in self._messages:
            if msg.id == key:
                return msg
        return None


class HelloWorldCommand(Command):
    """Note that for interop, we use a field, named in camelCase; we don't want _x field names, because that exposes an
        implementation detail when serialized.  Consumers in other languages may not follow that Python idiom and
        thus hydrate these private fields correctly.
    """
    def __init__(self, greeting: str=None) -> None:
        super().__init__()
        self.Greeting = greeting


class HelloWorldCommandHandler(Handler):
    def handle(self, request: HelloWorldCommand) -> None:
        print("Hello {}".format(request.Greeting))


