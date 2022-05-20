<div align="center">

URL | Branch | Environment | Workflow
--- | ------ | ----------- | ------
https://g7-wa-hakimlivs-dev.azurewebsites.net/ | Dev | Development | [![Dev](https://github.com/PUVSNV21-KVALIT21/Grupp7/workflows/Dev/badge.svg)](https://github.com/PUVSNV21-KVALIT21/Grupp7/actions/workflows/dev_g7-wa-hakimlivs-dev.yml)
https://g7-wa-hakimlivs-test.azurewebsites.net/ | Test| Test | [![Test](https://github.com/PUVSNV21-KVALIT21/Grupp7/workflows/Test/badge.svg)](https://github.com/PUVSNV21-KVALIT21/Grupp7/actions/workflows/test_g7-wa-hakimlivs-test.yml)
https://g7-wa-hakimlivs-prod.azurewebsites.net/ | Prod | Production | [![Prod](https://github.com/PUVSNV21-KVALIT21/Grupp7/workflows/Prod/badge.svg)](https://github.com/PUVSNV21-KVALIT21/Grupp7/actions/workflows/prod_g7-wa-hakimlivs-prod.yml)

</div>

# Group7 

## Architecture

### Web App

ASP.NET Razor Pages Web Application .NET 6, Entity Framework Core & individual accounts

#### NuGet Packages

* Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore Version="6.0.4"
* Microsoft.AspNetCore.Identity.EntityFrameworkCore Version="6.0.4" 
* Microsoft.AspNetCore.Identity.UI Version="6.0.4"
* Microsoft.AspNetCore.Mvc.NewtonsoftJson Version="6.0.4"
* Microsoft.EntityFrameworkCore Version="6.0.4"
* Microsoft.EntityFrameworkCore.SqlServer Version="6.0.4"
* Microsoft.EntityFrameworkCore.Tools Version="6.0.4"


#### Razor pages

* Index.cshtml: landing page.
* Products: Create, Edit, Details & Delete.
* Orders: Create, Details & Delete.
* Admin: Index, Products, Orders & Customers
* MyPages: Index
* Register: Index

#### Utilities

* DecodeHTML()
* DisplayPrice()
* DisplayUnitValue()
* GetProductDiscountPercentage()
* StripHTML()
* DropDatabase()

#### LocalStorage

* Product added to cart: `(<productID>, <quantity>)` (In use, CartCounter only)
* Number of products in cart: `('cartQuantity', <quantity>)` (Function not in use, can be used in a future sprint)

### Database

* PostgreSQL

### Models

* Order
* Product
* User
* Cart

### Tests

#### Unit-tests MS-Test

* DisplayNoDecimalPrice()
* DisplayDecimalPrice()
* DisplayMultiplePrice()
* DiscountPercentageInt()
* DiscountPercentageDouble()
* StripHTMLTags()
