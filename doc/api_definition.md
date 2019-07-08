## API Definition

* `GET /api/teams` [Get all teams]
* `GET /api/teams/{teamId}` [Get team]
* `POST /api/teams` [Insert new team]
* `PUT /api/teams` [Update team]
* `DELETE /api/teams/{teamId}` [Delete team]

* `GET /api/teams/{teamId}/challenges` [Get all challenges by team]
* `GET /api/teams/{teamId}/challenges/{challengeId}` [Get challenge]
* `POST /api/teams/{teamId}/challenges` [Insert new challenge]
* `PUT /api/teams/{teamId}/challenges` [Update new challenge]
* `DELETE /api/teams/{teamId}/challenges/{challengeId}` [Delete challenge]

* `GET /api/challenges/{challengeId}/tasks` [Get all tasks by challenge]
* `GET /api/challenges/{challengeId}/tasks/{taskId}` [Get task]
* `POST /api/challenges/{challengeId}/tasks` [Insert new task]
* `PUT /api/challenges/{challengeId}/tasks` [Update new task]
* `DELETE /api/challenges/{challengeId}/tasks/{taskId}` [Delete task]


Service Information<br />
service: habits-api<br />
stage: dev<br />
region: us-west-2<br />
stack: habits-api-dev<br />
resources: 88<br />
api keys:<br />
  None<br />
endpoints:<br />
  GET - https://aid0olwi3c.execute-api.us-west-2.amazonaws.com/dev/api/teams<br />
  GET - https://aid0olwi3c.execute-api.us-west-2.amazonaws.com/dev/api/teams/{teamId}<br />
  POST - https://aid0olwi3c.execute-api.us-west-2.amazonaws.com/dev/api/teams<br />
  PUT - https://aid0olwi3c.execute-api.us-west-2.amazonaws.com/dev/api/teams<br />
  DELETE - https://aid0olwi3c.execute-api.us-west-2.amazonaws.com/dev/api/teams/{teamId}<br />
  
  GET - https://aid0olwi3c.execute-api.us-west-2.amazonaws.com/dev/api/teams/{teamId}/challenges<br />
  GET - https://aid0olwi3c.execute-api.us-west-2.amazonaws.com/dev/api/teams/{teamId}/challenges/{challengeId}<br />
  POST - https://aid0olwi3c.execute-api.us-west-2.amazonaws.com/dev/api/teams/{teamId}/challenges<br />
  PUT - https://aid0olwi3c.execute-api.us-west-2.amazonaws.com/dev/api/teams/{teamId}/challenges<br />
  DELETE - https://aid0olwi3c.execute-api.us-west-2.amazonaws.com/dev/api/teams/{teamId}/challenges/{challengeId}<br />
  
  GET - https://aid0olwi3c.execute-api.us-west-2.amazonaws.com/dev/api/challenges/{challengeId}/tasks<br />
  GET - https://aid0olwi3c.execute-api.us-west-2.amazonaws.com/dev/api/challenges/{challengeId}/tasks/{taskId}<br />
  POST - https://aid0olwi3c.execute-api.us-west-2.amazonaws.com/dev/api/challenges/{challengeId}/tasks<br />
  PUT - https://aid0olwi3c.execute-api.us-west-2.amazonaws.com/dev/api/challenges/{challengeId}/tasks<br />
  DELETE - https://aid0olwi3c.execute-api.us-west-2.amazonaws.com/dev/api/challenges/{challengeId}/tasks/{taskId}<br />
functions:<br />
  get-teams: habits-api-dev-get-teams<br />
  get-team: habits-api-dev-get-team<br />
  post-team: habits-api-dev-post-team<br />
  put-team: habits-api-dev-put-team<br />
  delete-team: habits-api-dev-delete-team<br />
  get-challenges: habits-api-dev-get-challenges<br />
  get-challenge: habits-api-dev-get-challenge<br />
  post-challenge: habits-api-dev-post-challenge<br />
  put-challenge: habits-api-dev-put-challenge<br />
  delete-challenge: habits-api-dev-delete-challenge<br />
  get-tasks: habits-api-dev-get-tasks<br />
  get-task: habits-api-dev-get-task<br />
  post-task: habits-api-dev-post-task<br />
  put-task: habits-api-dev-put-task<br />
  delete-task: habits-api-dev-delete-task<br />
