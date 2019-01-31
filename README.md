# 🍰 Cakelist
Simple project to research and test Domain Driven Design (DDD) and Clean Architecture i ASP.NET Core with hosting in Azure.

See issues for progress and ideas.

## Business / Domain

The basic business domain / problem this application have to solve is focused on who and when to give cake (or similar) in a team.

### User stories

- [ ] As a user in a team, I want to create a cake request item, so I can ensure that my team member(s) give cakes in respect of the cake manifest/law
- [ ] As a user in a team, I want to see a list of cake request items (the cakelist), so I can see which team members needs to give cake.
- [ ] As a user in a team, I want to vote on a cake request items, so these can be "confirmed".
- [ ] As a user in a team, I want to be able to login with my Office 365/Microsoft account, so it is easy for me to login.
- [ ] As a user in a team, I want a notification, when somebody assign me a cake request item, so I know I have to make cake.
- [ ] As a system, I want to randomly calculate how many votes is needed to make a cake request item confirmed, so the cakelist is fun to use.
- [ ] As a system, I want to clean up cake request items calculated on the likelihood of the chances for the individual request to be delivered, to minimize the amount of undelivered cake requests items.

### Business language
- 👪 Team
- 👱 User
- 🍰 Cake request item
- 🚦 Cake request item status (Not confirmed, confirmed, delivered, canceled)
- 📝 Cake request item reason
- 🗳️ Vote on cake request item
- 🔔 Notification to user

### Business logic/rules

- [ ] A user can't vote multiple times on the same cake request item
- [ ] A user can't vote on his/her own cake request item (created by)
- [ ] A user can't vote on a cake request items assign to him/her (assigned to) 

## Tech
- ASP.NET 2.2
- .NET 2.2
- .Net Standard