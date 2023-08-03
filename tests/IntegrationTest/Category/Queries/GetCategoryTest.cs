using Application.UseCases.Category.Queries.GetCategory;
using Bogus;
using Domain.Entities;

namespace IntegrationTest.Category.Queries;

[TestClass]
public class GetCategoryTest : Testing
{
    [TestInitialize]
    public async Task TestInitialize()
    {
        var categoryEntity = new Faker<CategoryEntity>()
                         .RuleFor(x => x.Name, f => f.Commerce.Categories(1)[0])
                         .RuleFor(x => x.Slug, f => f.Commerce.Categories(1)[0])
                         .Generate();

        await AddAsync(categoryEntity);

        categoryEntity = new Faker<CategoryEntity>()
                               .RuleFor(x => x.Name, f => f.Commerce.Categories(1)[0])
                               .RuleFor(x => x.Slug, f => f.Commerce.Categories(1)[0])
                               .Generate();

        await AddAsync(categoryEntity);
    }

    [TestMethod]
    public async Task ShouldReturnAllCategory()
    {
        var query = new GetCategoryQuery();
        var result = await SendAsync(query);

        Assert.IsNotNull(result);
        Assert.IsTrue(result.Count > 0);
    }


    [TestMethod]
    public async Task ShouldReturnPaginatedListWithCategory()
    {
        var query = new GetCategoryWithPaginationQuery() { PageSize = 9999, PageNumber = 1 };
        var result = await SendAsync(query);
        Assert.IsNotNull(result);
        Assert.IsTrue(result.Items.Count > 0);
    }
}
