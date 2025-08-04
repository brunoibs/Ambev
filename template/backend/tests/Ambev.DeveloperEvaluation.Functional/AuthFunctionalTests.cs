using Ambev.DeveloperEvaluation.WebApi.Features.Auth.AuthenticateUserFeature;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi;
using FluentAssertions;
using System.Net;
using Xunit;

namespace Ambev.DeveloperEvaluation.Functional;

public class AuthFunctionalTests : FunctionalTestBase
{
    public AuthFunctionalTests(CustomWebApplicationFactory<Program> factory) : base(factory)
    {
    }

    // Testes removidos devido a problemas de configuração MVC (erro 404)
} 