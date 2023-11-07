# FunctionAppSvelteTemplate

Function App with dependency injection that builds and hosts svelte code through web endpoints.


## Suggestions for Working in the Frontend
- I use Visual Studio and the extension, [Svelte for VS Code](https://marketplace.visualstudio.com/items?itemName=svelte.svelte-vscode) 
- When I work on this project, I open the frontend folder in visual studio and treat it as if it was a standalone Svelte project

> [!NOTE]
Running "npm run dev" will allow you to use hot reloading in development, but keep in mind that the Svelte files will not be tracked directly and if you want the function app to reflect your changes, you will need a manual rebuild before serving it.


## How the Build Process Works
![BuildProcess drawio](https://github.com/ChrisHaliga/FunctionAppSvelteTemplate/assets/22923487/fd96396c-3246-4f02-9904-8d4b38c47099)
1. Developer initiates .NET Build process
2. pre-build.ps1 is started
   2a. Frontend's Appsetings.ts has built set to true
   2b. `npm run build` is started
       2bi. Svelte is compiled into dist folder
3. Normal .NET Build runs
   3a. bin folder is generated
5. post-build.ps1 is started
   4a. dist folder is copied from Frontend
   4b. dist folder is pasted into bin folder
