using System;
using Xunit;
using Cakelist.Business.Entities;
using Cakelist.Business.Entities.CakelistRequestAggregate;
using System.Linq;

namespace Cakelist.UnitTests
{
    public class CakeRequestTests
    {

        private readonly User _fakeCreator = new User {
            Id = "1",
            FirstName = "Tony",
            LastName = "Stark",
            Email = "tony@avengers.com"
        };

        private readonly User _fakeAssignee = new User {
            Id = "2",
            FirstName = "Steven",
            LastName = "Rogers",
            Email = "steven@avengers.com"
        };

        private readonly User _fakeVoter = new User {
            Id = "3",
            FirstName = "Bruce",
            LastName = "Banner",
            Email = "bruce@avengers.com"
        };


        [Fact]
        public void AddRequest_ShouldReturnRequest()
        {
            // Arrange and Act
            var cakeRequest = new CakeRequest(_fakeCreator, _fakeAssignee, "New shield..");

            // Assert
            Assert.Equal(typeof(CakeRequest), cakeRequest.GetType());            
        }

        [Fact]
        public void AddVote_ShouldReturnSameVoter()
        {
            //Arrange
            var cakeRequest = new CakeRequest(_fakeCreator, _fakeAssignee, "New shield..");

            // Act
            cakeRequest.AddVote(_fakeVoter);

            var voterId = cakeRequest.Votes.First().CreatedBy.Id;

            // Assert
            Assert.Equal(_fakeVoter.Id, voterId);
        }

        [Fact]
        public void AddVoteOnRequest_WithCreator_ShouldThrowException()
        {
            //Arrange
            var cakeRequest = new CakeRequest(_fakeCreator, _fakeAssignee, "New shield..");

            // Act and assert
            Assert.Throws<VoterIsCreatorException>(() => cakeRequest.AddVote(_fakeCreator));
        }

        [Fact]
        public void AddVoteOnRequest_WithAsignee_ShouldThrowException()
        {
            //Arrange
            var cakeRequest = new CakeRequest(_fakeCreator, _fakeAssignee, "New shield..");

            // Act and assert
            Assert.Throws<VoterIsAsigneeException>(() => cakeRequest.AddVote(_fakeAssignee));
        }

        [Fact]
        public void AddMultipleVoteOnRequest_WithSameUser_ShouldThrowException()
        {
            //Arrange
            var cakeRequest = new CakeRequest(_fakeCreator, _fakeAssignee, "New shield..");

            // Act - First vote should be fine
            cakeRequest.AddVote(_fakeVoter);

            // Act and assert
            Assert.Throws<VoterHasVotedException>(() => cakeRequest.AddVote(_fakeVoter));
        }
    }
}
