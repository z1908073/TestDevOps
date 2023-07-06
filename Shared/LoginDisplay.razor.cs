using Microsoft.AspNetCore.Components;

namespace TestDevOpsURL.Shared
{
    public partial class LoginDisplay
    {
        [Inject] public NavigationManager Navigation { get; set; }

        [Parameter] public string ReturnUrl { get; set; }

        protected override async Task OnInitializedAsync()
        {
            ReturnUrl = Navigation.ToBaseRelativePath(Navigation.Uri);
        }
    }
}