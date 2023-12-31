﻿using Application.Common.Models;
using Application.UseCases.Category.Queries.GetCategory;
using Bogus;
using Domain.Entities;
using Newtonsoft.Json;

namespace IntegrationTest.Category.Queries;

[TestClass]
public class GetCategoryTest : Testing
{
    [TestInitialize]
    public async Task TestInitialize()
    {
        var categoryEntity = new Faker<CategoryEntity>()
                               .CustomInstantiator(f => new CategoryEntity(f.Commerce.Categories(1)[0]))
                               .RuleFor(x => x.Slug, f => f.Commerce.Categories(1)[0])
                               .Generate();

        await AddAsync(categoryEntity);

        categoryEntity = new Faker<CategoryEntity>()
                               .CustomInstantiator(f => new CategoryEntity(f.Commerce.Categories(1)[0]))
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
    public async Task ShouldReturnPaginatedListWithCategoryUseCase()
    {
        var query = new GetCategoryWithPaginationQuery() { PageSize = 9999, PageNumber = 1 };
        var result = await SendAsync(query);
        Assert.IsNotNull(result);
        Assert.IsTrue(result.Items.Count > 0);
    }

    [TestMethod]
    public async Task ShouldReturnPaginatedListWithCategoryController()
    {
        using var client = await CreateHttpClient();
        var response = await client.GetAsync("/api/category?PageSize=9999&PageNumber=1");
        Assert.IsTrue(response.IsSuccessStatusCode);

        var content = await response.Content.ReadAsStringAsync();
        Assert.IsNotNull(content);

        var result = JsonConvert.DeserializeObject<PaginatedList<CategoryEntity>>(content);
        Assert.IsNotNull(result);
        Assert.IsTrue(result.Items.Count > 0);
    }
}
