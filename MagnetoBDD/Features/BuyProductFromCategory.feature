Feature: BuyProductFromCategory

Using differnt category on navbar selects product and proceeds to buy

@e2e-testing
Scenario: Using differnt category to buy product
	Given User will be on home page
	When User selects category button on navbar
	And selects product category
	And selects product from list
	Then User will be on selected product page
	When Filters product by size
	And selects the product from list
	And User clicks on the add to cart button without choosing required critetia
	Then Required criteria error occurs
