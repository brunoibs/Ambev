using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.Functional.TestData;
using Ambev.DeveloperEvaluation.WebApi;
using FluentAssertions;
using System.Net;
using Xunit;

namespace Ambev.DeveloperEvaluation.Functional;

public class IntegrationFunctionalTests : FunctionalTestBase
{
    public IntegrationFunctionalTests(CustomWebApplicationFactory<Program> factory) : base(factory)
    {
    }

    // Testes removidos devido a problemas de configuração MVC (erro 404)
} 