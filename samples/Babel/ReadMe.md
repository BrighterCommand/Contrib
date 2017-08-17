# What is this?
One goal here is to demonstrate interoperability between processes running different software stacks, using messaging as the medium, implemented using the Paramore family. This could be within a deployment boundary with a polygot service, but is most likely between two different services in a micro-services architecture.

# What do we show here
This sample has a C# producer and consumer, and a Python producer and consumer. These are derived from the single-language examples in the codebases for those languages.

To demonstrate the effectiveness of interop, you need to mix i.e. C# producer and Python consumer or vice versa.

# Gettng Started
The first thing is to check that you can run the Babel projects in the easiest use case - within the same runtine.
Navigate to the Python and CSharp directories and follow the instructions in the ReadMe.mds files there to ensure you can send messages between sender and receiver successfully. This will tell you that you have set up this sample correctly.

You will require RabbitMQ to be installed, but you can use a Docker for this if you do not want to install it locally.

Once you have established this case works, you can look to mix sender and receiver.

## Mixing  Producers and Consumers
Each language creates a different queue to subscriber for messages, so you can run both receivers at the same time.
An easy way to demonstrate the interop capability is to run both receivers, in seperate console windows and then in a new console window run either a Python of C# sender. Both receivers should respond to the message by printing a greeting at the console

## Using Docker
If you want to use docker you will want to use docker run to start the sender, and docker logs to view the console output on the receivers to check for success


