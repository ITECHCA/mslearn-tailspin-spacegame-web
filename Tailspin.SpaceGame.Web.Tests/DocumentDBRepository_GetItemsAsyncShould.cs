using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NUnit.Framework;
using TailSpin.SpaceGame.Web;
using TailSpin.SpaceGame.Web.Models;

namespace Tests
{
    public class DocumentDBRepository_GetItemsAsyncShould
    {
        private IDocumentDBRepository<Score> _scoreRepository;

        [SetUp]
        public void Setup()
        {
            using (Stream scoresData = typeof(IDocumentDBRepository<Score>)
                .Assembly
                .GetManifestResourceStream("Tailspin.SpaceGame.Web.SampleData.scores.json"))
            {
                _scoreRepository = new LocalDocumentDBRepository<Score>(scoresData);
            }
        }

        [TestCase("Milky Way")]
        [TestCase("Andromeda")]
        [TestCase("Pinwheel")]
        [TestCase("NGC 1300")]
        [TestCase("Messier 82")]
        public void FetchOnlyRequestedGameRegion(string gameRegion)
        {
            const int PAGE = 0; // take the first page of results
            const int MAX_RESULTS = 10; // sample up to 10 results

            // Form the query predicate.
            // This expression selects all scores for the provided game region.
            Expression<Func<Score, bool>> queryPredicate = score => (score.GameRegion == gameRegion);

            // Fetch the scores.
        public Task<IEnumerable<T>> GetItemsAsync(
    Expression<Func<T, bool>> queryPredicate,
    Expression<Func<T, int>> orderDescendingPredicate,
    int page = 1, int pageSize = 10
)
{
    var result = _items.AsQueryable()
        .Where(queryPredicate) // filter
        .OrderByDescending(orderDescendingPredicate) // sort
        .Skip(page * pageSize) // find page
        .Take(pageSize) // take items
        .AsEnumerable(); // make enumeratable

    return Task<IEnumerable<T>>.FromResult(result);
}
            // Verify that each score's game region matches the provided game region.
            Assert.That(scores, Is.All.Matches<Score>(score => score.GameRegion == gameRegion));
        }

        [TestCase(0, ExpectedResult = 0)]
        [TestCase(1, ExpectedResult = 1)]
        [TestCase(10, ExpectedResult = 10)]
        public int ReturnRequestedCount(int count)
        {
            const int PAGE = 0; // take the first page of results

            // Fetch the scores.
          public Task<IEnumerable<T>> GetItemsAsync(
    Expression<Func<T, bool>> queryPredicate,
    Expression<Func<T, int>> orderDescendingPredicate,
    int page = 1, int pageSize = 10
)
{
    var result = _items.AsQueryable()
        .Where(queryPredicate) // filter
        .OrderByDescending(orderDescendingPredicate) // sort
        .Skip(page * pageSize) // find page
        .Take(pageSize) // take items
        .AsEnumerable(); // make enumeratable

    return Task<IEnumerable<T>>.FromResult(result);
}

            // Verify that we received the specified number of items.
            return scores.Count();
        }
    }
}