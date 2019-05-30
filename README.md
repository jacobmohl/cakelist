# 🍰 Cakelist
Simple project to research and test Domain Driven Design (DDD) and Clean Architecture i ASP.NET Core with hosting in Azure.

See issues for progress and ideas.

[![Build Status](https://dev.azure.com/jacobmohl-dk/Cakelist/_apis/build/status/jacobmohl.Cakelist?branchName=master)](https://dev.azure.com/jacobmohl-dk/Cakelist/_build/latest?definitionId=1&branchName=master)

## Business / Domain

The basic business domain / problem this application have to solve is focused on who and when to give cake (or similar) in a team.

### User stories

- [ ] #S1 As a user in a team, I want to create a cake request, so I can ensure that my team member(s) give cakes in respect of the cake manifest/law
- [ ] #S2 As a user in a team, I want to see a list of cake requests (the cakelist), so I can see which team members needs to give cake.
- [ ] #S3 As a user in a team, I want to vote on a cake request, so these can be "confirmed".
- [ ] #S4 As a user in a team, I want to be able to login with my Office 365/Microsoft account, so it is easy for me to login.
- [ ] #S5 As a user in a team, I want a notification, when somebody assign me a cake request, so I know I have to make cake.
- [ ] #S6 As a system, I want to randomly calculate how many votes is needed to make a cake request confirmed, so the cakelist is fun to use.
- [ ] #S7 As a system, I want to clean up cake request items calculated on the likelihood of the chances for the individual request to be delivered, to minimize the amount of undelivered cake requests items.

### Business language
👪 Team : Where multiple users can be added do.
👱 User : Who can assign og be assigned cake requests.
🍰 Cake request : A request to bake/buy cake for the team for a reason.
🚦 Cake request status (Not confirmed, confirmed, delivered, canceled)
📝 Cake request reason : The reason for the cake request.
🗳️ Vote on cake request : Users in a team can vote on reqeusts for make it eligible 
🔔 Notification to user : Users can get several notifications (ex. when he/she is assigned a request, new vote ect.)

### Business logic requirement

- [X] #L1 A user can't vote multiple times on the same cake request
- [X] #L2 A user can't vote on his/her own cake request (created by)
- [X] #L3 A user can't vote on a cake requests assign to him/her (assigned to) 

## Tech
- ASP.NET Core 2.2
- .NET Core 2.2
- .Net Standard

### Dependencires
None (yet)