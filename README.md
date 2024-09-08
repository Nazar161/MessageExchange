to run application 
`docker compose up -d --build`


to stop application
`docker compose down`

Client 1: http://localhost:8080/MessageSender


Client 2: http://localhost:8080/MessageView


Client 3: http://localhost:8080/MessageHistory

Logs(Serilog + Seq): http://localhost:8081/#/events?range=5m

Swagger: http://localhost:8080/swagger/index.html







