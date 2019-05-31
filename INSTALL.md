dotnet ef migrations add <INSERTNAME> --project Cakelist.Infrastructure --startup-project Cakelist.Api


dotnet ef database update --startup-project Cakelist.Api

Add UserSecrets