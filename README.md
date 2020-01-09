# TweetAnalytics
This application gathers the following statistics from Twitter's [sample realtime API](https://developer.twitter.com/en/docs/tweets/sample-realtime/api-reference/get-statuses-sample) 

## Configuration 
### Twitter Credentials
[appsettings.json](src/appsettings.json) should be edited to include your Twitter API credentials.
```json
"twitter": {
    "consumerKey": "",
    "consumerKeySecret": "",
    "accessToken": "",
    "accessTokenSecret": ""
}
```

### Photo domains
By default, this application recognizes pic.twitter.com, pbs.twimg.com, www.instagram.com as image domains. This is editable through the appsettings as well.

## Prerequisites 
- [.NET Core 3](https://dotnet.microsoft.com/download)

## Running the project
```
dotnet run TweetAnalytics.csproj
```

## Running the tests
```
dotnet test
```