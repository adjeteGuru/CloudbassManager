# CloudbassManager
============

## Get Started

* Clone the repo locally.

* Please make you have the [FormatOnSave](https://marketplace.visualstudio.com/items?itemName=mynkow.FormatdocumentonSave) plugin installed.

* Run the migrations using Package Manager Console or the .NET Core CLI.


## Contributing

Please make sure that you leave detailed commit messages - note that we are using [Semantic Commit Messages](https://seesparkbox.com/foundry/semantic_commit_messages).

When creating a new branch to work on something, please also use the semantic commit messages as a guide for creating the branch - e.g. Add logging in.

```
feat/add-login
```

Note that hyphens replace spaces.

## Mac VSCode Development 

Install MSSQL on Docker following this [guide.](https://docs.microsoft.com/en-us/sql/linux/quickstart-install-connect-docker?view=sql-server-ver15&pivots=cs1-bash) 

Copy appsettings.json to appsettings.Development.json and change the connection string to (setting the password).

```
  "ConnectionStrings": {
   "CloudbassDb": "Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=GraphQLDB;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
```

## Running migrations

### Package Manager Console
```
Update-Database
```

### .Net Core CLI
Using poweshell

Make sure you have the dotnet-ef tools installed
```
dotnet tool install --global dotnet-ef
```
Update the database
```
dotnet ef database update --project .\CloudbassManager\
```

Add a migration
```
dotnet ef --startup-project .\CloudbassManager\ migrations add MIGRATIONNAME  --project .\Cloudbass.Database\
```

Drop the database - prompts for confirmation
```
dotnet ef database drop --project .\CloudbassManager  
```

## Testing

[Locally](http://localhost:5000/playground/)


### Settings

Make sure that ` "request.credentials": "include"` is set on the "Settings" page of the playground.

### Login

To log in, run the following mutation - note that the name and password might differ to below

```
mutation {
  login(input: {
    email: "dave@gmail.com"
    password: "Cloudba55"
  }) {
    user{
      id
      fullName
    }
    accessToken
  }
}
```


After running this mutation, make sure you add the returned `accessToken` to the HTTP Headers (at the bottom right of the webpage). E.g.

```
{
  "Authorization": "bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IkFkbWluIiwibmJmIjoxNTg1MjQ1MTc3LCJleHAiOjE1ODUyODgzNzcsImlhdCI6MTU4NTI0NTE3N30.iDJ2wkR-XdmvrN3rt-o0fBHg2fkLUrW2xQrb9AgFyLs"
}
```


### Queries 

Should look like this

```
query {
  users {
    id
    fullName
    email
    active
    createdAt
    updatedAt
  }
}
```

The will return something like this...

```
{
  "data": {
    "users": [
      {
        "id":"e1a045e2-2850-433f-7e71-08d8673394f4",
        "fullName": "Dave Cooper",
        "email": null,
        "active": false,
        "createdAt": "2020-03-26T18:07:49.296Z",
        "updatedAt": "2020-03-26T18:07:49.296Z"
      }
    ]
  }
}
```

### Mutations

Will look something like this

```
mutation {
  createUser(input: {
    fullName: "John Smith",
    email: "john.smith@gmail.com"
    password: "Cloudba55"
    active: true
  }){
    user{
      id
      fullName
      email
      createdAt
      updatedAt
    }
  }
}
```

and return 

```
{
  "data": {
    "createUser": {
      "user": {
        "id":"f2b1e5c4-5937-4016-309d-08d86733a4bc",
        "fullName": "John Smith",
        "email": "john.smith@gmail.com",
        "createdAt": "2020-03-26T18:49:50.694Z",
        "updatedAt": "2020-03-26T18:49:50.694Z"
      }
    }
  }
}
```

### Subcription

When you run a subcription like the one below, it will run async and let you know when that event has fired. 

For example, this subcription  

```
subscription {
  onCreateUser{
    id
    fullName
    email
    active
    createdAt
    updatedAt
  }
}
```

will return 

```
{
  "data": {
    "onCreateUser": {
      "id":"f2b1e5c4-5937-4016-309d-08d86733a4bc",
      "fullName": "John Smith",
      "email": "john.smith@gmail.com",
      "active": true,
      "createdAt": "2020-03-26T18:49:50.694Z",
      "updatedAt": "2020-03-26T18:49:50.694Z"
    }
  }
}
```

When the above mutation is ran.
