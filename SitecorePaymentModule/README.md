
![PAYMILL icon](https://static.paymill.com/r/335f99eb3914d517bf392beb1adaf7cccef786b6/img/logo-download_Light.png)


# Paymill payment module for [Sitecore](http://www.sitecore.net/)

This module allows payments through [PAYMILL](https://www.paymill.com/) API in [Sitecore](http://www.sitecore.net/) CMS.

## Getting started

- If you are not familiar with PAYMILL, start with the [documentation](https://www.paymill.com/en-gb/documentation-3/).
- Install the latest release.
- Check the samples.
- Check the API [reference](https://www.paymill.com/en-gb/documentation-3/reference/api-reference/).
- Check the additional [documentation](https://paymill.codeplex.com/documentation).
- Check the tests.

## Installation 

Before install the module please install Paymill Wrapper for the PAYMILL API, run the following command in the [Package Manager Console](http://docs.nuget.org/docs/start-here/using-the-package-manager-console)

### Nuget
This module is hosted in nuget and can be installed in Visual Studio via the package manager using the following command:

```nuget
PM> Install-Package Paymill.Sitecore
```

### Manual
All you need to do is to copy PaymillSubLayout.ascx, PaymillSubLayout.ascx.cs, PaymillSubLayout.ascx.designer.cs to layouts. Copy css and js to coresponding folder. 


### Configuration

To configure the module set your private in PaymillSubLayout.ascx.cs

```c#

PaymillContext paymill = new PaymillContext("YOUR PRIVATE KEY");

```

And set your public key in PaymillSubLayout.ascx

```c#

 var PAYMILL_PUBLIC_KEY = 'YOUR PUBLIC KEY';

```

## Usage

When this layout is installed, you can reuse it in your Sitecore project. For more information how to insert layouts in your project please read sitecore's developer [documentation](http://sdn.sitecore.net/Products/ECM/ECM%201,-d-,3/Documentation.aspx)

## License

Copyright 2014 PAYMILL GmbH.

MIT License (enclosed)

