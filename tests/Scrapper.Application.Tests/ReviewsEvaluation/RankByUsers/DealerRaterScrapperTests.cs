using Scrapper.Application.ReviewsEvaluation.RankByUsers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Scrapper.Application.Tests.ReviewsEvaluation.RankByUsers
{
    public class EvaluationTests
    {
        [Fact]
        public async Task EmptyReview_Must_Have_Zero()
        {

            //Arrange
            var data = AsyncEnumerable.Empty<ReviewEntry>();

            //Act
            var result = data.RankByUsers();

            //Assert
            var count = await result.CountAsync();
            Assert.Equal(0, count);
        }

        [Fact]
        public async Task Rank_Must_Have_Expected_Entries()
        {
            //Arrange
            List<ReviewEntry> entries = new()
            {
                new("abra", "alef", "kadabra", "alakazan", 5),
                new("abra", "alef", "kadabra", "alakazan", 5),
                new("abra", "alef2", "kadabra", "alakazan", 4),
                new("abra", "alef3", "kadabra", "alakazan", 5),
                new("abra", "alef4", "kadabra", "alakazan", 4),
                new("abra", "alef5", "kadabra", "alakazan", 3),
                new("abra", "alef6", "kadabra", "alakazan", 5),
                new("abra", "alef7", "kadabra", "alakazan", 6),
                new("abra", "alef8", "kadabra", "alakazan", 9),
                new("abra", "alef", "kadabra", "alakazan", 4),
                new("abra", "alef3", "kadabra", "alakazan", 5),
            };

            //Act
            var result = entries.ToAsyncEnumerable().RankByUsers(take: 3);

            //Assert
            var count = await result.CountAsync();
            Assert.Equal(3, count);

            var serialized = await result.ToListAsync();

            RankedByUser expectedFirst = new("alef", 14);
            Assert.Equal(expectedFirst, serialized[0]);

            RankedByUser expectedSecond = new("alef3", 10);
            Assert.Equal(expectedSecond, serialized[1]);

            RankedByUser expectedThird = new("alef8", 9);
            Assert.Equal(expectedThird, serialized[2]);
        }
    }
}
