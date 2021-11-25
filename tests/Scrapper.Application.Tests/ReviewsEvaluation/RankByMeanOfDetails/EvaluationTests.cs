using Scrapper.Application.ReviewsEvaluation.RankByUsers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using static Scrapper.Application.ReviewsEvaluation.RankByUsers.RankByMeanOfDetailsEval;

namespace Scrapper.Application.Tests.ReviewsEvaluation.RankByMeanOfDetails
{
    public class EvaluationTests
    {
        [Fact]
        public async Task EmptyReview_Must_Have_Zero()
        {

            //Arrange
            var data = AsyncEnumerable.Empty<ReviewEntry>();

            //Act
            var result = data.RankByMeanOfDetails();

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
                BuildReview("title1", "alef", 5, 5),
                BuildReview("title2", "alef1", 5, 4),
                BuildReview("title3", "alef2", 5, 4.9m),
                BuildReview("title4", "alef3", 5, 5),
                BuildReview("title5", "alef4", 5, 3),
                BuildReview("title6", "alef5", 5, 2),
                BuildReview("title7", "alef6", 5, 1),
                BuildReview("title8", "alef7", 5, 0),
                BuildReview("title9", "alef8", 5, 5),
                BuildReview("title10", "alef9", 5, 5),
            };

            //Act
            var result = entries.ToAsyncEnumerable().RankByMeanOfDetails(take: 3);

            //Assert
            var count = await result.CountAsync();
            Assert.Equal(3, count);

            var serialized = await result.ToListAsync();

            var expectedFirst = entries[7];
            Assert.Equal(expectedFirst, serialized[0].Review);
            Assert.Equal(5, serialized[0].Difference);

            var expectedSecond = entries[6];
            Assert.Equal(expectedSecond, serialized[1].Review);
            Assert.Equal(4, serialized[1].Difference);

            var expectedThird = entries[5];
            Assert.Equal(expectedThird, serialized[2].Review);
            Assert.Equal(3, serialized[2].Difference);
        }

        private static ReviewEntry BuildReview(string title, string user, int rating, decimal detailRatingMean)
        {
            var rev = new ReviewEntry("abra", user, title, "alakazan", rating);

            rev.AddDetail("mock", detailRatingMean);

            return rev;
        }
    }
}
