## All of this must be documented on Swagger, for now, this document is ok :D

GET /api/teams Get all teams
GET /api/teams/{teamId} Get team
POST /api/teams Insert new team
PUT /api/teams Update team
DELETE /api/teams/{teamId} Delete team

GET /api/teams/{teamId}/challenges Get all challenges by team
GET /api/teams/{teamId}/challenges/{challengeId} Get challenge
POST /api/teams/{teamId}/challenges Insert new challenge
PUT /api/teams/{teamId}/challenges Update new challenge
DELETE /api/teams/{teamId}/challenges/{challengeId} Delete challenge

GET /api/challenges/{challengeId}/tasks Get all tasks by challenge
GET /api/challenges/{challengeId}/tasks/{taskId} Get task
POST /api/challenges/{challengeId}/tasks Insert new task
PUT /api/challenges/{challengeId}/tasks Update new task
DELETE /api/challenges/{challengeId}/tasks/{taskId} Delete task


Service Information
service: habits-api
stage: dev
region: us-west-2
stack: habits-api-dev
resources: 88
api keys:
  None
endpoints:
  GET - https://aid0olwi3c.execute-api.us-west-2.amazonaws.com/dev/api/teams
  GET - https://aid0olwi3c.execute-api.us-west-2.amazonaws.com/dev/api/teams/{teamId}
  POST - https://aid0olwi3c.execute-api.us-west-2.amazonaws.com/dev/api/teams
  PUT - https://aid0olwi3c.execute-api.us-west-2.amazonaws.com/dev/api/teams
  DELETE - https://aid0olwi3c.execute-api.us-west-2.amazonaws.com/dev/api/teams/{teamId}
  
  GET - https://aid0olwi3c.execute-api.us-west-2.amazonaws.com/dev/api/teams/{teamId}/challenges
  GET - https://aid0olwi3c.execute-api.us-west-2.amazonaws.com/dev/api/teams/{teamId}/challenges/{challengeId}
  POST - https://aid0olwi3c.execute-api.us-west-2.amazonaws.com/dev/api/teams/{teamId}/challenges
  PUT - https://aid0olwi3c.execute-api.us-west-2.amazonaws.com/dev/api/teams/{teamId}/challenges
  DELETE - https://aid0olwi3c.execute-api.us-west-2.amazonaws.com/dev/api/teams/{teamId}/challenges/{challengeId}
  
  GET - https://aid0olwi3c.execute-api.us-west-2.amazonaws.com/dev/api/challenges/{challengeId}/tasks
  GET - https://aid0olwi3c.execute-api.us-west-2.amazonaws.com/dev/api/challenges/{challengeId}/tasks/{taskId}
  POST - https://aid0olwi3c.execute-api.us-west-2.amazonaws.com/dev/api/challenges/{challengeId}/tasks
  PUT - https://aid0olwi3c.execute-api.us-west-2.amazonaws.com/dev/api/challenges/{challengeId}/tasks
  DELETE - https://aid0olwi3c.execute-api.us-west-2.amazonaws.com/dev/api/challenges/{challengeId}/tasks/{taskId}
functions:
  get-teams: habits-api-dev-get-teams
  get-team: habits-api-dev-get-team
  post-team: habits-api-dev-post-team
  put-team: habits-api-dev-put-team
  delete-team: habits-api-dev-delete-team
  get-challenges: habits-api-dev-get-challenges
  get-challenge: habits-api-dev-get-challenge
  post-challenge: habits-api-dev-post-challenge
  put-challenge: habits-api-dev-put-challenge
  delete-challenge: habits-api-dev-delete-challenge
  get-tasks: habits-api-dev-get-tasks
  get-task: habits-api-dev-get-task
  post-task: habits-api-dev-post-task
  put-task: habits-api-dev-put-task
  delete-task: habits-api-dev-delete-task
