# users_crud_api


Backend done

* app - service configuration, controllerrs, convertation from model classses to Dto (and back)
* app.Contracts - Data contracts. Independent from app, app.Data, app.Services projects.
* app.Data - model + interaction with MySql via  EF Core 5
* app.Services - Buisness logic.
* app.Shared - common classes (errors, extensions etc) for any project. Do not reference other projects of solution

Console client done partialy (one method without validation). Example:

<pre><code>ConsoleClient.exe CreateUser -id 1 -n Name -s New -u admin -p Master1234 </code></pre>
