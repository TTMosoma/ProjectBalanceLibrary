\## Known Issue (documented during timebox)



\*\*Symptom:\*\* Navigating via the left menu does not update the page, and button click events (e.g., a simple counter or CRUD actions) do not fire unless the page is hard-refreshed.



\*\*Root cause (Blazor Web App render modes):\*\* interactive render mode (Interactive Server) was not fully applied to the layout/pages. In .NET 8/9 Blazor Web App you must enable interactivity via `@rendermode` on the layout or pages (not on `<Routes>`)



\*\*What I tried during the timebox:\*\*

\- Registered interactive services in `Program.cs` with  

&nbsp; `builder.Services.AddRazorComponents().AddInteractiveServerComponents()` and  

&nbsp; `app.MapRazorComponents<App>().AddInteractiveServerRenderMode()`.

\- Ensured Radzen services and assets were registered/loaded.

\- Attempted to set `@rendermode` on `<Routes>` (led to an exception about `ChildContent`/`RenderFragment`).



\*\*Workaround for reviewers (quick patch):\*\*  The workaround was not added back due to timebox challange



