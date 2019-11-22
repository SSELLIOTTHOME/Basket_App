Readme for Shopping Basket Example

Application is broken into two projects

Basket_App - Main Console Application (no seperate DLL for logic layer, feels like overkill). Program.cs contains the application that
writes the the command lne.

Basket_App_Unit_Tests - A non full code coverage set of Unit Tests just to indicate what sort of Unit Tests would be suitable

Object Structure


	     IProduct									     IBasket
		|										|
		|										|
		V										V		
	Product(Abstract)	
											      Basket
		|
		|
		V
   	|---------------------|
 	V     		      V
Product_Stock_Item	Product_Voucher





	IVoucher						IVoucherFactory
	  |								|
	  |								|
	  V								v
|---------------------|					         VoucherFactory
V       	      V
Gift_Voucher	Offer Voucher




IProductFactory
	|
	|
	V
ProductFactory



Comments on Implementation
---------------------------

Some Methods can be left as null if they are not there, yes I could use optional params, but I prefer to 
see where a value has been omitted by passing null - just a personal preference.

Everything is extracted to Interfaces for use in DI later (it feels a natural way to code anyway), but no container is used as that would
be overkill. Factories used instead.

The code to actually apply the discount is within the voucher itself. Probably not a good idea to include
in the basket implementation, as this would mean that a change to the way Offer Vouchers are calculated
would require testing Gift Vouchers as well. This does mean the Voucher is aware of the basket, but
as everything is implemented out to interfaces this not a hard dependency. 

The use of types for checking the type of voucher does mean in Unit Tests, we have to use concrete
implementations of both the Product and the Basket, only the Basket_Item 





