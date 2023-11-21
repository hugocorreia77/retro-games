# retro-games :video_game:

A simple .NET 6 Web Api to manage online games using MongoDB. 
Here you can see how to use dependency injection in ASP.NET Core 6.0 and MongoDB. 

You can deploy this project using Docker or using the compose.yaml file, which will also run a MongoDB image as it is a project dependency. 


## Docker

1 - Build the retro-games image from the Docker file

`docker build -t retro-games-image .`

2 - Create the network

`docker network create -d bridge retro-games_default`

3 - Create and run retro-games web api container using the network created before

`docker run -d --name retrogamescontainerimage --network retro-games_default -p 5010:80 retro-games-image`

4 - Create and run MongoDB using the same network 

`docker run -d --name mongodb --network retro-games_default -p 27018:27017 mongo`


## Docker Compose
Using `docker compose` will make your job easier as it will: 
1 - Create a default network 
2 - Create a container for MongoDb
3 - Create a container for retro-games web api

Using a terminal go to the project root folder, where the `compose.yaml` file is, and execute:
`docker compose up -d`

## Updating appSettings.json configurations

For both methods, at this point, both containers should be running. However, we need to configure the MongoDB connection string. 

First, check that you have the `retro-games_default` network:

`docker network ls`

If everithing is ok, you can check which IP address MongoDB container is using:

`docker network inspect retro-games_default`

Let's assume that it is using this IP address: `172.21.0.3`

Using the terminal you can go into the container and use `vim` to update the file. In order to do that, follow these commands:

`apt-get update`

`apt-get install vim` 

Then use vim to update the appsettings.json file:

`vim appsettings.json`

Set the value of `MongoDbConfigurations.ConnectionString` config to `172.21.0.3`

To check if everything is ok, you can call the webapi using your browser:
`http://localhost:5010/games`
