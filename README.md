# Fullstack dev test assignment
## Intro
This repo is my test assignment for a dev position. The aim was to create a system of 2 containerized apps using Docker:
- Server in Python
- Client in .NET

The server should also allow any text to be typed in via console (interactive mode).
The client should print out the typed in the server message to the console as it is being typed ("soft real time").

## Python : Server
The server is running at port 65432 and is waiting for any clients to be connected. Once a single connection is established, the keyboard listener ([sshkeyboard](https://sshkeyboard.readthedocs.io/en/latest/)) is used and on each key press just sends the pressed key to the connected client. The console also displays which key was pressed. `Esc` key is used to exit the session.

## .NET : Client
The client is running on .NET Core 3.1 and connects to the server host via port 65432. Each time something is written into the console, it is being shown in the console. 

# How to run

## Step 1: Cloning the git repos
Open the console and clone the repos:

`git clone https://github.com/oleshkin/PythonServer.git`

`git clone https://github.com/oleshkin/DotNetClient.git`

## Step 2: Build the Docker Images
Open up a terminal, move into the recently created PythonServer folder

`cd PythonServer`

And enter the command below to create a new image called **pythonserver**:

`docker build . -t pythonserver`

Next, perform the same actions for the DotNetClient, but naming it **dotnetclient**

``cd ..``

`cd DotNetClient`

`docker build . -t dotnetclient`


## Step 3: Creating a new Docker network
In order for the containers to communicate, we will expose the same port 65432 (which is made via the Dockerfile), but we also need to create a user-defined Docker network, so that the we could use the Container names as hostnames. 
In the terminal, type in the following:
`docker network create merry-network`

## Step 4: Run the server in container
Using the terminal, enter the following command:

`docker run -d -it --network=merry-network --name pythonserver pythonserver`

Check that you can see the **pythonserver** container running:

`docker container ls`

Now lets connect to the running server:

`docker attach pythonserver`

## Step 5: Run the client in container
Open a second terminal window, enter the following command:

`docker run -it --network=merry-network --name dotnetclient dotnetclient`

## Step 6: Enjoy!
If you switch back to the pythonserver terminal window, you should see the client connected msg:

```Connected by {ip}:65432```

Start typing in that window and see what people in 1800 only dreamed of: a message transmitted without any loss. Mission complete.
