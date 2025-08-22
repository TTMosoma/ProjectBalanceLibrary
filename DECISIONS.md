\## Incident \& trade-off (interactivity)

While integrating the Blazor Web App render model, I hit an interactivity issue: routes rendered, but events (menu navigation, button clicks) didn’t fire without a hard refresh. I believe this probably originated from placing `@rendermode` on `<Routes>`, which conflicts with `ChildContent` serialization. I chose to document the issue and ship the functional domain logic (EF Core, rules, Radzen CRUD, migrations) within the timebox. The fix is straightforward: move `@rendermode InteractiveServer` to the layout or pages, load `\_framework/blazor.web.js`, and keep `<Routes>` non-interactive. Steps are documented in README.



