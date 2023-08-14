using Application.UseCases.Tag.Queries.GetTag;
using Bogus;
using Domain.Entities;

namespace IntegrationTest.Tag.Queries;

[TestClass]
public class GetTagTest : Testing
{
    [TestInitialize]
    public async Task TestInitialize()
    {
        var tagEntity = new Faker<TagEntity>()
                         .CustomInstantiator(f => new TagEntity(f.Commerce.Categories(1)[0]))
                         .RuleFor(x => x.Slug, f => f.Commerce.Categories(1)[0])
                         .Generate();

        await AddAsync(tagEntity);

        tagEntity = new Faker<TagEntity>()
                               .CustomInstantiator(f => new TagEntity(f.Commerce.Categories(1)[0]))
                               .RuleFor(x => x.Slug, f => f.Commerce.Categories(1)[0])
                               .Generate();

        await AddAsync(tagEntity);
    }

    [TestMethod]
    public async Task ShouldReturnAllTag()
    {
        var query = new GetTagQuery();
        var result = await SendAsync(query);

        Assert.IsNotNull(result);
        Assert.IsTrue(result.Count > 0);
    }


    [TestMethod]
    public async Task ShouldReturnPaginatedListWithTag()
    {
        var query = new GetTagWithPaginationQuery() { PageSize = 9999, PageNumber = 1 };
        var result = await SendAsync(query);
        Assert.IsNotNull(result);
        Assert.IsTrue(result.Items.Count > 0);
    }
}
