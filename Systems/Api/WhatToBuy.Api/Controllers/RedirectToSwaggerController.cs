using Microsoft.AspNetCore.Mvc;

namespace WhatToBuy.Api.Controllers;

/// <summary>
/// Controller to redirect from domain name to swagger
/// </summary>
public class RedirectToSwaggerController : ControllerBase
{
    /// <summary>
    /// Redirects from domain name to swagger
    /// </summary>
    [HttpGet("")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public RedirectResult RedirectToSwaggerUi()
    {
        return Redirect("/api/index.html");
    }
}