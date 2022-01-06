using Fithub.UI.Interfaces;
using Fithub.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;

namespace Fithub.UI.Helpers
{
    public class AppRouteView : RouteView
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IStateContainer Container { get; set; }

        protected override void Render(RenderTreeBuilder builder)
        {
            var authorize = Attribute.GetCustomAttribute(RouteData.PageType, typeof(AuthorizeAttribute)) != null;
            var user = Container.GetItem<User>("user");

            if (authorize && user == null)
                NavigationManager.NavigateTo("login");
            else
                base.Render(builder);
        }
    }
}
