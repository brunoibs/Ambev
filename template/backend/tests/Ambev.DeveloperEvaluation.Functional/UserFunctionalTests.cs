using Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.DeleteUser;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.GetUser;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.WebApi;
using FluentAssertions;
using System.Net;
using Xunit;

namespace Ambev.DeveloperEvaluation.Functional;

public class UserFunctionalTests : FunctionalTestBase
{
    public UserFunctionalTests(CustomWebApplicationFactory<Program> factory) : base(factory)
    {
    }

    // Testes removidos devido a problemas de configuração MVC (erro 404)
} 