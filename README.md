# Lightning-Alert

A sample console application for lightning alert written in C# using Visual Studio 2022.

If you run the application you need to provide the full path of the .json files, see example below:
```
C:\lightning.json
C:\assets.json
```
![lightning](https://user-images.githubusercontent.com/112358797/195335491-404a3c74-a66a-4178-b511-61eacf80d4e8.jpg)


### Questions:

- What is the [time complexity](https://en.wikipedia.org/wiki/Time_complexity) for determining if a strike has occurred for a particular asset?

> O(n) - 

- If we put this code into production, but found it too slow, or it needed to scale to many more users or more frequent strikes, what are the first things you would think of to speed it up?

> We could add or use MessageQueuing like RabbitMQ or any MessageQueuing services. In addtion, create a separate application for uploading files that sends data into the queue. 
