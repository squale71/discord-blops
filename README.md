# discord-blops
Discord Bot that Provides Stats for COD: BLOPS IIII

This application uses MongoDB as it's database backend. You will also need to create a discord application and supply local configuration with your discord API key. Any keys/connection strings that have ever been committed to this repo in its early days are no longer valid, so nice try :).

Once you pull the project, you'll need to have ready your API key and MongoDB connection string so you can run it locally. This application uses the .NET Core User Secrets API to store a json file locally on your machine containing any secrets you don't want committed to the repo. Obviously you'll not want to do this for your production application, as you'll want to use whatever secrets manager you have available to you there. You can look up how to get started creating a Discord app in their documentation, and read up on MongoDB as well, as this will assume you have basic knowledge of both.

Do the following to get going:

Open Powershell and 'cd' to your project directory:

Run the following commands:
dotnet user-secrets set DiscordApiKey "{your-key}"
dotnet user-secrets set DatabaseConnectionString "{your-connection-string}"

