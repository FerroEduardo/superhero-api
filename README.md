<div align="center">

# SuperHeroAPI

[![test](https://github.com/FerroEduardo/superhero-api/actions/workflows/test.yml/badge.svg)](https://github.com/FerroEduardo/superhero-api/actions/workflows/test.yml)
[![build](https://github.com/FerroEduardo/superhero-api/actions/workflows/build.yml/badge.svg)](https://github.com/FerroEduardo/superhero-api/actions/workflows/build.yml)

</div>


> API inspired by these tutorials: [1](https://youtu.be/8pH5Lv4d5-g), [2](https://youtu.be/RXSPCIrrjHc)

> Swagger: http://localhost:5015/swagger

## Routes

| Description                | Method | URL                                       | Body                                                                            |
|:---------------------------|:------:|:-------------------|:-------------------------------------------------------------------------------------------------------|
| Sign in                    |  POST  | /auth/signin       | { "username": "username", "password": "password" }                                                     |
| Sign up                    |  POST  | /auth/signup       | { "username": "username", "password": "password" }                                                     |
| Index user super heroes    |  GET   | /superhero         |                                                                                                        |
| Show user super hero       |  GET   | /superhero/:id     |                                                                                                        |
| Create user super hero     |  POST  | /superhero         | { "name": "string", "firstName": "string", "lastName": "string", "place": "string" }                   |
| Update user super hero     |  PUT   | /superhero/:id     | { "name": "string", "firstName": "string", "lastName": "string", "place": "string" }                   |
| Delete user super hero     | DELETE | /superhero/:id     |                                                                                                        |

## Useful commands

1. If `dotnet-ef` is not installed: `dotnet tool install --global dotnet-ef`
0. Run migrations: `dotnet ef database update`