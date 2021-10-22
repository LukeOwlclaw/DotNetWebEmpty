/// <amd-module name='Global'/>
/// Module naming is necessary for use ASP.NET bundling: Bundle must contain named modules in order for requireJS to find and use them.
/// All named modules must be made known to RequireJS. This is done by RequireJsHelper and _RequireJs.cshtml
 
// tslib is a helper library of TypeScript. It should be included in (and thus loaded by) all modules which require it
// due to '"importHelpers": true' in tsconfig.json. However, this does not work reliably (error tslib not found/loaded)
// Workaround: Load tslib globally for all Fabric pages here:
import "tslib";
import "jquery";

/* eslint-disable no-console */
for (const x in [3, 4, 5]) {
    console.log(x);
}

for (const x in { a: 3, b: 4, c: 5 }) {
    console.log(x);
}

const users = [
    { name: "Oby", age: 12 },
    { name: "Heera", age: 32 },
];

const loggedInUser = users[0]!;
console.log(loggedInUser.age);