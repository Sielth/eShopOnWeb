Feature: Basket

@AddItem
Scenario: Add an item to the basket
	Given an item with id 1
	When the item is added into the basket
	Then the basket should contain the item with id 1
	
@RemoveItem
Scenario: Remove an item from the basket
	Given a basket that contains an item with id 1
	When the item is removed from the basket
	Then the basket should not contain the item with id 1