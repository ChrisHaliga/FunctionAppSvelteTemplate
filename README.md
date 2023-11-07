# FunctionAppSvelteTemplate

Function App with dependency injection that builds and hosts svelte code through web endpoints.


__Suggestions for Working in the Frontend__
*I use Visual Studio and the extension, [Svelte for VS Code](https://marketplace.visualstudio.com/items?itemName=svelte.svelte-vscode)*
*When I work on this project, I open the frontend folder in visual studio and treat it as if it was a standalone Svelte project*

Running "npm run dev" will allow you to use hot reloading in development, but keep in mind that the Svelte files will not be tracked directly and if you want the function app to reflect your changes, you will need a manual rebuild before serving it.


__How the Build Process Works__
![BuildProcess drawio](https://github.com/ChrisHaliga/FunctionAppSvelteTemplate/assets/22923487/fd96396c-3246-4f02-9904-8d4b38c47099)
1. User initiates .NET Build process
2. pre-build.ps1 is started
   2a. Frontend's Appsetings.ts has built set to true
   2b. `npm run build` is started
       2bi. Svelte is compiled into dist folder
3. Normal .NET Build runs
   3a. bin folder is generated
5. post-build.ps1 is started
   4a. dist folder is copied from Frontend
   4b. dist folder is pasted into bin folder
