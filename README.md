# GraphQL-API
GraphQL API with .NET 7 and Hot Chocolate

## POST: http://localhost:5213/graphql/

### Queries:

Get Platforms
```
query{
    platform{
        id
        name
    }
}
```

Get Platforms and Commands
```
query{
    platform
    {
        id
        name
        commands{
            howTo
            commandLine
        }
    }
}
```
### Mutations:

Add Platform
```
mutation{
    addPlatform(input: 
    {
        name: "Ubuntu"
    })
    {
        platform{
            id
            name
        }
    }
}
```

Add Command
```
mutation{
    addCommand(input: {
        howTo: "Perform directory listening"
        commandLine: "ls"
        platformId: 1
    })
    {
        command{
            id
            howTo
            commandLine
            platform{
                name
            }
        }
    }
}
```
