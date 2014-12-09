![PAYMILL icon](https://static.paymill.com/r/335f99eb3914d517bf392beb1adaf7cccef786b6/img/logo-download_Light.png)

# PAYMILL .NET

[![Build status](https://ci.appveyor.com/api/projects/status/6k6s2wk3y4cptl0s/branch/master?svg=true)](https://ci.appveyor.com/project/VMarinov1/paymill-net/branch/master)

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

### Compile for .Net 4.0
If you need to compile it for .Net 4.0 you should install [Async Targeting Pack] (http://blogs.msdn.com/b/bclteam/p/asynctargetingpackkb.aspx) through [Nuget](https://www.nuget.org/packages/Microsoft.Bcl.Async) as a replacement for the AsyncCTP.

## What's new

We have released version 3, which follows version 2.1 of the PAYMILL's REST API. This version is not backwards compatible with version 2, which follows version 2.0 of the PAYMILL's REST API. Concrete changes in the changelog.

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
### Updating objects
Because all methods of Wrapper for the PAYMILL API is asynchronous. To catch real paymill exception you must use:
```cs
 }catch(AggregateException  ex){

  PaymillException paymillEx = ex.InnerException;

}
```

## Changelog

### 3.0.1
+ Added Transaction property missing in preauth.

### 3.0.0
+ Works with version 2.1 of PAYMILL's REST API.
+ update project dependencies
+ improvement: remove workaround for subscriptions without offer.
+ improvement: now preauthorizations can be created with description.

### 2.1.4
+ improving client and payment object deserialization.
+ Improving interval, adding weekday.
+ Add new web hook types for subscriptions.


### 2.1.2
+ Made Subscription's next_capture_at nullable.

### 2.1.1
+ Fix problem in parsing response of some transactions.

### 2.1.0
+ Add Description in Preauthorization

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

## Contributors

To contribute to the wrapper, please fork it and create a pull request.

Thanks to:

* [jcantos](https://github.com/jcantos) for the original wrapper.
* [digitalcreations](https://github.com/digitalcreations) for the core changes for v2.
* all [contributors](https://github.com/paymill/paymill-net/graphs/contributors)

## License

Copyright 2013 PAYMILL GmbH.

MIT License (enclosed)
