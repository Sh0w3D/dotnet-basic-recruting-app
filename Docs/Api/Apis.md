# Application APIs

- [MatchDataManager APIs](#matchdatamanager-api)
    - [Location](#location)
        - [Get all locations](#get-locations)
        - [Get location](#get-single-location)
        - [Add location](#create-location)
        - [Update location](#update-location)
        - [Delete location](#delete-location)
    - [Team](#team)
        - [Get all teams](#get-teams)
        - [Get team](#get-single-team)
        - [Add team](#create-team)
        - [Update team](#update-team)
        - [Delete team](#delete-team)

#### Location
---
#### Get locations

Request:
```js
GET {{host}}/api/location
```

Response:
```js
200 OK
```
```json
[
    {
        "id": "dd374e9d-bce3-4d12-bf8a-157e210c532b",
        "name": "Test Name",
        "city": "Test City"
    },
    {
        "id": "dd374e9d-bce3-4d12-bf8a-157e210c532b",
        "name": "Test Name",
        "city": "Test City"
    },
    {
        "id": "dd374e9d-bce3-4d12-bf8a-157e210c532b",
        "name": "Test Name",
        "city": "Test City"
    },
]
```

#### Get single location

Request:
```js
GET {{host}}/api/location/{{id}}
```

Response:
```js
200 OK
```
```json
{
    "id": "dd374e9d-bce3-4d12-bf8a-157e210c532b",
    "name": "Test Name",
    "city": "Test City"
},
```

#### Create location

Request:

```js
POST {{host}}/api/location
```
Request body:
```json
{
    "name": "Test Name",
    "city": "Test City"
},
```

Response:
```js
201 Created
```
```json
{
    "id": "dd374e9d-bce3-4d12-bf8a-157e210c532b",
    "name": "Test Name",
    "city": "Test City"
},
```

#### Update location

Request:

```js
PUT {{host}}/api/location/{{id}}
```
Request body:
```json
{
    "name": "Test Name",
    "city": "Test City"
},
```

Response:
```js
204 No content
```

#### Delete location

Request:
```js
DELETE {{host}}/api/{{id}}
```

Response:
```js
204 No Content
```


#### Team
---
#### Get teams

Request:
```js
GET {{host}}/api/team
```

Response:
```js
200 OK
```
```json
[
    {
        "id": "dd374e9d-bce3-4d12-bf8a-157e210c532b",
        "name": "Test Name",
        "coachName": "Test Coach"
    },
    {
        "id": "dd374e9d-bce3-4d12-bf8a-157e210c532b",
        "name": "Test Name",
        "coachName": "Test Coach"
    },
    {
        "id": "dd374e9d-bce3-4d12-bf8a-157e210c532b",
        "name": "Test Name",
        "coachName": "Test Coach"
    },
]
```

#### Get single team

Request:
```js
GET {{host}}/api/team/{{id}}
```

Response:
```js
200 OK
```
```json
{
    "id": "dd374e9d-bce3-4d12-bf8a-157e210c532b",
    "name": "Test Name",
    "coachName": "Test Coach"
},
```

#### Create team

Request:

```js
POST {{host}}/api/team
```
Request body:
```json
{
    "name": "Test Name",
    "coachName": "Test Coach" // can be null
},
```

Response:
```js
201 Created
```
```json
{
    "id": "dd374e9d-bce3-4d12-bf8a-157e210c532b",
    "name": "Test Name",
    "coachName": "Test Coach"
},
```

#### Update team

Request:

```js
PUT {{host}}/api/team/{{id}}
```
Request body:
```json
{
    "name": "Test Name",
    "coachName": "Test Coach"
},
```

Response:
```js
204 No content
```
#### Delete team

Request:
```js
DELETE {{host}}/api/team/{{id}}
```

Response:
```js
204 No Content
```