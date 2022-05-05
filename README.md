<div align="center">

URL | Branch | Environment | Workflow
--- | ------ | ----------- | ------
https://g7-wa-hakimlivs-dev.azurewebsites.net/ | Dev | Development | [![Dev](https://github.com/PUVSNV21-KVALIT21/Grupp7/workflows/Dev/badge.svg)](https://github.com/PUVSNV21-KVALIT21/Grupp7/actions/workflows/dev_g7-wa-hakimlivs.yml)
https://g7-wa-hakimlivs-test.azurewebsites.net/ | Test| Test | [![Test](https://github.com/PUVSNV21-KVALIT21/Grupp7/workflows/Test/badge.svg)](https://github.com/PUVSNV21-KVALIT21/Grupp7/actions/workflows/test_g7-wa-hakimlivs.yml)
https://g7-wa-hakimlivs-prod.azurewebsites.net/ | Prod | Production | [![Prod](https://github.com/PUVSNV21-KVALIT21/Grupp7/workflows/Prod/badge.svg)](https://github.com/PUVSNV21-KVALIT21/Grupp7/actions/workflows/prod_g7-wa-hakimlivs.yml)

</div>

# Grupp7 

## Arkitektur

### Web App

.NET 6 individual identity

#### Razor pages

* Index.cshtml: landing page.
* Products: Create, Edit, Details, Delete.

#### LocalStorage

* Product added to cart: (<productID>, <quantity>)
* Number of products in cart: ('cartQuantity', <quantity>)

### Databas

#### Entity Framework Core 6.0.4

##### Modeller

* Order
* Product
* User

### Tester

#### Unit-tester
