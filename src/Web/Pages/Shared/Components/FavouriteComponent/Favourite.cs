﻿using Ardalis.GuardClauses;
using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.eShopWeb.Infrastructure.Identity;
using Microsoft.eShopWeb.Web.Interfaces;
using Microsoft.eShopWeb.Web.ViewModels;
using System;
using System.Threading.Tasks;
 


namespace Microsoft.eShopWeb.Web.Pages.Shared.Components.FavouriteComponent;

public class Favourite : ViewComponent
{
    private readonly IFavouritesViewModelService _favouriteService;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var vm = new FavouriteComponentViewModel
        {
            // FavourieItemCount = 3 // TODO: do right!!! await CountTotalBasketItems()
            FavourieItemCount = await CountTotalFavouriteItems()
        };
        return View(vm);
    }

    public Favourite(IFavouritesViewModelService basketService,
                    SignInManager<ApplicationUser> signInManager)
    {
        _favouriteService = basketService;
        _signInManager = signInManager;
    }
  
    private async Task<int> CountTotalFavouriteItems()
    {
        if (_signInManager.IsSignedIn(HttpContext.User))
        {
            Guard.Against.Null(User?.Identity?.Name, nameof(User.Identity.Name));
            return await _favouriteService.CountTotalFavouriteItems(User.Identity.Name);
        }

        string? anonymousId = GetAnnonymousIdFromCookie();
        if (anonymousId == null)
            return 0;

        return await _favouriteService.CountTotalFavouriteItems(anonymousId);
    }
    private string? GetAnnonymousIdFromCookie()
    {
        if (Request.Cookies.ContainsKey(Constants.BASKET_COOKIENAME))
        {
            var id = Request.Cookies[Constants.BASKET_COOKIENAME];

            if (Guid.TryParse(id, out var _))
            {
                return id;
            }
        }
        return null;
    }

    }

