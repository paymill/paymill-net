![PAYMILL icon](https://static.paymill.com/r/335f99eb3914d517bf392beb1adaf7cccef786b6/img/logo-download_Light.png)

# PAYMILL .NET

A .NET wrapper for the  [PAYMILL](https://www.paymill.com/) API.

## Getting started

- If you are not familiar with PAYMILL, start with the [documentation](https://www.paymill.com/en-gb/documentation-3/).
- Install the latest release.
- Check the samples.
- Check the API [reference](https://www.paymill.com/en-gb/documentation-3/reference/api-reference/).
- Check the additional [documentation](https://paymill.codeplex.com/documentation).
- Check the tests.

## Installation

### Nuget
To install Wrapper for the PAYMILL API, run the following command in the [Package Manager Console](http://docs.nuget.org/docs/start-here/using-the-package-manager-console)

```
PM> Install-Package PaymillWrapper 
```
More info [here](https://www.nuget.org/packages/PaymillWrapper/)

### Manual
Download the lastest stable release from https://paymill.codeplex.com/releases.

## What's new

We have released version 2. This version is not backwards compatible with version 1. Concrete changes in the changelog.

## Usage

Initialize the library by providing your api key:
```cs
  PaymillContext paymillContext = new PaymillContext( "<YOUR PRIVATE API KEY>" );
```
PaymillContext loads the context of PAYMILL for a single account, by providing a merchants private key. It creates 8 services, which represents the PAYMILL API:

 * ClientService
 * OfferService
 * PaymentService
 * PreauthorizationService
 * RefundService
 * SubscriptionService
 * TransactionService
 * WebhookService

These services should not be created directly. They have to be obtained by the context's accessors.


### Using services

This library is based on asynchronous programming in .NET. All service calls returns Task.You use Task if no meaningful value is returned when the method is completed. If you're new to asynchronous programming or do not understand how an async method uses the await keyword to do potentially long-running work without blocking the caller’s thread, you should read the introduction in [Asynchronous Programming with Async and Await (C# and Visual Basic)](http://msdn.microsoft.com/en-us/library/hh191443.aspx).  
To get the result of asynchronous task jut call .Result, more about Task and how it Return Types [Async Return Types (C# and Visual Basic)](http://msdn.microsoft.com/en-us/library/hh524395.aspx)

In all cases, you'll use the predefined service classes to access the PAYMILL API.

To fetch a service instance, call *service name* from paymillContext, like
```cs 
  ClientService clientService = paymillContext.ClientService;
```
Every service instance provides basic methods for CRUD functionality.

### Creating objects

Every service provides instance factory methods for creation. They are very different for every service, because every object can be created in a different way. The common pattern is
```cs 
  xxxService.CreateXXX( params... );
```
For example: client can be created with two optional parameters: *email* and *description*. So we have four possible methods to create the client:
* clientService.CreateAsync() - creates a client without email and description
* clientService.CreateWithEmailAsync( "john.rambo@paymill.com" ) - creates a client with email
* clientService.CreateWithDescriptionAsync( "CRM Id: fake_34212" ) - creates a client with description
* clientService.CreateWithEmailAndDescriptionAsync( "john.rambo@paymill.com", "CRM Id: fake_34212" ) - creates a client with email and description

### Retrieving objects

You can retrieve an object by using the get() method with an object id:
```cs
  Task<Client> client = clientService.GetAsync( "client_12345" );
```
or with the instance itself, which also refreshes it:
```cs
  clientService.GetAsync( client );
```
This method throws an ApiException if there is no client under the given id.

### Retrieving lists

To retrieve a list you may simply use the list() method:
```cs
  PaymillList<Client> clients = clientService.ListAsync().Result;
```
You may provide a filter and order to list method:
```cs
  PaymillList<Client> clients =
    clientService.ListAsync(
      Client.CreateFilter().ByEmail( "john.rambo@paymill.com" ),
      Client.CreateOrder().ByCreatedAt().Desc()
    ).Result;
```
This will load only clients with email john.rambo@paymill.com, order descending by creation date.

### Updating objects

In order to update an object simply call a service's update() method:
```cs
  clientServive.UpdateAsync( client );
```
The update method also refreshes the the given instance. For example: If you changed the value of 'CreatedAt' locally and  pass the instance to the Update() method, it will be refreshed with the data from PAYMILL. Because 'CreatedAt' is not updateable field your change will be lost.

### Deleting objects

You may delete objects by calling the service's delete() method with an object instance or object id.
```cs
  clientService.DeleteAsync( "client_12345" );
```
or
```cs
  clientService.DeleteAsync( client );
```

## Changelog

### 2.0.0
* Change library to asynchronous programming.
+ Add factory methods for all objects.
+ Clear object's models.
+ Add 'Updatable' attribute.
+ Add custom JSON converters.
+ Add filters and orders.
+ Hide API url.
+ Change all enums to EnumBaseType.
+ Merge with [digitalcreations / paymillsharp](https://github.com/digitalcreations/paymillsharp)

### 1.2.1
* Add source field and Status for Preauthorization.

### 1.2.0
* Add  currency and billed_at to the Fee model.

### 1.0
Create library.

## License

Copyright 2013 PAYMILL GmbH.

MIT License (enclosed)
