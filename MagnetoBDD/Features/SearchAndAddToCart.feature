Feature: SearchAndAddToCart

Searching for a product and adding the product to the cart and proceed to checkout

@e2e-testing
Scenario Outline: Searching for a product and adding the product to the cart
	Given User will be on home page
	When User search for a product '<SearchText>' and click enter
	Then User will be on search result page contains '<SearchText>'
	When User selects a product at position '<ProductPosition>'
	Then User will be on the selected product page
	When User selects size and colour
	And User clicks on add to cart button
	And User checks cart and click on proceed button
	Then User will be on checkoutpage

Examples: 
	| SearchText | ProductPosition |
	| pants      | 3               |
	| tops       | 4               |

