# TV Maze Scraper API                                                                                                                     
This project consists of 2 parts that RESTful API and Scraper application. The project was developed by using C#, .NET Core, MongoDB, DDD approaches and several tools such as Microsoft Extensions and Polly for retry operations.

## Infrastructure

### RESTful API

This is an API for exposing TV Shows data which scraped by the application.

#### Scraper Application
This is an application which runs periodically scraping TV Shows from TV Maze database. It runs every 24 hours. When it runs, it will be creating a database named "TvMazeScraper", collections named "Shows" and saving the data which scraped from TV Maze API.

## RESTful API Interfaces
Swagger was added for this project and you can reach it for testing the API using http://localhost:5000/swagger/index.html address.


## Build up the project with Docker
All project was dockerized. You can run build-all.sh script where is in the root directory of the project using any terminal. After the building, the project will be running on localhost:5000.

`bash build-all.sh`

##### Benefits of Docker
Simply, if you have Docker, you don't need set up the applications on your local machine. With simply a few Docker commands and files, you can build the project on a virtualized container created by Docker. Also, you can scale all projects using one command on Docker. Especially, it is a very useful technique in microservices architecture.

### Testing with Swagger
After the project builds up, then for testing, you can use Swagger.

### Testing with cURL
After the project builds up, then for testing, you can use the following cURL commands.

#### TV Shows Request Example
You can use the following cURL command to get TV Shows in the database.


```
curl -X GET "http://localhost:5000/api/shows?offset=:offset&limit=:limit" -H "accept: application/json"
```
You can use the parameters which offset and limit for paging.

### Benefits of Docker
Simply, if you have Docker, you don't need set up the applications into local machine. With simply a few Docker commands and files, you can build the project on a virtualized container created by Docker. Also, you can scale all projects using one command on Docker. Especially, it is a very useful technique in microservices architecture.
