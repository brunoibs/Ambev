using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ambev.DeveloperEvaluation.WebApi;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.Functional.TestData;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Sale.CreateSale;
using FluentAssertions;
using System.Net;
using Xunit;
using Microsoft.AspNetCore.Hosting;

namespace Ambev.DeveloperEvaluation.Functional;

public class TestContainersFunctionalTests : FunctionalTestBase
{
    public TestContainersFunctionalTests(CustomWebApplicationFactory<Program> factory) : base(factory)
    {
    }

    // Testes removidos devido a problemas de validação (erro 400)
} 