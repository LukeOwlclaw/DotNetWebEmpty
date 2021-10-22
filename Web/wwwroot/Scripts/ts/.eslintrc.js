module.exports =
{
    "parser": "@typescript-eslint/parser",
    "parserOptions": {
        // tsconfigRootDir must be absolute path according to:
        // https://github.com/typescript-eslint/typescript-eslint/blob/HEAD/docs/getting-started/linting/TYPED_LINTING.md
        // In JS file we have constant "__dirname" containing the directory path of the currently executing script file.
        // See: https://eslint.org/docs/rules/no-path-concat
        // __dirname is thus the solution path + "wwwroot\Scripts\ts\"
        // Let's configure tsconfigRootDir to point to the project directory:
        "tsconfigRootDir": __dirname + "/../../../",
        "project": ["./wwwroot/Scripts/ts/tsconfig.json"]
    },
    extends: [
        "plugin:@typescript-eslint/recommended-requiring-type-checking",
    ],
    "overrides": [
        {
            "files": ["*.ts", "*.tsx"],
            "rules": {
                "@typescript-eslint/no-unnecessary-condition": ["error"],
                "@typescript-eslint/strict-boolean-expressions": ["error"]
            }
        }
    ]
};