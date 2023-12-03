<h2>EventCamping Documentation</h2>

There are three executable applications: two of them are web servers, and the other one is a console app functioning as a background service. None of them requires a database connection. However, there is a layer called PropertyTechCase.Database for storing data locally. In high-traffic scenarios, though, this may not be the most sensible solution.

Web Servers have attribute called TokenRequired and there is a middleware called AuthorizationMiddleware both are check if there is a Authorization header exist or not for security purpose. Web Servers uses the package called MvcFilters for logging operations.
It goes without saying there are two middlewares, for the exception handling and the setting CorrelationId to request header for the request tracing.

<h3>PropertyTechCase.Api</h3>
Main purpose of PropertyTechCase.Api is just basic post and get action of campaigns for the frontend applications. It has two endpoints. And it uses local cache for post requests. Cache can be used as distributed for using multiple projects.

*GET*

`curl --location '/api/GetById/EventCampaigns/:id'`

*POST*

`curl --location '/api/Post/EventCampaigns' \
--header 'Content-Type: application/json' \
--data '{
"campaignCategoryId": "<long>",
"name": "<string>",
"budget": "<double>",
"spend": "<double>",
"type": "<integer>",
"status": "<integer>"
}'`


<h3>PropertyTechCase.Event.Api</h3>
The main purpose of PropertyTechCase.Event.Api is to manage events related to campaigns and propagate these events using RabbitMQ. But if san error occurs when application tried to publish event, app writes event to outbox table for the consistency.

*POST*

`curl --location '/api/Post/EventCampaigns' \
--header 'Content-Type: application/json' \
--data '{
"campaignCategoryId": "<long>",
"name": "<string>",
"budget": "<double>",
"spend": "<double>",
"type": "<integer>",
"status": "<integer>"
}'`

<h3>PropertyTechCase.Worker</h3>
It has two workers. One is for consuming data for some occasions. And the other on is the cleaning outbox table.