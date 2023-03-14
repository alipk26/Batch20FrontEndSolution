using Microsoft.AspNetCore.Components;
using Realta.Domain.RequestFeatures;
using Realta.Frontend.Features;

namespace Realta.Frontend.Components;

public partial class Paging
{
    [Parameter] public MetaData MetaData { get; set; }

    [Parameter] public int Spread { get; set; }

    [Parameter] public EventCallback<int> SelectedPage { get; set; }
    private List<PagingLink> _links;

    protected override void OnParametersSet()
    {
        CreatePaginationLinks();
    }

    private void CreatePaginationLinks()
    {
        _links = new List<PagingLink>();

        for (var i = 1; i <= MetaData.TotalPages; i++) {
            if (i >= MetaData.CurrentPage - Spread && i <= MetaData.CurrentPage + Spread) {
                _links.Add(new PagingLink(i, true, i.ToString()) { Active = MetaData.CurrentPage == i });
            }
        }

        _links.Add(new PagingLink(MetaData.CurrentPage + 1, MetaData.HasNext, "�"));
    }

    private async Task OnSelectedPage(PagingLink link)
    {
        if (link.Page == MetaData.CurrentPage || !link.Enabled)
            return;

        MetaData.CurrentPage = link.Page;
        await SelectedPage.InvokeAsync(link.Page);
    }
}