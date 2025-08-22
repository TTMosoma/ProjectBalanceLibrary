\## Known Issue (documented during timebox)



\*\*Symptom:\*\* Navigating via the left menu does not update the page, and button click events (e.g., a simple counter or CRUD actions) do not fire unless the page is hard-refreshed.



\*\*Root cause (Blazor Web App render modes):\*\* interactive render mode (Interactive Server) was not fully applied to the layout/pages. In .NET 8/9 Blazor Web App you must enable interactivity via `@rendermode` on the layout or pages (not on `<Routes>`)



\*\*What I tried during the timebox:\*\*

\- Registered interactive services in `Program.cs` with  

&nbsp; `builder.Services.AddRazorComponents().AddInteractiveServerComponents()` and  

&nbsp; `app.MapRazorComponents<App>().AddInteractiveServerRenderMode()`.

\- Ensured Radzen services and assets were registered/loaded.

\- Attempted to set `@rendermode` on `<Routes>` (led to an exception about `ChildContent`/`RenderFragment`).



\*\*Workaround for reviewers (quick patch):\*\*

1\. In `App.razor`, keep `<Routes>` \*\*without\*\* `@rendermode`. Include:

&nbsp;  ```razor

&nbsp;  <HeadOutlet />

&nbsp;  <Routes>

&nbsp;    <NotFound>

&nbsp;      <LayoutView Layout="@typeof(MainLayout)">

&nbsp;        <p role="alert">Sorry, there’s nothing at this address.</p>

&nbsp;      </LayoutView>

&nbsp;    </NotFound>

&nbsp;  </Routes>

&nbsp;  <script src="\_framework/blazor.web.js"></script>

&nbsp;  <link rel="stylesheet" href="\_content/Radzen.Blazor/css/default.css" />

&nbsp;  <script src="\_content/Radzen.Blazor/Radzen.Blazor.js"></script>



